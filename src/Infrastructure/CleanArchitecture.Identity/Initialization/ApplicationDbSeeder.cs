using CleanArchitecture.Application.Features.Identities.Roles;
using CleanArchitecture.Domain.Constants.Authorization;
using CleanArchitecture.Identity.Auth;
using CleanArchitecture.Identity.DatabaseContext;
using CleanArchitecture.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Identity.Initialization;

internal class ApplicationDbSeeder
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ApplicationDbSeeder> _logger;
    private readonly AdminSetting _adminSetting;

    public ApplicationDbSeeder(
        RoleManager<ApplicationRole> roleManager, 
        UserManager<ApplicationUser> userManager, 
        ILogger<ApplicationDbSeeder> logger,
        IOptions<AdminSetting> adminSetting)
    {
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _adminSetting = adminSetting.Value;
    }

    public async Task SeedDatabaseAsync(ApplicationIdentityDbContext dbContext, CancellationToken cancellationToken)
    {
        await SeedRolesAsync(dbContext);
        await SeedAdminUserAsync();
    }

    private async Task SeedRolesAsync(ApplicationIdentityDbContext dbContext)
    {
        foreach (string roleName in Roles.DefaultRoles)
        {
            if (await _roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                is not ApplicationRole role)
            {
                // Create the role
                _logger.LogInformation($"Seeding {roleName} Role for database.");
                role = new ApplicationRole(roleName, $"{roleName} Role for database");
                await _roleManager.CreateAsync(role);
            }

            // Assign permissions
            if (roleName == Roles.Customer)
            {
                await AssignPermissionsToRoleAsync(dbContext, Permissions.Customer, role);
            }
            else if (roleName == Roles.Admin)
            {
                await AssignPermissionsToRoleAsync(dbContext, Permissions.Admin, role);
            }
        }
    }

    private async Task AssignPermissionsToRoleAsync(ApplicationIdentityDbContext dbContext, IReadOnlyList<Permission> permissions, ApplicationRole role)
    {
        var currentClaims = await _roleManager.GetClaimsAsync(role);
        foreach (var permission in permissions)
        {
            if (!currentClaims.Any(c => c.Type == Claims.Permission && c.Value == permission.Name))
            {
                _logger.LogInformation("Seeding {role} Permission '{permission}' for database.", role.Name, permission.Name);
                dbContext.RoleClaims.Add(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = Claims.Permission,
                    ClaimValue = permission.Name,
                    CreatedBy = "ApplicationDbSeeder"
                });
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private async Task SeedAdminUserAsync()
    {
        if (await _userManager.Users.FirstOrDefaultAsync(u => u.Email == _adminSetting.Email)
            is not ApplicationUser adminUser)
        {
            adminUser = new ApplicationUser
            {
                FirstName = _adminSetting.FirstName,
                LastName = _adminSetting.LastName,
                Email = _adminSetting.Email,
                UserName = _adminSetting.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = _adminSetting.Email.ToUpperInvariant(),
                NormalizedUserName = _adminSetting.UserName.ToUpperInvariant(),
                IsActive = true
            };

            _logger.LogInformation("Seeding Default Admin User for database.");
            var password = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = password.HashPassword(adminUser, _adminSetting.Password);
            await _userManager.CreateAsync(adminUser);
        }

        // Assign role to user
        if (!await _userManager.IsInRoleAsync(adminUser, Roles.Admin))
        {
            _logger.LogInformation("Assigning Admin Role to Admin User for database.");
            await _userManager.AddToRoleAsync(adminUser, Roles.Admin);
        }
    }
}

using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Features.Identities.Users;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity.Services;

internal partial class UserService
{
    public async Task<List<UserRoleResponse>> GetRolesAsync(Guid userId, CancellationToken cancellationToken)
    {
        var userRoles = new List<UserRoleResponse>();

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null) throw new NotFoundException("User Not Found.");
        var roles = await _roleManager.Roles.AsNoTracking().ToListAsync(cancellationToken);
        if (roles is null) throw new NotFoundException("Roles Not Found.");
        foreach (var role in roles)
        {
            userRoles.Add(new UserRoleResponse
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Description = role.Description,
                IsEnabled = await _userManager.IsInRoleAsync(user, role.Name!)
            });
        }

        return userRoles;
    }

    public async Task<string> AssignRolesAsync(Guid userId, UserRolesRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User Not Found.");

        foreach (var userRole in request.UserRoles)
        {
            // Check if Role Exists
            if (await _roleManager.FindByNameAsync(userRole.RoleName!) is not null)
            {
                if (userRole.IsEnabled)
                {
                    if (!await _userManager.IsInRoleAsync(user, userRole.RoleName!))
                    {
                        await _userManager.AddToRoleAsync(user, userRole.RoleName!);
                    }
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, userRole.RoleName!);
                }
            }
        }

        return "User Roles Updated Successfully.";
    }
}
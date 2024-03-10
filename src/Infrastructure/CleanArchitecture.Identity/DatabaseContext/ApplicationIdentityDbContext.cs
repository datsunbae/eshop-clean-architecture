using CleanArchitecture.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity.DatabaseContext;

public class ApplicationIdentityDbContext : IdentityDbContext<
        ApplicationUser, 
        ApplicationRole, 
        Guid, 
        IdentityUserClaim<Guid>, 
        IdentityUserRole<Guid>, 
        IdentityUserLogin<Guid>, 
        ApplicationRoleClaim, 
        IdentityUserToken<Guid>>
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationIdentityDbContext).Assembly);

        builder.HasDefaultSchema("Identity");

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable(name: "User");
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable(name: "Role");
        });

        builder.Entity<IdentityUserRole<Guid>>(entity =>
        {
            entity.ToTable("UserRoles");
        });

        builder.Entity<IdentityUserClaim<Guid>>(entity =>
        {
            entity.ToTable("UserClaims");
        });

        builder.Entity<IdentityUserLogin<Guid>>(entity =>
        {
            entity.ToTable("UserLogins");
        });

        builder.Entity<ApplicationRoleClaim>(entity =>
        {
            entity.ToTable("RoleClaims");
        });

        builder.Entity<IdentityUserToken<Guid>>(entity =>
        {
            entity.ToTable("UserTokens");
        });
    }
}

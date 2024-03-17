using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Application.Common.Persistence;
using CleanArchitecture.Domain.Categories;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Products;
using CleanArchitecture.Persistence.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace CleanArchitecture.Persistence;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly ICurrentUser _currentUser;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUser currentUser) : base(options)
    {
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // QueryFilters need to be applied before base.OnModelCreating
        builder.AppendGlobalQueryFilter<ISoftDelete>(s => s.DeletedOn == null);

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            UpdateAuditableEntities();

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<BaseEntity>> entries = 
                 ChangeTracker
                .Entries<BaseEntity>();

        foreach (EntityEntry<BaseEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedOn)
                    .CurrentValue = DateTime.UtcNow;

                entityEntry.Property(a => a.CreatedBy)
                    .CurrentValue = _currentUser.GetUserId();
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.LastModifiedOn)
                    .CurrentValue = DateTime.UtcNow;

                entityEntry.Property(a => a.LastModifiedBy)
                    .CurrentValue = _currentUser.GetUserId();
            }
        }
    }

}

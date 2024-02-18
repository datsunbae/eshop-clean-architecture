using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence.DataAccess.Repositories.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Infrastructure.Persistence.DataAccess;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserContext _userContext;

    public UnitOfWork(
        ApplicationDbContext dbContext,
        IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }

    public IGenericRepository<T> GetRepository<T>() where T : BaseEntity
    {
        return new GenericRepository<T>(_dbContext);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();

        await _dbContext.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync(); ;
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<BaseEntity>> entries =
            _dbContext
                .ChangeTracker
                .Entries<BaseEntity>();

        foreach (EntityEntry<BaseEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.Created)
                    .CurrentValue = DateTime.UtcNow;

                entityEntry.Property(a => a.CreatedBy)
                    .CurrentValue = _userContext.UserId;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.LastModified)
                    .CurrentValue = DateTime.UtcNow;

                entityEntry.Property(a => a.LastModifiedBy)
                    .CurrentValue = _userContext.UserId;
            }
        }
    }
}

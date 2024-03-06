﻿using Ardalis.Specification.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Persistence.Repositories;

public class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
    where TEntity : BaseEntity, IAggregateRoot
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public Repository(ApplicationDbContext dbContext, ICurrentUser currentUser) : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.DeletedOn = DateTime.UtcNow;
        entity.DeletedBy = _currentUser.GetUserId();

        _dbContext.Set<TEntity>().Update(entity);

        await SaveChangesAsync(cancellationToken);
    }
}

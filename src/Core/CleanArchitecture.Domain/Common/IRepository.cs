﻿using Ardalis.Specification;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Persistence;

public interface IRepository<TEntity> : IRepositoryBase<TEntity>
    where TEntity : BaseEntity
{
    Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}

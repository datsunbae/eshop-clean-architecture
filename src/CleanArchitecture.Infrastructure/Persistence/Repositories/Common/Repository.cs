using Ardalis.Specification.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories.Common;

public class Repository<T> : RepositoryBase<T>, IRepository<T>
    where T : BaseEntity
{
    public Repository(DbContext dbContext) : base(dbContext)
    {
    }
}

using Ardalis.Specification;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Interfaces.Persistence;

public interface IRepository<T> : IRepositoryBase<T>
    where T : BaseEntity
{
}

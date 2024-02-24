using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<T> GetRepository<T>() where T : BaseEntity;
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

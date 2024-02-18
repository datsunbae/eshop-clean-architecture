namespace CleanArchitecture.Domain.Common;

public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<T> GetRepository<T>() where T : BaseEntity;
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

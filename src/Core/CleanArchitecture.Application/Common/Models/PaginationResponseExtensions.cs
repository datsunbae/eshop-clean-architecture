using Ardalis.Specification;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Models;

public static class PaginationResponseExtensions
{
    public static async Task<PaginationResponse<TDestination>> PaginatedListAsync<T, TDestination>(
        this IReadRepositoryBase<T> repository, ISpecification<T, TDestination> spec, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        where T : BaseEntity
        where TDestination : class, IResponse
    {
        try
        {
            var list = await repository.ListAsync(spec, cancellationToken);
            int count = await repository.CountAsync(spec, cancellationToken);

            return new PaginationResponse<TDestination>(list, count, pageNumber, pageSize);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<PaginationResponse<T>> PaginatedListAsync<T>(
        this IReadRepositoryBase<T> repository, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        where T : class
    {
        var list = await repository.ListAsync(cancellationToken);
        int count = await repository.CountAsync(cancellationToken);

        return new PaginationResponse<T>(list, count, pageNumber, pageSize);
    }
}
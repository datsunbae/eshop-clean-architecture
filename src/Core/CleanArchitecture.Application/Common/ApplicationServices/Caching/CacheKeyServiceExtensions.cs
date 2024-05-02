using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.ApplicationServices.Caching;

public static class CacheKeyServiceExtensions
{
    public static string GetCacheKey<TEntity>(this ICacheKeyService cacheKeyService, object id)
        where TEntity : BaseEntity =>
        cacheKeyService.GetCacheKey(typeof(TEntity).Name, id);
}
namespace CleanArchitecture.Application.Common.Caching;

public interface ICacheKeyService
{
    public string GetCacheKey(string name, object id);
}
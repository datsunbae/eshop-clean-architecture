namespace CleanArchitecture.Application.Common.Interfaces.Caching;

public interface ICacheKeyService
{
    public string GetCacheKey(string name, object id);
}
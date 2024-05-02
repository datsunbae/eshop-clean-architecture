namespace CleanArchitecture.Application.Common.ApplicationServices.Caching;

public interface ICacheKeyService
{
    public string GetCacheKey(string name, object id);
}
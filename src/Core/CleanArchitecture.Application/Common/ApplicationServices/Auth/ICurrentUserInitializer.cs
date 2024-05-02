using System.Security.Claims;

namespace CleanArchitecture.Application.Common.ApplicationServices.Auth;

public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);

    void SetCurrentUserId(string userId);
}

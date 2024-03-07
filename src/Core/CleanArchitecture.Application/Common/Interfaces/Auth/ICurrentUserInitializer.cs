using System.Security.Claims;

namespace CleanArchitecture.Application.Common.Interfaces.Auth;

public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);

    void SetCurrentUserId(string userId);
}

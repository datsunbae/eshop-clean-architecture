namespace CleanArchitecture.Application.Features.Identities.Authentication;

public interface IAuthService
{
    Task<LoginResponse> GetTokenAsync(LoginRequest request, string ipAddress, CancellationToken cancellationToken);

    Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
}
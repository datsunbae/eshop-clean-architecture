using CleanArchitecture.Application.Features.Identities.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Identity;

[ApiController]
[Route("api/[controller]")]
public sealed class TokensController : ControllerBase
{
    private readonly IAuthService _tokenService;

    public TokensController(IAuthService tokenService) => _tokenService = tokenService;

    [HttpPost]
    [AllowAnonymous]
    public Task<LoginResponse> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        return _tokenService.GetTokenAsync(request, GetIpAddress()!, cancellationToken);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public Task<LoginResponse> RefreshAsync(RefreshTokenRequest request)
    {
        return _tokenService.RefreshTokenAsync(request, GetIpAddress()!);
    }

    private string? GetIpAddress() =>
        Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"]
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";
}
namespace CleanArchitecture.Application.Features.Identities.Tokens;

public record RefreshTokenRequest(string Token, string RefreshToken);
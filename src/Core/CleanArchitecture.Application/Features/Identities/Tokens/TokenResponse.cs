namespace CleanArchitecture.Application.Features.Identities.Tokens;

public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
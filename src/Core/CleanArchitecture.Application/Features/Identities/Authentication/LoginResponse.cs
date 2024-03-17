namespace CleanArchitecture.Application.Features.Identities.Authentication;

public record LoginResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
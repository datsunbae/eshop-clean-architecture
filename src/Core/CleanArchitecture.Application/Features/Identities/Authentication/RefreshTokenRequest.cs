namespace CleanArchitecture.Application.Features.Identities.Authentication;

public record RefreshTokenRequest(string Token, string RefreshToken);
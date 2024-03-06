namespace CleanArchitecture.Application.Identity.Tokens;

public record RefreshTokenRequest(string Token, string RefreshToken);
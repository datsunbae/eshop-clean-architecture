namespace CleanArchitecture.Application.Identity.Users.Tokens;

public record RefreshTokenRequest(string Token, string RefreshToken);
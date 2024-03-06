namespace CleanArchitecture.Application.Identity.Users.Tokens;

public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
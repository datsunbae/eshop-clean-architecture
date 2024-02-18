namespace CleanArchitecture.Infrastructure.Persistence.Authentication;

public sealed class JwtOptions
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double TokenValidityInMinutes { get; set; }
    public int RefreshTokenValidityInDays { get; set; }
}

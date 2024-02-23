using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Identity.Models;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime RefreshTokenExpiryTime { get; private set; }
}

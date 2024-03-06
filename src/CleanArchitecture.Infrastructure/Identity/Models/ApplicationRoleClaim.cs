using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Identity.Models;

public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public string? CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
}
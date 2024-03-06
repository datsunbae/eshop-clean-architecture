using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Entities;

public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public string? CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
}
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public string? Description { get; set; }

    public ApplicationRole(string name, string? description = null)
        : base(name)
    {
        Description = description;
        NormalizedName = name.ToUpperInvariant();
    }
}
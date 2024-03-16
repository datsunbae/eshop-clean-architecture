namespace CleanArchitecture.Application.Features.Identities.Roles;

public class RoleResponse
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public List<string>? Permissions { get; set; }
}
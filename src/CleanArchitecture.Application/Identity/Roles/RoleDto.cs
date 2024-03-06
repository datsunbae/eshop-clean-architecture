namespace CleanArchitecture.Application.Identity.Roles;

public class RoleDto
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public List<string>? Permissions { get; set; }
}
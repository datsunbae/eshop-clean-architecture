namespace CleanArchitecture.Application.Identity.Users;

public class UserRoleResponse
{
    public Guid? RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? Description { get; set; }
    public bool IsEnabled { get; set; }
}
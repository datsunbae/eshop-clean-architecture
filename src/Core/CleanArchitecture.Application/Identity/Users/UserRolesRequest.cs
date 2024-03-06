namespace CleanArchitecture.Application.Identity.Users;

public class UserRolesRequest
{
    public List<UserRoleResponse> UserRoles { get; set; } = new();
}
using CleanArchitecture.Application.Identity.Users.Roles;

namespace CleanArchitecture.Application.Identity.Roles;

public interface IRoleService
{
    Task<List<RoleDto>> GetListAsync(CancellationToken cancellationToken);

    Task<int> GetCountAsync(CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string roleName, Guid? excludeId);

    Task<RoleDto> GetByIdAsync(Guid id);

    Task<RoleDto> GetByIdWithPermissionsAsync(Guid roleId, CancellationToken cancellationToken);

    Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request);

    Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken);

    Task<string> DeleteAsync(Guid id);
}
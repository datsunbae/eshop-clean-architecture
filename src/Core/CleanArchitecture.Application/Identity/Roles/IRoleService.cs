namespace CleanArchitecture.Application.Identity.Roles;

public interface IRoleService
{
    Task<List<RoleResponse>> GetListAsync(CancellationToken cancellationToken);

    Task<int> GetCountAsync(CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string roleName, Guid? excludeId);

    Task<RoleResponse> GetByIdAsync(Guid id);

    Task<RoleResponse> GetByIdWithPermissionsAsync(Guid roleId, CancellationToken cancellationToken);

    Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request);

    Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken);

    Task<string> DeleteAsync(Guid id);
}
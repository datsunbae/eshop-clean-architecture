using CleanArchitecture.Application.Identity.Roles;
using CleanArchitecture.Domain.Constants.Authorization;
using CleanArchitecture.Identity.Auth.Permissions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Action = CleanArchitecture.Domain.Constants.Authorization.Action;

namespace CleanArchitecture.Api.Controllers.Identity;

public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService) => _roleService = roleService;


      
    [HttpGet]
    [MustHavePermission(Action.View, Resource.Roles)]
    [OpenApiOperation("Get a list of all roles.", "")]
    public Task<List<RoleResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        return _roleService.GetListAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    [MustHavePermission(Action.View, Resource.Roles)]
    [OpenApiOperation("Get role details.", "")]
    public Task<RoleResponse> GetByIdAsync(Guid id)
    {
        return _roleService.GetByIdAsync(id);
    }

    [HttpGet("{id}/permissions")]
    [MustHavePermission(Action.View, Resource.RoleClaims)]
    [OpenApiOperation("Get role details with its permissions.", "")]
    public Task<RoleResponse> GetByIdWithPermissionsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _roleService.GetByIdWithPermissionsAsync(id, cancellationToken);
    }

    [HttpPut("{id}/permissions")]
    [MustHavePermission(Action.Update, Resource.RoleClaims)]
    [OpenApiOperation("Update a role's permissions.", "")]
    public async Task<ActionResult<string>> UpdatePermissionsAsync(Guid id, UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        if (id != request.RoleId)
        {
            return BadRequest();
        }

        return Ok(await _roleService.UpdatePermissionsAsync(request, cancellationToken));
    }

    [HttpPost]
    [MustHavePermission(Action.Create, Resource.Roles)]
    [OpenApiOperation("Create or update a role.", "")]
    public Task<string> RegisterRoleAsync(CreateOrUpdateRoleRequest request)
    {
        return _roleService.CreateOrUpdateAsync(request);
    }

    [HttpDelete("{id}")]
    [MustHavePermission(Action.Delete, Resource.Roles)]
    [OpenApiOperation("Delete a role.", "")]
    public Task<string> DeleteAsync(Guid id)
    {
        return _roleService.DeleteAsync(id);
    }
}
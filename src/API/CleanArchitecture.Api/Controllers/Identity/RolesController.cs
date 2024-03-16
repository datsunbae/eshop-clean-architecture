using CleanArchitecture.Application.Features.Identities.Roles;
using CleanArchitecture.Identity.Auth.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Identity;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService) => _roleService = roleService;
      
    [HttpGet]
    [MustHavePermission(Action.View, Resource.Roles)]
    public Task<List<RoleResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        return _roleService.GetListAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    [MustHavePermission(Action.View, Resource.Roles)]
    public Task<RoleResponse> GetByIdAsync(Guid id)
    {
        return _roleService.GetByIdAsync(id);
    }

    [HttpGet("{id}/permissions")]
    [MustHavePermission(Action.View, Resource.RoleClaims)]
    public Task<RoleResponse> GetByIdWithPermissionsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _roleService.GetByIdWithPermissionsAsync(id, cancellationToken);
    }

    [HttpPut("{id}/permissions")]
    [MustHavePermission(Action.Update, Resource.RoleClaims)]
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
    public Task<string> RegisterRoleAsync(CreateOrUpdateRoleRequest request)
    {
        return _roleService.CreateOrUpdateAsync(request);
    }

    [HttpDelete("{id}")]
    [MustHavePermission(Action.Delete, Resource.Roles)]
    public Task<string> DeleteAsync(Guid id)
    {
        return _roleService.DeleteAsync(id);
    }
}
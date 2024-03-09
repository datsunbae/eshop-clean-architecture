using CleanArchitecture.Application.Identity;
using CleanArchitecture.Application.Identity.Users;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Identity.Auth.Permissions;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserService _userService;

    public PermissionAuthorizationHandler(IUserService userService) =>
        _userService = userService;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User?.GetUserId() is { } userId &&
            await _userService.HasPermissionAsync(new Guid(userId), requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
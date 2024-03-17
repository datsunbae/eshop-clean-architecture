using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.Identities.Users;
using CleanArchitecture.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Events;

internal class InvalidateUserPermissionCacheHandler :
    IDomainEventHandler<ApplicationUserUpdatedEvent>,
    IDomainEventHandler<ApplicationRoleUpdatedEvent>
{
    private readonly IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;

    public InvalidateUserPermissionCacheHandler(IUserService userService, UserManager<ApplicationUser> userManager) =>
        (_userService, _userManager) = (userService, userManager);

    public async Task Handle(ApplicationUserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        if (notification.IsRolesUpdated)
        {
            await _userService.InvalidatePermissionCacheAsync(notification.UserId, cancellationToken);
        }
    }

    public async Task Handle(ApplicationRoleUpdatedEvent notification, CancellationToken cancellationToken)
    {
        if (notification.IsPermissionsUpdated)
        {
            foreach (var user in await _userManager.GetUsersInRoleAsync(notification.RoleName))
            {
                await _userService.InvalidatePermissionCacheAsync(user.Id, cancellationToken);
            }
        }
    }
}

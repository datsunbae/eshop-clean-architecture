using CleanArchitecture.Application.Common.Interfaces.Messaging;
using CleanArchitecture.Application.Identity.Users;
using CleanArchitecture.Domain.Identity;
using CleanArchitecture.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Identity;

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
        throw new NotImplementedException();
    }
}
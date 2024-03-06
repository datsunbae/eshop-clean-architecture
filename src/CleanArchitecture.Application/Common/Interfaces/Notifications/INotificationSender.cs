namespace CleanArchitecture.Application.Common.Interfaces.Notifications;

public interface INotificationSender
{
    Task BroadcastAsync(INotificationMessage notification, CancellationToken cancellationToken);
    Task BroadcastAsync(INotificationMessage notification, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken);

    Task SendToAllAsync(INotificationMessage notification, CancellationToken cancellationToken);
    Task SendToAllAsync(INotificationMessage notification, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken);
    Task SendToGroupAsync(INotificationMessage notification, string group, CancellationToken cancellationToken);
    Task SendToGroupAsync(INotificationMessage notification, string group, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken);
    Task SendToGroupsAsync(INotificationMessage notification, IEnumerable<string> groupNames, CancellationToken cancellationToken);
    Task SendToUserAsync(INotificationMessage notification, Guid userId, CancellationToken cancellationToken);
    Task SendToUsersAsync(INotificationMessage notification, IEnumerable<Guid> userIds, CancellationToken cancellationToken);
}
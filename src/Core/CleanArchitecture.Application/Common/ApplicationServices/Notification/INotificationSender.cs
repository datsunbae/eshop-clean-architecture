namespace CleanArchitecture.Application.Common.ApplicationServices.Notification;

public interface INotificationSender
{
    Task SendToAllAsync(INotificationMessage notification, CancellationToken cancellationToken);
    Task SendToAllAsync(INotificationMessage notification, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken);
    Task SendToGroupAsync(INotificationMessage notification, string group, CancellationToken cancellationToken);
    Task SendToGroupAsync(INotificationMessage notification, string group, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken);
    Task SendToGroupsAsync(INotificationMessage notification, IEnumerable<string> groupNames, CancellationToken cancellationToken);
    Task SendToUserAsync(INotificationMessage notification, string userId, CancellationToken cancellationToken);
    Task SendToUsersAsync(INotificationMessage notification, IEnumerable<string> userIds, CancellationToken cancellationToken);
}
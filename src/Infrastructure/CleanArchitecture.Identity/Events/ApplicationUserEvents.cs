using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Identity.Events;

public abstract class ApplicationUserEvent : IDomainEvent
{
    public Guid UserId { get; set; } = default!;

    protected ApplicationUserEvent(Guid userId) => UserId = userId;
}

public class ApplicationUserUpdatedEvent : ApplicationUserEvent
{
    public bool IsRolesUpdated { get; set; }

    public ApplicationUserUpdatedEvent(Guid userId, bool isRolesUpdated = false)
        : base(userId) =>
        IsRolesUpdated = isRolesUpdated;
}
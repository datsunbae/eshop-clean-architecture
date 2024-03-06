using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Identity;

public abstract class ApplicationUserEvent : IDomainEvent
{
    public Guid UserId { get; private set; } = default!;

    protected ApplicationUserEvent(Guid userId) => UserId = userId;
}

public sealed class ApplicationUserCreatedEvent : ApplicationUserEvent
{
    public ApplicationUserCreatedEvent(Guid userId)
        : base(userId)
    {
    }
}

public sealed class ApplicationUserUpdatedEvent : ApplicationUserEvent
{
    public bool IsRolesUpdated { get; set; }

    public ApplicationUserUpdatedEvent(Guid userId, bool rolesUpdated = false)
        : base(userId) =>
        IsRolesUpdated = rolesUpdated;
}
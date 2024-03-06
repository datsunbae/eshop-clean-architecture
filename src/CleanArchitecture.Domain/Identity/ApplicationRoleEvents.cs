using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Identity;

public abstract class ApplicationRoleEvent : IDomainEvent
{
    public Guid RoleId { get; private set; } = default!;
    public string RoleName { get; private set; } = default!;
    protected ApplicationRoleEvent(Guid roleId, string roleName) =>
        (RoleId, RoleName) = (roleId, roleName);
}

public sealed class ApplicationRoleCreatedEvent : ApplicationRoleEvent
{
    public ApplicationRoleCreatedEvent(Guid roleId, string roleName)
        : base(roleId, roleName)
    {
    }
}

public sealed class ApplicationRoleUpdatedEvent : ApplicationRoleEvent
{
    public bool IsPermissionsUpdated { get; set; }

    public ApplicationRoleUpdatedEvent(Guid roleId, string roleName, bool permissionsUpdated = false)
        : base(roleId, roleName) =>
        IsPermissionsUpdated = permissionsUpdated;
}

public sealed class ApplicationRoleDeletedEvent : ApplicationRoleEvent
{
    public bool IsPermissionsUpdated { get; set; }

    public ApplicationRoleDeletedEvent(Guid roleId, string roleName)
        : base(roleId, roleName)
    {
    }
}
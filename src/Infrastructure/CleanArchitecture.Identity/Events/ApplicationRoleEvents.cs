using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Identity.Events;

public abstract class ApplicationRoleEvent : IDomainEvent
{
    public Guid RoleId { get; set; } = default!;
    public string RoleName { get; set; } = default!;
    protected ApplicationRoleEvent(Guid roleId, string roleName) =>
        (RoleId, RoleName) = (roleId, roleName);
}

public class ApplicationRoleUpdatedEvent : ApplicationRoleEvent
{
    public bool IsPermissionsUpdated { get; set; }

    public ApplicationRoleUpdatedEvent(Guid roleId, string roleName, bool permissionsUpdated = false)
        : base(roleId, roleName) =>
        IsPermissionsUpdated = permissionsUpdated;
}

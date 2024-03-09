using CleanArchitecture.Domain.Constants.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Identity.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = Permission.NameFor(action, resource);
}
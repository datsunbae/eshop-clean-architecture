using System.Collections.ObjectModel;

namespace CleanArchitecture.Application.Features.Identities.Roles;

public static class Roles
{
    public const string Admin = nameof(Admin);
    public const string Customer = nameof(Customer);

    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        Admin,
        Customer
    });

    public static bool IsDefault(string roleName) => DefaultRoles.Any(r => r == roleName);
}

using System.Collections.ObjectModel;

namespace CleanArchitecture.Domain.Constants.Authorization;

public static class Action
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
}

public static class Resource
{
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
}

public static class Permissions
{
    private static readonly Permission[] _all = new Permission[]
    {
        new("View Users", Action.View, Resource.Users),
        new("Search Users", Action.Search, Resource.Users),
        new("Create Users", Action.Create, Resource.Users),
        new("Update Users", Action.Update, Resource.Users),
        new("Delete Users", Action.Delete, Resource.Users),
        new("Export Users", Action.Export, Resource.Users),
        new("View UserRoles", Action.View, Resource.UserRoles),
        new("Update UserRoles", Action.Update, Resource.UserRoles),
        new("View Roles", Action.View, Resource.Roles),
        new("Create Roles", Action.Create, Resource.Roles),
        new("Update Roles", Action.Update, Resource.Roles),
        new("Delete Roles", Action.Delete, Resource.Roles),
        new("View RoleClaims", Action.View, Resource.RoleClaims),
        new("Update RoleClaims", Action.Update, Resource.RoleClaims),
        new("View Products", Action.View, Resource.Products, IsCustomer: true),
        new("Search Products", Action.Search, Resource.Products, IsCustomer: true),
        new("Create Products", Action.Create, Resource.Products),
        new("Update Products", Action.Update, Resource.Products),
        new("Delete Products", Action.Delete, Resource.Products),
        new("Export Products", Action.Export, Resource.Products),
    };

    public static IReadOnlyList<Permission> Admin { get; } = new ReadOnlyCollection<Permission>(_all);
    public static IReadOnlyList<Permission> Customer { get; } = new ReadOnlyCollection<Permission>(_all.Where(p => p.IsCustomer).ToArray());
}

public record Permission(string Description, string Action, string Resource, bool IsCustomer = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}

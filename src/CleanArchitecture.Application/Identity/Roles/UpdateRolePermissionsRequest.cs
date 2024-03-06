using FluentValidation;

namespace CleanArchitecture.Application.Identity.Roles;

public class UpdateRolePermissionsRequest
{
    public Guid RoleId { get; set; } = default!;
    public List<string> Permissions { get; set; } = default!;
}

public class UpdateRolePermissionsRequestValidator : AbstractValidator<UpdateRolePermissionsRequest>
{
    public UpdateRolePermissionsRequestValidator()
    {
        RuleFor(r => r.RoleId)
            .NotEmpty();

        RuleFor(r => r.Permissions)
            .NotNull();
    }
}
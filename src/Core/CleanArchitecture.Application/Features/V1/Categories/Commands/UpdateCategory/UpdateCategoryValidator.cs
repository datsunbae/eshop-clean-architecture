using FluentValidation;

namespace CleanArchitecture.Application.Features.V1.Categories.Commands.UpdateCategory;

public sealed class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();
    }
}

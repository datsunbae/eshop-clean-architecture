using FluentValidation;

namespace CleanArchitecture.Application.Features.V1.Categories.Commands.CreateCategory;

public class CreateProductValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateProductValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();
    }
}

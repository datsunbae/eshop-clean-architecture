using FluentValidation;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.AddBasketProductItem;

public sealed class AddBasketProductItemValidator : AbstractValidator<AddBasketProductItemCommand>
{
    public AddBasketProductItemValidator()
    {
        RuleFor(a => a.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty).WithMessage("Product Id is required.");

        RuleFor(a => a.Quantity)
            .NotEmpty()
            .NotNull().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity cannot be less than 0.");
    }
}

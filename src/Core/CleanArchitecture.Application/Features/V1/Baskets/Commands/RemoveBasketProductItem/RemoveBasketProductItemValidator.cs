using FluentValidation;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.RemoveBasketProductItem;

public class RemoveBasketProductItemValidator : AbstractValidator<RemoveBasketProductItemCommand>
{
    public RemoveBasketProductItemValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull()
            .NotEmpty().WithMessage("Product Id is required.");
    }
}

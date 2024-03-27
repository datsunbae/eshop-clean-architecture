using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.CheckoutBasket;

public sealed class CheckoutValidator : AbstractValidator<CheckoutCommand>
{
    public CheckoutValidator()
    {
        RuleFor(c => c.UserInformation)
            .NotNull().WithMessage("User information is required.");

        RuleFor(c => c.UserInformation.Phone)
            .NotEmpty()
            .NotNull().WithMessage("Phone number is required.")
            .MinimumLength(10).WithMessage("Phone number must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("Phone number must not exceed 50 characters.")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("Phone number not valid");

        RuleFor(c => c.UserInformation.Address)
            .NotEmpty()
            .NotNull().WithMessage("Address is required.");

        RuleFor(c => c.UserInformation.Address.City)
            .NotEmpty()
            .NotNull().WithMessage("City is required.");

        RuleFor(c => c.UserInformation.Address.Street)
            .NotEmpty()
            .NotNull().WithMessage("Street is required.");
    }
}

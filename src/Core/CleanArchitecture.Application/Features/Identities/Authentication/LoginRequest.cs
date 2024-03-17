using FluentValidation;

namespace CleanArchitecture.Application.Features.Identities.Authentication;

public record LoginRequest(string Email, string Password);

public class TokenRequestValidator : AbstractValidator<LoginRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                .WithMessage("Invalid Email Address.");

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}
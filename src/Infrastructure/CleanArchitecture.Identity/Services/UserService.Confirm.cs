using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Identity.Entities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CleanArchitecture.Identity.Services;

internal partial class UserService
{
    private async Task<string> GetEmailVerificationUriAsync(ApplicationUser user, string origin)
    {
        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        const string route = "api/users/confirm-email/";
        var endpointUri = new Uri(string.Concat($"{origin}/", route));
        string verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "userId", user.Id.ToString());
        verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
        return verificationUri;
    }

    public async Task<string> ConfirmEmailAsync(Guid userId, string code, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Where(u => u.Id == userId && !u.EmailConfirmed)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new InternalServerException("An error occurred while confirming E-Mail.");

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        return result.Succeeded
            ? string.Format("Account Confirmed for E-Mail {0}. You can now use the /api/tokens endpoint to generate JWT.", user.Email)
            : throw new InternalServerException(string.Format("An error occurred while confirming {0}", user.Email));
    }

    public async Task<string> ConfirmPhoneNumberAsync(Guid userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        _ = user ?? throw new InternalServerException("An error occurred while confirming Mobile Phone.");
        if (string.IsNullOrEmpty(user.PhoneNumber)) throw new InternalServerException("An error occurred while confirming Mobile Phone.");

        var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, code);

        return result.Succeeded
            ? user.PhoneNumberConfirmed
                ? string.Format("Account Confirmed for Phone Number {0}. You can now use the /api/tokens endpoint to generate JWT.", user.PhoneNumber)
                : string.Format("Account Confirmed for Phone Number {0}. You should confirm your E-mail before using the /api/tokens endpoint to generate JWT.", user.PhoneNumber)
            : throw new InternalServerException(string.Format("An error occurred while confirming {0}", user.PhoneNumber));
    }
}

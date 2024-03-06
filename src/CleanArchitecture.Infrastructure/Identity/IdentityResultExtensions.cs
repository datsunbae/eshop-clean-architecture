using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Identity;

internal static class IdentityResultExtensions
{
    public static List<ValidationError> GetErrors(this IdentityResult result) =>
        result.Errors.Select(e => new ValidationError(e.Code, e.Description)).ToList();
}
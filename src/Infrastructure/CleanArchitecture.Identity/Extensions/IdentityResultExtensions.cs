using CleanArchitecture.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Extensions;

internal static class IdentityResultExtensions
{
    public static List<Error> GetErrors(this IdentityResult result)
    {
        return result.Errors
            .Select(error => new Error(error.Code, error.Description))
            .ToList();
    }
}
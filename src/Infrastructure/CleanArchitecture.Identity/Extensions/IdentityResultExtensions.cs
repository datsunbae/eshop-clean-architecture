using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Extensions;

internal static class IdentityResultExtensions
{
    public static List<string> GetErrors(this IdentityResult result) =>
        result.Errors.Select(e => e.Description).ToList();
}
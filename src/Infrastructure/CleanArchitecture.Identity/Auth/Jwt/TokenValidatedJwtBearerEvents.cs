using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Application.Features.Identities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace CleanArchitecture.Identity.Auth.Jwt;

internal class TokenValidatedJwtBearerEvents : JwtBearerEvents
{
    public TokenValidatedJwtBearerEvents()
    {
    }

    /// <summary>
    /// This method contains the logic that validates the user's tenant and normalizes claims.
    /// </summary>
    /// <param name="context">The validated token context.</param>
    /// <returns>A task.</returns>
    public override async Task TokenValidated(TokenValidatedContext context)
    {
        var principal = context.Principal;

        if (principal is null)
        {
            throw new UnauthorizedException("Authentication Failed.");
        }

        var userId = principal.GetUserId();

        // The caller comes from an admin-consented, recorded issuer.
        var identity = principal.Identities.First();

        if(userId == string.Empty || userId is null)
            throw new UnauthorizedException("Authentication Failed.");

        // We use the nameidentifier claim to store the user id.
        var idClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
        identity.TryRemoveClaim(idClaim);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));

        // And the email claim for the email.
        var upnClaim = principal.FindFirst(ClaimTypes.Upn);
        if (upnClaim is not null)
        {
            var emailClaim = principal.FindFirst(ClaimTypes.Email);
            identity.TryRemoveClaim(emailClaim);
            identity.AddClaim(new Claim(ClaimTypes.Email, upnClaim.Value));
        }
    }
}

using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions;

public class UnauthorizedException : DomainException
{
    public UnauthorizedException(string message)
        : base(message, null, HttpStatusCode.Unauthorized)
    {
    }
}

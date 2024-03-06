using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions;

public class ForbiddenException : DomainException
{
    public ForbiddenException(string message) 
        : base(message, null, HttpStatusCode.Forbidden)
    {
    }
}

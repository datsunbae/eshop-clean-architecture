using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions;

public class BadRequestException : DomainException
{
    public BadRequestException(string message)
        : base(message, null, HttpStatusCode.BadRequest)
    {
    }
}

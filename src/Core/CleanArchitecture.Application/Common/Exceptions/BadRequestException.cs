using CleanArchitecture.Domain.Common;
using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions;

public class BadRequestException : DomainException
{
    public BadRequestException(IEnumerable<Error>? errors)
        : base("Bad request encountered.", errors, HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message)
        : base(message, null, HttpStatusCode.BadRequest)
    {
    }
}

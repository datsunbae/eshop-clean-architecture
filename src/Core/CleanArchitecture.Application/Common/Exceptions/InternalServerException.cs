using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions;

public class InternalServerException : DomainException
{
    public InternalServerException(string message, List<string>? errors = default)
        : base(message, errors, HttpStatusCode.InternalServerError)
    {
    }
}

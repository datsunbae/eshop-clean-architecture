using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions;

public class InternalServerException : DomainException
{
    public InternalServerException(string message, List<ValidationError>? errors = default)
        : base(message, errors, HttpStatusCode.InternalServerError)
    {
    }
}

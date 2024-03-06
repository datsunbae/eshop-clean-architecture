using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions;

public sealed class ValidationException : DomainException
{
    public ValidationException(IEnumerable<ValidationError> errors)
        : base("One or more validation errors occurred", errors, HttpStatusCode.BadRequest)
    {
    }
}

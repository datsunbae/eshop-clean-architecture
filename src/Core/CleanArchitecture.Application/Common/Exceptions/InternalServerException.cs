using CleanArchitecture.Domain.Common;
using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions;

public class InternalServerException : DomainException
{
    public InternalServerException(string message, List<Error>? errors = default)
        : base(message, errors, HttpStatusCode.InternalServerError)
    {
    }
}

using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message, IEnumerable<string>? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        Errors = errors;
        StatusCode = statusCode;
    }
    public IEnumerable<string>? Errors { get; }

    public HttpStatusCode StatusCode { get; }
}

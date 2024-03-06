using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(string message) 
        : base(message, null, HttpStatusCode.NotFound)
    {
    }
}

using CleanArchitecture.Domain.Common;
using MediatR;

namespace CleanArchitecture.Application.Common.Interfaces.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}

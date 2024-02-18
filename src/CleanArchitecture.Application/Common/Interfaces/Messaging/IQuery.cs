using CleanArchitecture.Domain.Common;
using MediatR;

namespace CleanArchitecture.Application.Common.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

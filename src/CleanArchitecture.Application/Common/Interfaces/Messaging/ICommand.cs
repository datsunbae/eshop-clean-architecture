using CleanArchitecture.Domain.Common;
using MediatR;

namespace CleanArchitecture.Application.Common.Interfaces.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

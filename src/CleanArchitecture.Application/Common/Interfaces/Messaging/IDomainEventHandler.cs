using CleanArchitecture.Domain.Common;
using MediatR;

namespace CleanArchitecture.Application.Common.Interfaces.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}

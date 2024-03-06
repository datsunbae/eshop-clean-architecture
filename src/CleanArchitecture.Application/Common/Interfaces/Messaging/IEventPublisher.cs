using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Interfaces.Messaging;

public interface IEventPublisher
{
    Task PublishAsync(IDomainEvent @event);
}

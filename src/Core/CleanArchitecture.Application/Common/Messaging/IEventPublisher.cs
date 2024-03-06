using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Messaging;

public interface IEventPublisher
{
    Task PublishAsync(IDomainEvent @event);
}

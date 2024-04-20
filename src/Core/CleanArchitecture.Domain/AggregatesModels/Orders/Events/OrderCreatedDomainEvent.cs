using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Orders.Events;

public sealed record OrderCreatedDomainEvent(Guid UserId) : IDomainEvent;

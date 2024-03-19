using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Products.Events;

public sealed record ProductUpdatedDomainEvent(Guid productId) : IDomainEvent;

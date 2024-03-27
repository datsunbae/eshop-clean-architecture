using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Products.Events;

public sealed record ProductUpdatedDomainEvent(Guid productId) : IDomainEvent;

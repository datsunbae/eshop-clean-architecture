using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;

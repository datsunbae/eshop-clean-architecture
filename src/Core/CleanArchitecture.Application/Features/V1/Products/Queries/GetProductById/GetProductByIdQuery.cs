using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Products.Models.Responses;

namespace CleanArchitecture.Application.Features.V1.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;

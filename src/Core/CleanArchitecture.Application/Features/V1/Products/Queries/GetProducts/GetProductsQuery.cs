using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Features.V1.Products.Queries.GetProducts;

public sealed class GetProductsQuery : PaginationFilter, IQuery<PaginationResponse<ProductResponse>>;


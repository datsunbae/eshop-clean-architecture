using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Features.V1.Orders.Models.Responses;

namespace CleanArchitecture.Application.Features.V1.Orders.Queries.GetOrderByUserId;

public sealed class GetOrderByUserIdQuery : PaginationFilter, IQuery<PaginationResponse<OrderResponse>>;


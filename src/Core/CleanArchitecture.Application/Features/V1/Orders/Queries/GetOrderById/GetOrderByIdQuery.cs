using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Orders.Models.Responses;

namespace CleanArchitecture.Application.Features.V1.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(Guid Id) 
    : IQuery<OrderResponse>;

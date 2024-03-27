using Ardalis.Specification;
using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Application.Common.Specification;
using CleanArchitecture.Application.Features.V1.Orders.Models.Responses;
using CleanArchitecture.Application.Features.V1.Orders.Queries.GetOrderByUserId;
using CleanArchitecture.Domain.AggregatesModels.Orders;

namespace CleanArchitecture.Application.Features.V1.Orders.Specifications;

public class OrderByUserIdPaginatedSpec : EntitiesByPaginationFilterSpec<Order, OrderResponse>
{
    public OrderByUserIdPaginatedSpec(GetOrderByUserIdQuery request, ICurrentUser currentUser) : base(request)
    {
        Query
            .Where(o => o.UserId == currentUser.GetUserId());
    }
}

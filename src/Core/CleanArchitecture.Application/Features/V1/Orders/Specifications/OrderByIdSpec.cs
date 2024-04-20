using Ardalis.Specification;
using CleanArchitecture.Application.Features.V1.Orders.Models.Responses;
using CleanArchitecture.Domain.AggregatesModels.Orders;

namespace CleanArchitecture.Application.Features.V1.Orders.Specifications;

public sealed class OrderByIdSpec : Specification<Order, OrderResponse>
{
    public OrderByIdSpec(Guid id)
    {
        Query
            .Where(o => o.Id == id)
            .Include(o => o.OrderItems)
            .ThenInclude(ot => ot.Product);
    }
}

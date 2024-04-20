using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Orders.Models.Responses;
using CleanArchitecture.Application.Features.V1.Orders.Specifications;
using CleanArchitecture.Domain.AggregatesModels.Orders;
using CleanArchitecture.Domain.AggregatesModels.Orders.Repository;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Constants.Authorization;
using Mapster;

namespace CleanArchitecture.Application.Features.V1.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICurrentUser _currentUser;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, ICurrentUser currentUser)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }
    public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository
            .FirstOrDefaultAsync(new OrderByIdSpec(request.Id), cancellationToken);

        if (order is null || 
            (order.UserId != _currentUser.GetUserId() && _currentUser.IsInRole(Roles.Customer)))
            return Result.Failure<OrderResponse>(OrderErrors.NotFound);

        return Result.Success(order);
    }
}

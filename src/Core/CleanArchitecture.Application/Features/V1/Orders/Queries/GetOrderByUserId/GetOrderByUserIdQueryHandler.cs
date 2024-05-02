using CleanArchitecture.Application.Common.ApplicationServices.Auth;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Features.V1.Orders.Models.Responses;
using CleanArchitecture.Application.Features.V1.Orders.Specifications;
using CleanArchitecture.Domain.AggregatesModels.Orders;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Orders.Queries.GetOrderByUserId;

public sealed class GetOrderByUserIdQueryHandler : IQueryHandler<GetOrderByUserIdQuery, PaginationResponse<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICurrentUser _currentUser;
    public GetOrderByUserIdQueryHandler(IOrderRepository orderRepository, ICurrentUser currentUser)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<Result<PaginationResponse<OrderResponse>>> Handle(
        GetOrderByUserIdQuery request, 
        CancellationToken cancellationToken)
    {
        return await _orderRepository
            .PaginatedListAsync(
                new OrderByUserIdPaginatedSpec(request, _currentUser), 
                request.PageNumber,
                request.PageSize);
    }
}

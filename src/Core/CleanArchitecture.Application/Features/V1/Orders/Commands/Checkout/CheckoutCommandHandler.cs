using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Domain.AggregatesModels.Orders.Repository;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Orders.Commands.Checkout;

public class CheckoutCommandHandler : ICommandHandler<CheckoutCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    public CheckoutCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public Task<Result<Guid>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

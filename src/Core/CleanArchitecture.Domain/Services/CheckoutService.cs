using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Baskets.Repository;
using CleanArchitecture.Domain.AggregatesModels.Orders;
using CleanArchitecture.Domain.AggregatesModels.Orders.Repository;
using CleanArchitecture.Domain.AggregatesModels.Shared;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Services;

public class CheckoutService : IDomainService
{
    private readonly IOrderRepository _orderRepository;

    public CheckoutService(IBasketRepository basketRepository, IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository)); 
    }

    public async Task<Order> Checkout(Basket basket, UserInfomation userInfomation)
    {
        Order order = Order.Create(basket, userInfomation);
        await _orderRepository.AddAsync(order);
        basket.Clear();

        return order;
    }
}

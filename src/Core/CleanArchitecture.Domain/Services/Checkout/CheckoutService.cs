using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Orders;
using CleanArchitecture.Domain.AggregatesModels.Products;
using CleanArchitecture.Domain.AggregatesModels.Shared;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Services.Checkout;

public class CheckoutService : IDomainService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public CheckoutService(IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<Order> Checkout(Basket basket, UserInformation userInfomation)
    {
        Order order = Order.Create(basket, userInfomation);
        await _orderRepository.AddAsync(order);
        basket.Clear();

        return order;
    }
}

using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Orders.Enums;
using CleanArchitecture.Domain.AggregatesModels.Orders.Events;
using CleanArchitecture.Domain.AggregatesModels.Shared;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Orders;

public sealed class Order : BaseEntityRoot
{
    private readonly List<OrderItem> _orderItems = new();

    private Order(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; private set; }

    public UserInformation UserInformation { get; private set; }

    public decimal TotalPrice { get => OrderItems.Sum(o => o.Quantity * o.Price); }

    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public static Order Create(Basket basket, UserInformation userInfomation)
    {
        Order order = new Order(basket.UserId);
        order.UserInformation = userInfomation;

        foreach(var item in basket.BasketProductItems)
        {
            order.AddOrderItem(item.ProductId, item.Quantity, item.Product.Price);
        }

        order.RaiseDomainEvent(new OrderCreatedDomainEvent(order.UserId));

        return order;
    }

    private void AddOrderItem(Guid productId, int quantity, decimal price)
    {
        _orderItems.Add(OrderItem.Create(Id, productId, quantity, price));
    }
}

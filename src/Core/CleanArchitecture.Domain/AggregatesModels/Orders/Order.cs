using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Orders.Enums;
using CleanArchitecture.Domain.AggregatesModels.Shared;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Orders;

public sealed class Order : BaseEntityRoot
{
    private Order(
        Guid id,
        Guid? userId,
        UserInfomation userInfomation,
        List<OrderItem> orderItems) : base(id)
    {
        UserId = userId;
        UserInfomation = userInfomation;
        OrderItems = orderItems;
    }

    public Guid? UserId { get; private set; }

    public UserInfomation UserInfomation { get; private set; }

    public decimal TotalPrice { get => OrderItems.Sum(o => o.Quantity * o.Price); }

    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public List<OrderItem> OrderItems { get; private set; }

    public static Order Create(Basket basket, UserInfomation userInfomation)
    {
        Guid orderId = Guid.NewGuid();

        List<OrderItem> orderItems = basket.BasketProductItems.Select(b =>
            OrderItem.Create(orderId, b.ProductId, b.Quantity, b.Price)).ToList();

        Order order = new Order(
            orderId,
            basket.UserId,
            userInfomation,
            orderItems);

        return order;
    }
}

using CleanArchitecture.Domain.Baskets;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Orders.Enums;
using CleanArchitecture.Domain.Orders.ValueObjects;

namespace CleanArchitecture.Domain.Orders;

public sealed class Order : BaseEntityRoot
{
    private Order(
        Guid id,
        Guid customerId,
        string note,
        Address orderAddress,
        List<OrderItem> orderItems) : base(id)
    {
        CustomerId = customerId;
        OrderAddress = orderAddress;
        Note = note;
        OrderItems = orderItems;
    }

    public Guid CustomerId { get; private set; }

    public decimal TotalPrice { get; private set; }

    public string Note { get; private set; }

    public Address OrderAddress { get; private set; }

    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public List<OrderItem> OrderItems { get; private set; }

    public Order Create(Basket basket, Address orderAdress, string note)
    {
        Guid orderId= Guid.NewGuid();

        List<OrderItem> orderItems = basket.BasketProductItems.Select(b =>
            OrderItem.Create(orderId, b.ProductId, b.Quantity, b.Price)).ToList();

        Order order = new Order(orderId, basket.UserId, note, orderAdress, orderItems);

        return order;
    }
}

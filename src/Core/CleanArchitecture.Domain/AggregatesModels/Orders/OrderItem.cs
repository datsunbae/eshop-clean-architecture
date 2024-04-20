using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Orders;

public sealed class OrderItem : BaseEntity
{
    private OrderItem(
        Guid orderId,
        Guid productId,
        int quantity,
        decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public static OrderItem Create(Guid orderId, Guid productId, int quantity, decimal price)
    {
        OrderItem orderItem = new OrderItem(
            orderId,
            productId,
            quantity,
            price);

        return orderItem;
    }
}

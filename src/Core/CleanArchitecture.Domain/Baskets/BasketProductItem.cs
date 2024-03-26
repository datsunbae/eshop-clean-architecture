using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Baskets;

public class BasketProductItem : BaseEntity
{
    private BasketProductItem(
        Guid id,
        Guid basketId, 
        Guid productId,
        int quantity, 
        decimal price) : base(id)
    {
        BasketId = basketId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Guid BasketId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    internal BasketProductItem Create(
        Basket basket, 
        Guid productId, 
        int quantity, 
        decimal price)
    {
        BasketProductItem basketProductItem = new BasketProductItem(
            Guid.NewGuid(),
            basket.Id,
            productId,
            quantity,
            price);

        return basketProductItem;
    }

    internal BasketProductItem Update(int? quantity)
    {
        if (quantity is not null && Quantity.Equals(quantity) is not true)
            Quantity = quantity.Value;

        return this;
    }

}

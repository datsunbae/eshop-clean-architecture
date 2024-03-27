using CleanArchitecture.Domain.AggregatesModels.Products;
using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.AggregatesModels.Baskets;

public class BasketProductItem : BaseEntity
{
    private BasketProductItem(
        Guid id,
        Guid basketId,
        Guid productId,
        int quantity) : base(id)
    {
        BasketId = basketId;
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid BasketId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public virtual Product Product { get; init; }

    #region NotMapped

    [NotMapped]
    public decimal Price { get => Product.Price; }

    #endregion

    internal static BasketProductItem Create(
        Basket basket,
        Guid productId,
        int quantity)
    {
        BasketProductItem basketProductItem = new BasketProductItem(
            Guid.NewGuid(),
            basket.Id,
            productId,
            quantity);

        return basketProductItem;
    }

    internal BasketProductItem Update(int quantity)
    {
        Quantity = Quantity + quantity;
        return this;
    }

}

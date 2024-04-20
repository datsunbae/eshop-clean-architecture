using CleanArchitecture.Domain.AggregatesModels.Products;
using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.AggregatesModels.Baskets;

public class BasketProductItem : BaseEntity
{
    private BasketProductItem(
        Guid basketId,
        Guid productId,
        int quantity)
    {
        BasketId = basketId;
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid BasketId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public virtual Product? Product { get; init; }

    #region NotMapped

    [NotMapped]
    public string ProductName { get => Product is not null ? Product.Name : string.Empty; }

    [NotMapped]
    public decimal Price { get => Product is not null ? Product.Price : 0; }

    #endregion

    internal static BasketProductItem Create(
        Basket basket,
        Guid productId,
        int quantity)
    {
        BasketProductItem basketProductItem = new BasketProductItem(
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

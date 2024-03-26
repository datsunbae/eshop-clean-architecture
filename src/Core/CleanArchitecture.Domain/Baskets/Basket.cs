using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Baskets;

public sealed class Basket : BaseEntityRoot
{
    private readonly List<BasketProductItem> _bastketProductItems = new();
    private Basket(
        Guid id,
        Guid userId) : base(id)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }

    public decimal TotalPrice { get => BasketProductItems.Sum(b => b.Quantity * b.Price); }

    public IReadOnlyCollection<BasketProductItem> BasketProductItems => _bastketProductItems.AsReadOnly();

    public Basket Create(Guid userId)
    {
        Basket basket = new Basket(Guid.NewGuid(), userId);
        return basket;
    }

    public void AddBasketProductItem(BasketProductItem item)
    {
        var basketProductItem = _bastketProductItems.FirstOrDefault(b => b.ProductId == item.ProductId);
        if (basketProductItem is null)
            _bastketProductItems.Add(item);
    }

    public void UpdateBasketProductItem(BasketProductItem item)
    {
        var basketProductItem = _bastketProductItems.FirstOrDefault(b => b.ProductId == item.ProductId);
        if(basketProductItem is not null)
            basketProductItem.Update(item.Quantity);
    }

    public void RemoveBasketProductItem(BasketProductItem item)
    {
        var basketProductItem = _bastketProductItems.FirstOrDefault(x => x.Id == item.Id);
        if(basketProductItem is not null)
            _bastketProductItems.Remove(basketProductItem);
    }
}

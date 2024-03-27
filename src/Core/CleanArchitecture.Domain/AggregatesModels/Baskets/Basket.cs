﻿using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.AggregatesModels.Baskets;

public sealed class Basket : BaseEntityRoot
{
    private readonly List<BasketProductItem> _bastketProductItems = new();

    private Basket(
        Guid id) : base(id)
    {
    }
    private Basket(
        Guid id,
        Guid userId) : base(id)
    {
        UserId = userId;
    }

    public Guid UserId { get; private set; }

    public IReadOnlyCollection<BasketProductItem> BasketProductItems => _bastketProductItems.AsReadOnly();

    #region NotMapped

    [NotMapped]
    public decimal TotalPrice { get => BasketProductItems.Sum(b => b.Quantity * b.Price); }

    #endregion

    public static Basket Create(Guid userId)
    {
        Basket basket = new Basket(Guid.NewGuid(), userId);
        return basket;
    }

    public static Basket Create()
    {
        Basket basket = new Basket(Guid.NewGuid());
        return basket;
    }

    public void AddBasketProductItem(Guid productId, int quantity)
    {
        var basketProductItem = _bastketProductItems.FirstOrDefault(b => b.ProductId == productId);
        if (basketProductItem is null)
            _bastketProductItems.Add(BasketProductItem.Create(this, productId, quantity));
        else
            basketProductItem.Update(quantity);
    }

    public void RemoveBasketProductItem(Guid productId)
    {
        var basketProductItem = _bastketProductItems.FirstOrDefault(x => x.ProductId == productId);
        if (basketProductItem is not null)
            _bastketProductItems.Remove(basketProductItem);
    }

    public void Clear()
    {
        _bastketProductItems.Clear();
    }
}

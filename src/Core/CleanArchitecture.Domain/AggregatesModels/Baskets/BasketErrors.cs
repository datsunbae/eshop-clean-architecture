using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Baskets;

public static class BasketErrors
{
    public static readonly Error NotFound = new(
        "Basket.NotFound",
        "Basket not found!");

    public static Error BasketProductItemNotFound = new(
        "BasketProductItem.NotFound",
        "Basket product item not found!");

    public static Error BasketProductItemEmpty = new(
        "BasketProductItem.Empty",
        "Basket product item is empty!");
}

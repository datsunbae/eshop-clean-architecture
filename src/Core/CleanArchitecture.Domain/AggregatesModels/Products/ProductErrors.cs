using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Products;

public static class ProductErrors
{
    public static Error NotFound = new(
        "Product.NotFound",
        "Product not found!");
}

using Ardalis.Specification;
using CleanArchitecture.Domain.AggregatesModels.Baskets;

namespace CleanArchitecture.Application.Features.V1.Baskets.Specs;

public sealed class BasketByUserIdWithBasketItemAndProductSpec : Specification<Basket>
{
    public BasketByUserIdWithBasketItemAndProductSpec(Guid userId) =>
        Query
            .Where(b => b.UserId == userId)
            .Include(b => b.BasketProductItems)
            .ThenInclude(p => p.Product);
}

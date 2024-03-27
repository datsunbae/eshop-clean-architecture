using Ardalis.Specification;
using CleanArchitecture.Domain.AggregatesModels.Baskets;

namespace CleanArchitecture.Application.Features.V1.Baskets.Specs;

public class BasketByUserIdSpec : Specification<Basket>
{
    public BasketByUserIdSpec(Guid userId) =>
        Query
            .Where(b => b.UserId == userId)
            .Include(b => b.BasketProductItems);
}

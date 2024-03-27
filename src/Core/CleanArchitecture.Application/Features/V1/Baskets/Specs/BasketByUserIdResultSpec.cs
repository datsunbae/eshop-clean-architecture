using Ardalis.Specification;
using CleanArchitecture.Application.Features.V1.Baskets.Dtos;
using CleanArchitecture.Domain.AggregatesModels.Baskets;

namespace CleanArchitecture.Application.Features.V1.Baskets.Specs;

public sealed class BasketByUserIdResultSpec : Specification<Basket, BasketReponse>
{
    public BasketByUserIdResultSpec(Guid userId) =>
        Query
            .Where(b => b.UserId == userId)
            .Include(b => b.BasketProductItems)
            .ThenInclude(p => p.Product);
}

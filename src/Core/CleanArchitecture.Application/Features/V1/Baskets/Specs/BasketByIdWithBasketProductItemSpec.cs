using Ardalis.Specification;
using CleanArchitecture.Application.Features.V1.Baskets.Dtos;
using CleanArchitecture.Domain.AggregatesModels.Baskets;

namespace CleanArchitecture.Application.Features.V1.Baskets.Specs;

public sealed class BasketByIdWithBasketProductItemSpec : Specification<Basket, BasketReponse>
{
    public BasketByIdWithBasketProductItemSpec(Guid id) =>
        Query
            .Where(b => b.Id == id)
            .Include(b => b.BasketProductItems);
}

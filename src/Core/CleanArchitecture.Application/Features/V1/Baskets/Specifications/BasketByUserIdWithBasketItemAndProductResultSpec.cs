using Ardalis.Specification;
using CleanArchitecture.Application.Features.V1.Baskets.Models.Responses;
using CleanArchitecture.Domain.AggregatesModels.Baskets;

namespace CleanArchitecture.Application.Features.V1.Baskets.Specifications;

public class BasketByUserIdWithBasketItemAndProductResultSpec : Specification<Basket, BasketResponse>
{
    public BasketByUserIdWithBasketItemAndProductResultSpec(Guid userId) =>
        Query
            .Where(b => b.UserId == userId)
            .Include(b => b.BasketProductItems)
            .ThenInclude(p => p.Product);
}

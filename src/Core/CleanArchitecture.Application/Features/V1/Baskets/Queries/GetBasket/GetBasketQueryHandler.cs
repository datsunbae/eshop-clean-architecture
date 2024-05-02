using Ardalis.Specification;
using CleanArchitecture.Application.Common.ApplicationServices.Auth;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Baskets.Models.Responses;
using CleanArchitecture.Application.Features.V1.Baskets.Specifications;
using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Baskets.Queries.GetBasket;

public sealed class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, BasketResponse>
{
    private readonly IBasketRepository _basketRepository;
    private readonly ICurrentUser _currentUser;
    public GetBasketQueryHandler(IBasketRepository basketRepository, ICurrentUser currentUser)
    {
        _basketRepository = basketRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<BasketResponse>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        Guid userId = _currentUser.GetUserId();

        var result = await _basketRepository
            .FirstOrDefaultAsync(
                (ISpecification<Basket, BasketResponse>)new BasketByUserIdWithBasketItemAndProductResultSpec(userId), cancellationToken)
                    ?? new BasketResponse(userId, 0, Enumerable.Empty<BasketProductItemResponse>());

        return result;
    }
}

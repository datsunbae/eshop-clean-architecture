using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Baskets.Models.Responses;
using CleanArchitecture.Application.Features.V1.Baskets.Specifications;
using CleanArchitecture.Domain.AggregatesModels.Baskets.Repository;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Baskets.Queries.GetBasket;

public sealed class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, BasketReponse>
{
    private readonly IBasketRepository _basketRepository;
    public GetBasketQueryHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<Result<BasketReponse>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        return await _basketRepository.FirstOrDefaultAsync(new BasketByUserIdWithBasketItemResultSpec(request.UserId)); ;
    }
}

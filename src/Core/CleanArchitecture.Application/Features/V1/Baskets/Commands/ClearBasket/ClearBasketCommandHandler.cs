using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Persistence;
using CleanArchitecture.Application.Features.V1.Baskets.Specs;
using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Baskets.Repository;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.ClearBasket;

public sealed class ClearBasketCommandHandler : ICommandHandler<ClearBasketCommand, Guid>
{
    private readonly IBasketRepository _basketRepository;
    private readonly ICurrentUser _currentUser;

    public ClearBasketCommandHandler(IBasketRepository basketRepository, ICurrentUser currentUser)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<Result<Guid>> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
    {
        Basket basket = await _basketRepository.FirstOrDefaultAsync(new BasketByUserIdWithBasketItemSpec(_currentUser.GetUserId()));

        if(basket is null)
            return Result.Failure<Guid>(BasketErrors.NotFound);

        basket.Clear();

        return basket.Id;
    }
}
    
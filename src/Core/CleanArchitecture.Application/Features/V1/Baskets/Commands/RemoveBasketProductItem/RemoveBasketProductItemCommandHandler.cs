using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Baskets.Specs;
using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Baskets.Repository;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.RemoveBasketProductItem;

public class RemoveBasketProductItemCommandHandler : ICommandHandler<RemoveBasketProductItemCommand, Guid>
{
    private readonly IBasketRepository _basketRepository;
    private readonly ICurrentUser _currentUser;
    public RemoveBasketProductItemCommandHandler(
        IBasketRepository basketRepository, 
        ICurrentUser currentUser)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<Result<Guid>> Handle(RemoveBasketProductItemCommand request, CancellationToken cancellationToken)
    {
        Basket basket = await _basketRepository.FirstOrDefaultAsync(new BasketByUserIdWithBasketItemSpec(_currentUser.GetUserId()));

        if(basket is null)
            return Result.Failure<Guid>(BasketErrors.NotFound);

        BasketProductItem basketProductItem = basket.BasketProductItems
            .FirstOrDefault(b => b.ProductId == request.ProductId);

        if (basketProductItem is null)
            return Result.Failure<Guid>(BasketErrors.BasketProductItemNotFound);

        basket.RemoveBasketProductItem(request.ProductId);

        return basket.Id;
    }
}

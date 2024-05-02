using CleanArchitecture.Application.Common.ApplicationServices.Auth;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Baskets.Specs;
using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Orders;
using CleanArchitecture.Domain.AggregatesModels.Shared;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Services.Checkout;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.CheckoutBasket;

public class CheckoutCommandHandler : ICommandHandler<CheckoutCommand, Guid>
{
    private readonly CheckoutService _checkoutService;
    private readonly IBasketRepository _basketRepository;
    private readonly ICurrentUser _currentUser;
    public CheckoutCommandHandler(CheckoutService checkoutService, IBasketRepository basketRepository, ICurrentUser currentUser)
    {
        _checkoutService = checkoutService ?? throw new ArgumentNullException(nameof(checkoutService));
        _basketRepository = basketRepository ?? throw new ArgumentException(nameof(basketRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<Result<Guid>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _currentUser.GetUserId();

        UserInformation userInfomation = new UserInformation(
            request.UserInformation.Name,
            new Phone(request.UserInformation.Phone),
            new Address(request.UserInformation.Address.Street, request.UserInformation.Address.City));

        Basket basket = await _basketRepository.FirstOrDefaultAsync(new BasketByUserIdWithBasketItemAndProductSpec(userId));

        if (basket is null || !basket.BasketProductItems.Any())
            return Result.Failure<Guid>(BasketErrors.BasketProductItemEmpty);

        Order result = await _checkoutService.Checkout(basket, userInfomation);

        return result.Id;
    }
}

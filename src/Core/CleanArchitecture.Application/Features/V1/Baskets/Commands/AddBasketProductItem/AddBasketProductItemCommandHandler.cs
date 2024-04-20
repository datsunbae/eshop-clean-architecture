using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Baskets.Specs;
using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Baskets.Repository;
using CleanArchitecture.Domain.AggregatesModels.Products;
using CleanArchitecture.Domain.AggregatesModels.Products.Repository;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.AddBasketProductItem;

public sealed class AddBasketProductItemCommandHandler : ICommandHandler<AddBasketProductItemCommand, Guid>
{
    private readonly IBasketRepository _basketRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IProductRepository _productRepository;

    public AddBasketProductItemCommandHandler(
        IBasketRepository basketRepository,
        ICurrentUser currentUser,
        IProductRepository productRepository)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<Guid>> Handle(AddBasketProductItemCommand request, CancellationToken cancellationToken)
    {
        Guid result = Guid.Empty;
        Guid userId = _currentUser.GetUserId();
        if (userId.Equals(Guid.Empty) is true)
            throw new UnauthorizedException("Authentication Failed.");

        Product product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
            return Result.Failure<Guid>(ProductErrors.NotFound);

        Basket basket = await _basketRepository.FirstOrDefaultAsync(new BasketByUserIdWithBasketItemSpec(userId), cancellationToken);
        if (basket is null)
        {
            Basket newBasket = Basket.Create(userId);
            newBasket.AddBasketProductItem(request.ProductId, request.Quantity);
            await _basketRepository.AddAsync(newBasket);

            result = newBasket.Id;
        }
        else
        {
            basket.AddBasketProductItem(request.ProductId, request.Quantity);
            await _basketRepository.UpdateAsync(basket, cancellationToken);

            result = basket.Id;
        }

        return result;
    }
}

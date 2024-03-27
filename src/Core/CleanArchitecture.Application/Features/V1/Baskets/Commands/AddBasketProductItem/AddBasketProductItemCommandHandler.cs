using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.Identities.Users;
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
    private readonly IUserService _userServices;
    private readonly IProductRepository _productRepository;

    public AddBasketProductItemCommandHandler(
        IBasketRepository basketRepository,
        IUserService userService,
        IProductRepository productRepository)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        _userServices = userService ?? throw new ArgumentNullException(nameof(userService));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<Guid>> Handle(AddBasketProductItemCommand request, CancellationToken cancellationToken)
    {
        var user = await _userServices.GetAsync(request.UserId, cancellationToken);
        if (user is null)
            throw new BadRequestException($"User with Id = {request.UserId} is not found!");

        Product product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
            return Result.Failure<Guid>(ProductErrors.NotFound);

        Basket basket = await _basketRepository.FirstOrDefaultAsync(new BasketByUserIdSpec(user.Id));
        if (basket is null)
        {
            Basket newBasket = Basket.Create(user.Id);
            newBasket.AddBasketProductItem(request.ProductId, request.Quantity);
            await _basketRepository.AddAsync(basket);
        }
        else
        {
            basket.AddBasketProductItem(request.ProductId, request.Quantity);
            await _basketRepository.UpdateAsync(basket);
        }

        return basket.Id;
    }
}

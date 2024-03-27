using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.AddBasketProductItem;

public sealed record AddBasketProductItemCommand(
    Guid ProductId,
    int Quantity
    ) : ICommand<Guid>;

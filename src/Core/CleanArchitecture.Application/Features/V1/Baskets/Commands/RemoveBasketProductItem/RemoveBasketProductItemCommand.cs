using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.RemoveBasketProductItem;

public sealed record RemoveBasketProductItemCommand(Guid ProductId) : ICommand<Guid>;

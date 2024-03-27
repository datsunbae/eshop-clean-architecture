using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.ClearBasket;

public sealed record ClearBasketCommand(
    Guid UserId) : ICommand<Guid>;

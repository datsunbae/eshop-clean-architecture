namespace CleanArchitecture.Application.Features.V1.Baskets.Dtos;

public sealed record BasketProductItemRequest(
    Guid productId,
    int Quantity);

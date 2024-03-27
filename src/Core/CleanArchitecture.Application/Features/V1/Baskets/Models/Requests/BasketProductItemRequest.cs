namespace CleanArchitecture.Application.Features.V1.Baskets.Models.Requests;

public sealed record BasketProductItemRequest(
    Guid ProductId,
    int Quantity);

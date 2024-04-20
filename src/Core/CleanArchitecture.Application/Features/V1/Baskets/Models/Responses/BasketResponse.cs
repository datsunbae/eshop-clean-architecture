namespace CleanArchitecture.Application.Features.V1.Baskets.Models.Responses;

public sealed record BasketResponse
    (Guid UserId, 
    decimal TotalPrice, 
    IEnumerable<BasketProductItemResponse> BasketProductItems);
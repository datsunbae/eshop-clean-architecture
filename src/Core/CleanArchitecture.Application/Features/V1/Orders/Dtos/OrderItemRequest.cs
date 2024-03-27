namespace CleanArchitecture.Application.Features.V1.Orders.Dtos;

public sealed record OrderItemRequest(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal Price);

namespace CleanArchitecture.Application.Features.V1.Orders.Dtos;

public sealed record OrderItemResponse(
    Guid Id,
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal Price);

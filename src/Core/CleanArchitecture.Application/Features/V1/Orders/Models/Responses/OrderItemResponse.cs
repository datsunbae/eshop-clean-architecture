namespace CleanArchitecture.Application.Features.V1.Orders.Models.Responses;

public sealed record OrderItemResponse(
    Guid Id,
    Guid OrderId,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal Price);

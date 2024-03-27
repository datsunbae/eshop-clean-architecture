﻿using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Features.V1.Baskets.Dtos;

public sealed class BasketProductItemResponse : IResponse
{
    public Guid Id { get; init; }
    public Guid BasketId { get; init; }
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}

using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Baskets.Dtos;

namespace CleanArchitecture.Application.Features.V1.Baskets.Queries.GetBasket;

public sealed record GetBasketQuery(
    Guid UserId) : IQuery<BasketReponse>;

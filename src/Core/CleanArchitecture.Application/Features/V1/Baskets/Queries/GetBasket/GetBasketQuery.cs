using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Baskets.Models.Responses;

namespace CleanArchitecture.Application.Features.V1.Baskets.Queries.GetBasket;

public sealed record GetBasketQuery : IQuery<BasketResponse>;

using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Baskets.Models.Requests;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.CheckoutBasket;

public sealed record CheckoutCommand(UserInformationRequest UserInformation) : ICommand<Guid>;

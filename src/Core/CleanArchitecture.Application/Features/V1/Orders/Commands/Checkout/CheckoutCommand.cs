using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Orders.Dtos;

namespace CleanArchitecture.Application.Features.V1.Orders.Commands.Checkout;

public sealed record CheckoutCommand(
    Guid? UserId,
    string UserName,
    string Phone,
    AddressDto AddressOrder,
    string Note) : ICommand<Guid>;


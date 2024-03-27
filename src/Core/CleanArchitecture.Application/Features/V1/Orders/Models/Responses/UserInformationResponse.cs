namespace CleanArchitecture.Application.Features.V1.Orders.Models.Responses;

public sealed record UserInformationResponse(
    string UserName,
    string Phone,
    AddressResponse OrderAddress);

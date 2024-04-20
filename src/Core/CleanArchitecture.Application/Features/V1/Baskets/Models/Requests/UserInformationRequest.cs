namespace CleanArchitecture.Application.Features.V1.Baskets.Models.Requests;

public sealed record UserInformationRequest(
    string Name,
    string Phone,
    AddressRequest Address);

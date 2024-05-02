using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.AggregatesModels.Orders.Enums;

namespace CleanArchitecture.Application.Features.V1.Orders.Models.Responses;

public sealed class OrderResponse : IAuditResponse
{
    public Guid Id { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
    public Guid LastModifiedBy { get; init; }
    public DateTime? LastModifiedOn { get; init; }
    public DateTime? DeletedOn { get; init; }
    public Guid? DeletedBy { get; init; }

    public Guid UserId { get; init; }
    public UserInformationResponse UserInformation { get; init; }
    public OrderStatus Status { get; init; }
    public decimal TotalPrice { get; init; }
    public List<OrderItemResponse> OrderItems { get; init; }
}

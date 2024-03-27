using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.AggregatesModels.Orders.Enums;

namespace CleanArchitecture.Application.Features.V1.Orders.Dtos;

public sealed class OrderResponse : IAuditResponse
{
    public Guid Id { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
    public Guid LastModifiedBy { get; init; }
    public DateTime? LastModifiedOn { get; init; }
    public DateTime? DeletedOn { get; init; }
    public Guid? DeletedBy { get; init; }

    public Guid CustomerId { get; init; }
    public AddressDto OrderAddress { get; init; }
    public OrderStatus Status { get; init; }
    public decimal TotalPrice { get; init; }
    public string Note { get; init; }
    public string UserName { get; init; }
    public string Phone { get; init; }
    public List<OrderItemResponse> OrderItems { get; init; }
}

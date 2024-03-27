using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Features.V1.Baskets.Dtos;

public sealed class BasketReponse : IAuditResponse
{
    public Guid Id { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
    public Guid LastModifiedBy { get; init; }
    public DateTime? LastModifiedOn { get; init; }
    public DateTime? DeletedOn { get; init; }
    public Guid? DeletedBy { get; init; }
    public Guid UserId { get; init; }
    public decimal TotalPrice { get; init; }
    public IEnumerable<BasketProductItemResponse> BasketProductItems { get; init; }
}

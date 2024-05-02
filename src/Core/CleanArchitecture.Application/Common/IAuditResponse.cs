namespace CleanArchitecture.Application.Common;

public interface IAuditResponse : IResponse
{
    public Guid CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
    public Guid LastModifiedBy { get; init; }
    public DateTime? LastModifiedOn { get; init; }
    public DateTime? DeletedOn { get; init; }
    public Guid? DeletedBy { get; init; }
}

namespace CleanArchitecture.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public Guid CreatedBy { get; protected set; } 
    public DateTime CreatedOn { get; protected set; }
    public Guid LastModifiedBy { get; protected set; }
    public DateTime? LastModifiedOn { get; protected set; }
    public DateTime? DeletedOn { get; protected set; }
    public Guid? DeletedBy { get; protected set; }
}

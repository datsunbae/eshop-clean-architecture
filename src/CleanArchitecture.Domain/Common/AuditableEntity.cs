namespace CleanArchitecture.Domain.Common;

public abstract class AuditableEntity : AuditableEntity<DefaultIdType>
{
}

public abstract class AuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity, ISoftDelete
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; private set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public Guid? DeletedBy { get; set; }

    protected AuditableEntity()
    {
        CreatedOn = DateTime.UtcNow;
    }
}

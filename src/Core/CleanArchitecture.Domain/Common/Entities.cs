namespace CleanArchitecture.Domain.Common;

public interface IAggregateRoot
{
}

public abstract class BaseEntity : IAuditableEntity, ISoftDelete
{
    public Guid Id { get; protected set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public Guid? DeletedBy { get; set; }

    protected BaseEntity(Guid id)
    {
        Id = id;
    }
}

public abstract class BaseEntityRoot : BaseEntity, IAggregateRoot
{
    protected BaseEntityRoot(Guid id) : base(id)
    {
    }

    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}

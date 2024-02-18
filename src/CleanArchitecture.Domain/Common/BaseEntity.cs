namespace CleanArchitecture.Domain.Common;

public abstract class BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    protected BaseEntity()
    {
    }

    public Guid Id { get; init; }
    public Guid CreatedBy { get; protected set; }
    public DateTime Created { get; protected init; } = DateTime.UtcNow;
    public Guid LastModifiedBy { get; protected set; }
    public DateTime? LastModified { get; protected set; }
    public bool IsDeleted { get; protected set; } = false;

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

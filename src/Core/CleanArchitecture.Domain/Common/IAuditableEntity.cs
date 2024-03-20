namespace CleanArchitecture.Domain.Common;

public interface IAuditableEntity : ISoftDelete
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}

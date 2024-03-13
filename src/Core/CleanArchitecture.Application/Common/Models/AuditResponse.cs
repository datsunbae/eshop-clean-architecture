using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Common.Models;

public abstract class AuditResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public Guid? DeletedBy { get; set; }
}

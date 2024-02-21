namespace CleanArchitecture.Domain.Common;

internal interface ISoftDelete
{
    DateTime? DeletedOn { get; set; }
    Guid? DeletedBy { get; set; }
}

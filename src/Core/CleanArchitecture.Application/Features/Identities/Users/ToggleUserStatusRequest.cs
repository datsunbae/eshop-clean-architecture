namespace CleanArchitecture.Application.Features.Identities.Users;

public class ToggleUserStatusRequest
{
    public bool IsActivateUser { get; set; }
    public Guid? UserId { get; set; }
}

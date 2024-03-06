namespace CleanArchitecture.Application.Common.Interfaces;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }

    Guid GetUserId();
}

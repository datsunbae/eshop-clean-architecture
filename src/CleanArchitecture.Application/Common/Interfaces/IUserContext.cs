namespace CleanArchitecture.Application.Common.Interfaces;

public interface IUserContext
{
    bool IsAuthenticated { get; }

    Guid UserId { get; }
}

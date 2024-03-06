using CleanArchitecture.Application.Identity.Users.Password;
using System.Security.Claims;

namespace CleanArchitecture.Application.Identity.Users;

public interface IUserService
{
    Task<bool> ExistsWithNameAsync(string name);
    Task<bool> ExistsWithEmailAsync(string email, Guid? exceptId = null);
    Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, Guid? exceptId = null);

    Task<List<UserDto>> GetListAsync(CancellationToken cancellationToken);

    Task<int> GetCountAsync(CancellationToken cancellationToken);

    Task<UserDto> GetAsync(Guid userId, CancellationToken cancellationToken);

    Task<List<UserRoleDto>> GetRolesAsync(Guid userId, CancellationToken cancellationToken);
    Task<string> AssignRolesAsync(Guid userId, UserRolesRequest request, CancellationToken cancellationToken);

    Task<List<string>> GetPermissionsAsync(Guid userId, CancellationToken cancellationToken);
    Task<bool> HasPermissionAsync(Guid userId, string permission, CancellationToken cancellationToken = default);
    Task InvalidatePermissionCacheAsync(Guid userId, CancellationToken cancellationToken);

    Task ToggleStatusAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken);

    Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal);
    Task<string> CreateAsync(CreateUserRequest request, string origin);
    Task UpdateAsync(UpdateUserRequest request, Guid userId);

    Task<string> ConfirmEmailAsync(Guid userId, string code, CancellationToken cancellationToken);
    Task<string> ConfirmPhoneNumberAsync(Guid userId, string code);

    Task<string> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
    Task<string> ResetPasswordAsync(ResetPasswordRequest request);
    Task ChangePasswordAsync(ChangePasswordRequest request, Guid userId);
}
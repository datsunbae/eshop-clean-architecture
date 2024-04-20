using CleanArchitecture.Application.Features.Identities.Users;
using CleanArchitecture.Application.Features.Identities.Users.Password;
using CleanArchitecture.Identity.Auth.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Identity;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    [MustHavePermission(Action.View, Resource.Users)]
    public Task<List<UserResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        return _userService.GetListAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    [MustHavePermission(Action.View, Resource.Users)]
    public Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _userService.GetAsync(id, cancellationToken);
    }

    [HttpGet("{id}/roles")]
    [MustHavePermission(Action.View, Resource.UserRoles)]
    public Task<List<UserRoleResponse>> GetRolesAsync(Guid id, CancellationToken cancellationToken)
    {
        return _userService.GetRolesAsync(id, cancellationToken);
    }

    [HttpPost("{id}/roles")]
    [MustHavePermission(Action.Update, Resource.UserRoles)]
    public Task<string> AssignRolesAsync(Guid id, UserRolesRequest request, CancellationToken cancellationToken)
    {
        return _userService.AssignRolesAsync(id, request, cancellationToken);
    }

    [HttpPost]
    [MustHavePermission(Action.Create, Resource.Users)]
    public Task<string> CreateAsync(CreateUserRequest request)
    {
        // TODO: check if registering anonymous users is actually allowed (should probably be an appsetting)
        // and return UnAuthorized when it isn't
        // Also: add other protection to prevent automatic posting (captcha?)
        return _userService.CreateAsync(request, GetOriginFromRequest());
    }

    [HttpPost("self-register")]
    [AllowAnonymous]
    public Task<string> SelfRegisterAsync(CreateUserRequest request)
    {
        // TODO: check if registering anonymous users is actually allowed (should probably be an appsetting)
        // and return UnAuthorized when it isn't
        // Also: add other protection to prevent automatic posting (captcha?)
        return _userService.CreateAsync(request, GetOriginFromRequest());
    }

    [HttpPost("{id}/toggle-status")]
    [MustHavePermission(Action.Update, Resource.Users)]
    public async Task<ActionResult> ToggleStatusAsync(Guid id, ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        if (id != request.UserId)
        {
            return BadRequest();
        }

        await _userService.ToggleStatusAsync(request, cancellationToken);
        return Ok();
    }

    [HttpGet("confirm-email")]
    [AllowAnonymous]
    public Task<string> ConfirmEmailAsync([FromQuery] Guid userId, [FromQuery] string code, CancellationToken cancellationToken)
    {
        return _userService.ConfirmEmailAsync(userId, code, cancellationToken);
    }

    //[HttpGet("confirm-phone-number")]
    //[AllowAnonymous]
    //public Task<string> ConfirmPhoneNumberAsync([FromQuery] Guid userId, [FromQuery] string code)
    //{
    //    return _userService.ConfirmPhoneNumberAsync(userId, code);
    //}

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public Task<string> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        return _userService.ForgotPasswordAsync(request, GetOriginFromRequest());
    }

    [HttpPost("reset-password")]
    public Task<string> ResetPasswordAsync(ResetPasswordRequest request)
    {
        return _userService.ResetPasswordAsync(request);
    }

    private string GetOriginFromRequest() => $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
}

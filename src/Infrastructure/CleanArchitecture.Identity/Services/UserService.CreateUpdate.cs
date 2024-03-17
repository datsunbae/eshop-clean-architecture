using CleanArchitecture.Application.Common.Email;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Features.Identities.Users;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Constants.Authorization;
using CleanArchitecture.Identity.Entities;
using CleanArchitecture.Identity.Events;
using CleanArchitecture.Identity.Extensions;
using CleanArchitecture.Identity.Models;
using FluentValidation;

namespace CleanArchitecture.Identity.Services;

internal partial class UserService
{
    public async Task<string> CreateAsync(CreateUserRequest request, string origin)
    {
        await new CreateUserRequestValidator(this)
            .ValidateAndThrowAsync(request);

        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new InternalServerException("Validation Errors Occurred.", result.GetErrors());
        }

        await _userManager.AddToRoleAsync(user, Roles.Customer);

        var messages = new List<string> { string.Format("User {0} Registered.", user.UserName) };

        if (!string.IsNullOrEmpty(user.Email))
        {
            // send verification email
            string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
            RegisterUserEmailModel eMailModel = new RegisterUserEmailModel()
            {
                Email = user.Email,
                UserName = user.UserName,
                Url = emailVerificationUri
            };
            var mailRequest = new MailRequest(
                new List<string> { user.Email },
                "Confirm Registration",
                _templateService.GenerateEmailTemplate("email-confirmation", eMailModel));
            _jobService.Enqueue(() => _mailService.SendAsync(mailRequest, CancellationToken.None));
            messages.Add($"Please check {user.Email} to verify your account!");
        }

        return string.Join(Environment.NewLine, messages);
    }

    public async Task UpdateAsync(UpdateUserRequest request, Guid userId)
    {
        await new UpdateUserRequestValidator(this)
            .ValidateAndThrowAsync(request);

        var user = await _userManager.FindByIdAsync(userId.ToString());

        _ = user ?? throw new NotFoundException("User Not Found.");

        string currentImage = user.ImageUrl ?? string.Empty;
        if (request.Image != null || request.IsDeleteCurrentImage)
        {
            user.ImageUrl = await _fileStorage.UploadAsync<ApplicationUser>(request.Image, FileType.Image);
            if (request.IsDeleteCurrentImage && !string.IsNullOrEmpty(currentImage))
            {
                string root = Directory.GetCurrentDirectory();
                _fileStorage.Remove(Path.Combine(root, currentImage));
            }
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        string? phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (request.PhoneNumber != phoneNumber)
        {
            await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
        }

        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        await _mediator.Publish(new ApplicationUserUpdatedEvent(user.Id));

        if (!result.Succeeded)
        {
            throw new InternalServerException("Update profile failed", result.GetErrors());
        }
    }
}

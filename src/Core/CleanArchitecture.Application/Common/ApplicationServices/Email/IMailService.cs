namespace CleanArchitecture.Application.Common.ApplicationServices.Email;

public interface IMailService
{
    Task SendAsync(MailRequest request, CancellationToken ct);
}
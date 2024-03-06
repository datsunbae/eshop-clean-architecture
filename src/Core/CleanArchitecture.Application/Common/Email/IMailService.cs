namespace CleanArchitecture.Application.Common.Email;

public interface IMailService
{
    Task SendAsync(MailRequest request, CancellationToken ct);
}
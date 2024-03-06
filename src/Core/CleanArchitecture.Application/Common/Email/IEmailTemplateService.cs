namespace CleanArchitecture.Application.Common.Email;

public interface IEmailTemplateService
{
    string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel);
}
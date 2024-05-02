namespace CleanArchitecture.Application.Common.ApplicationServices.Email;

public interface IEmailTemplateService
{
    string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel);
}
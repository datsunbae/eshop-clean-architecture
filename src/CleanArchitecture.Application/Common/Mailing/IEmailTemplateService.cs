namespace CleanArchitecture.Application.Common.Mailing;

public interface IEmailTemplateService
{
    string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel);
}
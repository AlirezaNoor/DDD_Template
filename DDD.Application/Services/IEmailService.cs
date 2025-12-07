namespace DDD.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
    Task SendTemplateEmailAsync(string to, string templateName, object data, CancellationToken cancellationToken = default);
}
using SendEmail.Domain.Dtos;

namespace SendEmail.Domain.Services;

public interface ISendEmailService
{
    Task SendEmailAsync(EmailDto dto);
}

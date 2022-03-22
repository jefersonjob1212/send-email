using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using SendEmail.Domain.Configurations;
using SendEmail.Domain.Dtos;
using SendEmail.Domain.Services;

namespace SendEmail.Services;

public class SendEmailService : ISendEmailService
{
    private readonly EmailConfiguration _emailConfiguration;

    public SendEmailService(EmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }

    public async Task SendEmailAsync(EmailDto dto)
    {
        var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            await smtp.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);
            var mimeMessage = CreateEmailMessage(dto);
            await smtp.SendAsync(mimeMessage);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            smtp.Disconnect(true);
            smtp.Dispose();
        }
    }

    private MimeMessage CreateEmailMessage(EmailDto dto)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_emailConfiguration.From));
        message.To.Add(new MailboxAddress(dto.ToEmail));
        message.Subject = dto.Subject;
        message.Body = new TextPart(TextFormat.Html)
        {
            Text = CreateEmailBody(dto)
        };
        return message;
    }

    private string CreateEmailBody(EmailDto dto)
    {
        var body = string.Empty;
        if (dto != null)
        {
            using (StreamReader reader = new StreamReader("Template/emailTemplate.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", dto.ToName);
            body = body.Replace("{message}", dto.Message);
        }
        return body;
    }
}

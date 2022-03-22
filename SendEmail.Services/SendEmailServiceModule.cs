using Microsoft.Extensions.DependencyInjection;
using SendEmail.Domain.Services;

namespace SendEmail.Services;

public static class SendEmailServiceModule
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services.AddTransient<ISendEmailService, SendEmailService>();
    }
}

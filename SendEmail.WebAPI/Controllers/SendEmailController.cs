using Microsoft.AspNetCore.Mvc;
using SendEmail.Domain.Dtos;
using SendEmail.Domain.Services;

namespace SendEmail.WebAPI.Controllers
{
    [Route("api/email")]
    public class SendEmailController : Controller
    {
        private readonly ISendEmailService _sendEmailService;

        public SendEmailController(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        [HttpPost]
        [Route("send")]
        public async Task SendEmail([FromBody] EmailDto dto)
        {
            await _sendEmailService.SendEmailAsync(dto);
        }
    }
}

using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("SendMail")]
public class MailSendController(MailSendService mailSendService)
{
    [HttpPost]
    public async Task Send([FromBody] EmailModel emailModel)
    {
        await mailSendService.SendEmail(emailModel);
    }
}
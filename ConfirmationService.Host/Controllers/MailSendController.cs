using ConfirmationService.BusinessLogic;
using ConfirmationService.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("SendMail")]
public class MailSendController
{
    private readonly MailSendService _mailSendService;

    public MailSendController(MailSendService mailSendService)
    {
        _mailSendService = mailSendService;
    }

    [HttpPost]
    public async Task Send([FromBody] EmailModel emailModel)
    {
        await _mailSendService.SendEmail(emailModel);
    }
}
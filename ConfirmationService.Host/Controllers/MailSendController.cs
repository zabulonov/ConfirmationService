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
    public void Send([FromBody] EmailModel emailModel)
    {
        _mailSendService.SendEmail(emailModel);
    }
}
using ConfirmationService.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("email-confirmation")]
public class MailСonfirmController(MailConfirmService confirmService)
{
    [HttpGet("confirm")]
    public async Task<string> Confirmation(Guid token)
    {
        await confirmService.ConfirmMail(token);
        return $"email confirmed!";
    }
}
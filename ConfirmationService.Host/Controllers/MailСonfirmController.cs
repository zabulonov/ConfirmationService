using ConfirmationService.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("email-confirmation")]
public class MailСonfirmController
{
    private readonly MailConfirmService _confirmService;

    public MailСonfirmController(MailConfirmService confirmService)
    {
        _confirmService = confirmService;
    }

    [HttpGet("confirm")]
    public async Task<string> Confirmation(Guid token)
    {
        await _confirmService.ConfirmMail(token);
        return $"email confirmed!";
    }
}
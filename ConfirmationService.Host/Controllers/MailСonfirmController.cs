using ConfirmationService.BusinessLogic.Services;
using ConfirmationService.Host;
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
    public string Confirmation(Guid token)
    {
        _confirmService.ConfirmMail(token);
        return $"email confirmed!";
    }
    
    [HttpPost("new")]
    public void NewConfirmation()
    {
        
    }
}
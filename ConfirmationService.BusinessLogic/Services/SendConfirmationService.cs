using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.Core.Entity;
using ConfirmationService.Infrastructure.EntityFramework;

namespace ConfirmationService.BusinessLogic.Services;

public class SendConfirmationService
{
    private readonly MailSendService _mailSendService;
    private readonly UserService _userService;
    private readonly ConfirmServiceContext _confirmServiceContext;

    public SendConfirmationService(MailSendService mailSendService, UserService userService, ConfirmServiceContext confirmServiceContext)
    {
        _mailSendService = mailSendService;
        _userService = userService;
        _confirmServiceContext = confirmServiceContext;
    }

    public async Task CreateUserOfClient(UserClientModel userClientModel)
    {
        await _userService.AddClientToUser(userClientModel);
    }
}
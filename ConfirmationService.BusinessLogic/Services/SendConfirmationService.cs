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

    public async Task<Guid> CreateUserOfClient(ClientOfUserModel clientModel)
    {
        var user =  _userService.CheckToken(clientModel.UserToken).Result;
        var newClient = new ClientOfUser(clientModel.Name, clientModel.Email, user.Id);
        var token = newClient.ConfirmToken;
        await _confirmServiceContext.AddClient(newClient);
        return token;
    }
}
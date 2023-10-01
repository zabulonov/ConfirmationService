using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

/*
 * 1. зарегать нового юзера
 * 3. отправить confirmation email для клиента юзера
 * 4. обработать переход по подтверждающей ссылке
 * 5. получить информацию о своих юзере(ах) 
 */
[ApiController]
[Route("user")]
public class UserControllerA
{
    private readonly UserService _userService;
    private readonly MailConfirmService _mailConfirmService;
    private readonly SendConfirmationService _sendConfirmationService;

    public UserControllerA(UserService userService, MailConfirmService mailConfirmService, SendConfirmationService sendConfirmationService)
    {
        _userService = userService;
        _mailConfirmService = mailConfirmService;
        _sendConfirmationService = sendConfirmationService;
    }
    
    [HttpPost]
    public async Task<Guid> AddNewUser(string companyName)
    {
        return await _userService.RegisterNewUser(companyName);
    }
    
    [HttpPost("{userToken}/send-confirmation-email")]
    public async Task SendConfirmationEmail(Guid userToken, string clientEmail)
    {
        await _userService.SendConfirmationEmail(userToken, clientEmail);
    }
    
    [HttpPost("CreateAndSendToClient")]
    public async Task CreateAndSendToClient([FromBody] UserClientModel userClient)
    {
        var token = await _sendConfirmationService.CreateUserOfClient(userClient);
        // await _mailSendService.SendEmailToClient(client, token);
    }

    [HttpGet("confirm/{clientToken}")]
    public async Task ConfirmClient(Guid clientToken)
    {
        await _mailConfirmService.ConfirmMail(clientToken);
    }

    [HttpGet("{userToken}/clients")]
    public async Task<List<ClientModel>> GetClients(Guid userToken)
    {
        return await _userService.GetUserClients(userToken);
    }
    
}
using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.Core.Entity;
using ConfirmationService.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ConfirmationService.BusinessLogic.Services;

public class UserService
{
    private readonly ConfirmServiceContext _confirmServiceContext;
    private readonly MailSendService _mailSendService;

    public UserService(ConfirmServiceContext confirmServiceContext, MailSendService mailSendService)
    {
        _confirmServiceContext = confirmServiceContext;
        _mailSendService = mailSendService;
    }

    public async Task<Guid> RegisterNewUser(string companyName)
    {
        var newUser = new User(companyName);
        await _confirmServiceContext.AddUser(newUser);
        return newUser.Token;
    }

    public async Task<UserModel> CheckToken(Guid token)
    {
        var user = await _confirmServiceContext.GetUserByToken(token);
        if (user == null)
        {
            throw new Exception("Token isn't valid.");    
        }
        
        return new UserModel
        {
            Id = user.Id,
            Token = user.Token,
            CompanyName = user.CompanyName
        };
    }

    public async Task<bool> DeleteUser(Guid token)
    {   
        try
        {
            await _confirmServiceContext.DeleteUserByToken(token);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

    }

    public Task<User?> GetUser(long id)
    {
        return _confirmServiceContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ClientModel>> GetUserClients(Guid token)
    {
        var clients = await _confirmServiceContext.GetUserClients(token);
        return clients.Select(client => new ClientModel
        {
            Email = client.Email,
            Name = client.Name,
            IsEmailConfirm = client.IsEmailConfirm,
            IsMailSent = client.IsMailSent
        }).ToList();
    }

    public async Task SendConfirmationEmail(Guid userToken, string clientEmail)
    {
        //это первая транзакция
        var user = await _confirmServiceContext.GetUserByToken(userToken);
        var newClient = new ClientOfUser("Vasya", clientEmail);
        user.AddClient(newClient);
        await _confirmServiceContext.SaveChangesAsync();
        
        //это вторая
        // await _mailSendService.SendEmailToClient();
        newClient.MarkAsEmailSent();
        await _confirmServiceContext.SaveChangesAsync();
    }
}

public class ClientModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsMailSent { get; set; }
    public bool IsEmailConfirm {  get; set; }
}
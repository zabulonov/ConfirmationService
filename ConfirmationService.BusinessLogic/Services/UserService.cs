using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.Core.Entity;
using ConfirmationService.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ConfirmationService.BusinessLogic.Services;

public class UserService(ConfirmServiceContext confirmServiceContext, MailSendService mailSendService)
{
    public async Task<Guid> RegisterNewUser(string companyName)
    {
        var newUser = new User(companyName);
        await confirmServiceContext.AddUser(newUser);
        UserTokens.AddUser(new UserTokens.UserToken()
        {
            Token = newUser.Token,
            CompanyName = companyName
        });
        return newUser.Token;
    }
    
    public async Task<bool> DeleteUser(Guid token)
    {   
        try
        {
            await confirmServiceContext.DeleteUserByToken(token);
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
        return confirmServiceContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ClientModel>> GetUserClients(Guid token)
    {
        var clients = await confirmServiceContext.GetUserClients(token);
        return clients.Select(client => new ClientModel
        {
            Email = client.Email,
            Name = client.Name,
            IsEmailConfirm = client.IsEmailConfirm,
            IsMailSent = client.IsMailSent
        }).ToList();
    }
 // todo нужно ли переименовать для понятности?
    public async Task SendConfirmationEmail(UserClientModel userClientModel, Guid userToken)
    {
        var user = await confirmServiceContext.GetUserByToken(userToken);
        var newClient = new ClientOfUser(userClientModel.Name, userClientModel.Email);
        user.AddClient(newClient);
        await confirmServiceContext.SaveChangesAsync();
        
        await mailSendService.SendEmailToClient(userClientModel, newClient.ConfirmToken);
        newClient.MarkAsEmailSent();
        await confirmServiceContext.SaveChangesAsync();
    }
}

public class ClientModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsMailSent { get; set; }
    public bool IsEmailConfirm {  get; set; }
}
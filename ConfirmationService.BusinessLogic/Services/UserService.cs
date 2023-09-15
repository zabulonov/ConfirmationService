using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.Core.Entity;
using ConfirmationService.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ConfirmationService.BusinessLogic.Services;

public class UserService
{
    private readonly ConfirmServiceContext _confirmServiceContext;

    public UserService(ConfirmServiceContext confirmServiceContext)
    {
        _confirmServiceContext = confirmServiceContext;
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

    public async Task<List<ClientOfUser>> GetUserClients(long id)
    {
        return _confirmServiceContext.Clients.Where(x => x.UserId == id).ToList();
    }

    public long TokenToPK(Guid token)
    {
        return _confirmServiceContext.Users.FirstOrDefault(x => x.Token == token)!.Id;
    }
}
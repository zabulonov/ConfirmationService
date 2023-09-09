using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.Core.Entity;
using ConfirmationService.Host;
using ConfirmationService.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ConfirmationService.BusinessLogic.Services;

public class UserService
{
    private readonly ConfirmServiceContext _db;
    private readonly UserTokens _userTokens;

    public UserService(ConfirmServiceContext db, UserTokens userTokens)
    {
        _db = db;
        _userTokens = userTokens;
    }

    public async Task<Guid> RegisterNewUser(string companyName)
    {
        var newUser = new User(companyName);
        await _db.AddUser(newUser);
        _userTokens.AddUser(new UserTokens.UserToken
        {
            Token = newUser.Token,
            CompanyName = newUser.CompanyName
        });
        return newUser.Token;
    }

    public async Task<UserModel> CheckToken(Guid token)
    {
        var user = await _db.GetUserByToken(token);
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
            await _db.DeleteUserByToken(token);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

    }

    public Task<UserModel?> GetUser(long id)
    {
        return _db.Set<UserModel>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
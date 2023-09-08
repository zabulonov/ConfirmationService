using ConfirmationService.BusinessLogic;
using ConfirmationService.BusinessLogic.DbModels;

namespace ConfirmationService.Host;

public class UserService
{
    private readonly ConfirmServiceContext _db;

    public UserService(ConfirmServiceContext db)
    {
        _db = db;
    }

    public Guid RegisterNewUser(string companyName)
    {
        UserModel newUser = new UserModel(companyName);
        _db.Add(newUser);
        _db.SaveChanges();
        return newUser.GetToken();
    }

    public UserModel CheckToken(Guid token)
    {
        foreach (var user in _db.Users)
        {
            if (user.Token == token)
                return user;
        }
        throw new Exception("Token isn't valid.");
    }

    public bool DeleteUser(UserModel user)
    {
        try
        {
            _db.Remove(user);
            _db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

    }
    
}
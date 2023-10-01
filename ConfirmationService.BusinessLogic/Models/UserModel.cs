using ConfirmationService.Core.Entity;

namespace ConfirmationService.BusinessLogic.Models;


public class UserModel
{
    public int Id { get; set; }
    
    public string CompanyName { get; set; }

    public Guid Token { get; set; }

    public UserModel()
    {
    }

    public UserModel(string companyName)
    {
        CompanyName = companyName;
        Token = Guid.NewGuid();
    }

    public Guid GetToken()
    {
        return Token;
    }
}
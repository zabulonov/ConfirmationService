using System.ComponentModel.DataAnnotations;

namespace ConfirmationService.BusinessLogic.DbModels;

public class UserModel
{
    public int Id { get; set; }

    [Required]
    public string CompanyName { get; set; }

    public Guid Token { get; private set; }

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
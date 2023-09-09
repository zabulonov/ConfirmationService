namespace ConfirmationService.Core.Entity;

public class User
{
    public int Id { get; private set; }
    public string CompanyName { get; private set; }
    public Guid Token { get; private set; }
    
    private User()
    {
    }

    public User(string companyName)
    {
        CompanyName = companyName;
        Token = Guid.NewGuid();
    }

}
namespace ConfirmationService.Core.Entity;

public class User
{
    public int Id { get; private set; }
    public string CompanyName { get; private set; }
    public Guid Token { get; private set; }

    public ICollection<ClientOfUser> Clients { get; private set; }

    private User()
    {
    }

    public User(string companyName)
    {
        CompanyName = companyName;
        Token = Guid.NewGuid();
    }

    public void AddClient(ClientOfUser newClient)
    {
        Clients.Add(newClient);
    }
}
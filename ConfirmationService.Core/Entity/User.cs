namespace ConfirmationService.Core.Entity;

public class User(string companyName)
{
    public int Id { get; private set; }
    public string CompanyName { get; private set; } = companyName;
    public Guid Token { get; private set; } = Guid.NewGuid();
    public ICollection<ClientOfUser>? Clients { get; private set; }

    // private User()
    // {
    // }

    public void AddClient(ClientOfUser newClient)
    {
        Clients ??= new List<ClientOfUser>();
        Clients.Add(newClient);
    }
}
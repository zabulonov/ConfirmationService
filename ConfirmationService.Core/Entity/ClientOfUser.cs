namespace ConfirmationService.Core.Entity;

public class ClientOfUser
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public bool IsMailSent { get; private set; }
    public bool IsEmailConfirm {  get; private set; }
    public Guid ConfirmToken {  get; private set; }
    
    public int UserId { get; }
    public User User { get; }

    // private ClientOfUser()
    // {
    // }

    public ClientOfUser(string name, string email)
    {
        Name = name;
        Email = email;
        ConfirmToken = Guid.NewGuid();
        IsMailSent = false;
        IsEmailConfirm = false;
    }

    public void IsEmailConfirmSetter(bool isConfirm)
    {
        IsEmailConfirm = isConfirm;
    }
    
    public void IsEmailSendSetter(bool isSend)
    {
        IsMailSent = isSend;
    }

    public void MarkAsEmailSent()
    {
        IsMailSent = true;
    }
}
namespace ConfirmationService.BusinessLogic.Models;

public class ClientOfUserModel
{
    public Guid UserToken {  get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
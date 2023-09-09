namespace ConfirmationService.BusinessLogic.Models;

public class ClientModel
{
    public int Id { get; set; }
    
    public bool IsConfirmMail { get; set; }
    
    public bool IsMailSend { get; set; }

    public EmailModel EmailModel { get; set; }
}
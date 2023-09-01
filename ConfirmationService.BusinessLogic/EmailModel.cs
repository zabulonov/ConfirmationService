namespace ConfirmationService.BusinessLogic;

public class EmailModel
{
    public string FromName { get; set; }
    public string FromAddress{ get; set; }
    public string ToName{ get; set; }
    public string ToAdress{ get; set; }
    public string Subject{ get; set; }
    public string Body{ get; set; }
}
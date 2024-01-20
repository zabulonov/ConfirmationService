namespace ConfirmationService.BusinessLogic.Models;
public class EmailModel
{
    public string FromName { get; set; } = null!;
    public string ToName { get; set; } = null!;
    public string ToAdress { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
}
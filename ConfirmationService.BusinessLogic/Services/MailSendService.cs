using MimeKit;

namespace ConfirmationService.BusinessLogic.Services;

public class MailSendService
{
    private readonly MailConnect mail;
    
    public MailSendService(MailConnect mailConnect)
    {
        mail = mailConnect;
    }

    public void SendEmail(EmailModel emailModel)
    {
        var message = new MimeMessage ();
        message.From.Add (new MailboxAddress (emailModel.FromName, "zabulonov444@yandex.ru"));
        message.To.Add (new MailboxAddress (emailModel.ToName, emailModel.ToAdress));
        message.Subject = emailModel.Subject;

        message.Body = new TextPart ("plain") {
            Text = emailModel.Body + 
                   @" 
                https://localhost:7276/EmailConfirmation/Confirm"

        };
        mail.client.Send(message);
    }
}
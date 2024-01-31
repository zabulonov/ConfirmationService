using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.Infrastructure.MailConnectService;
using MimeKit;

namespace ConfirmationService.BusinessLogic.Services;

public class MailSendService(MailConnect mailConnectConnect)
{
    public async Task SendEmail(EmailModel emailModel)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(emailModel.FromName, mailConnectConnect.GetEmailLogin()));
        message.To.Add(new MailboxAddress(emailModel.ToName, emailModel.ToAdress));
        message.Subject = emailModel.Subject;

        message.Body = new TextPart("plain")
        {
            Text = emailModel.Body
        };
        await mailConnectConnect.Send(message);
    }
    public async Task SendEmailToClient(UserClientModel userClientModel, Guid token)
    {
        
        var emailModel = new EmailModel
        {
            FromName = "Email Confirmation Service",
            ToName = userClientModel.Name,
            ToAdress = userClientModel.Email,
            Subject = "Email Confirmation"
        };

        var message = new MimeMessage();
        
        message.From.Add(new MailboxAddress(emailModel.FromName, mailConnectConnect.GetEmailLogin()));
        message.To.Add(new MailboxAddress(emailModel.ToName, emailModel.ToAdress));
        message.Subject = emailModel.Subject;
        
        var builder = new BodyBuilder ();
        // TODO - Можно ли как то вынести это в appsettings?
        builder.HtmlBody = string.Format("<h2>Dear {0},</h2><h4>To confirm your email, please follow the link:</h4><center><form method=\"get\" action=\"http://localhost:5277/email-confirmation/confirm?\"> <input type=\"hidden\" name=\"token\" value=\"{1}\" /> <input type=\"submit\" value=\"Confirm\" style=\"height:50px; width:100px\" /></form></center><br>Made as a pet project by Alexey Zabulonov<br>\n<a href=\"https://t.me/ulove1337\"><b>Telegram</b></a><br>\n<a href=\"https://github.com/zabulonov\">GitHub</a><br>\n<a href=\"https://www.linkedin.com/in/alexey-zabulonov-442b03283\">LinkedIn</a>", userClientModel.Name, token);
        
        message.Body = builder.ToMessageBody ();
        await mailConnectConnect.Send(message);
    }
}
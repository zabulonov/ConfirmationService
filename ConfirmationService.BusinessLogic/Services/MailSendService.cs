using System.Text;
using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.Core.Entity;
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

    public async Task SendClientsStatus(List<ClientOfUser> clients, User user, String mail)
    {
        var emailModel = new EmailModel
        {
            FromName = "Email Confirmation Service",
            ToName = user.CompanyName,
            ToAdress = mail,
            Subject = "Clients Email Confirmation status"
        };
        
        var message = new MimeMessage();
        
        message.From.Add(new MailboxAddress(emailModel.FromName, mailConnectConnect.GetEmailLogin()));
        message.To.Add(new MailboxAddress(emailModel.ToName, emailModel.ToAdress));
        message.Subject = emailModel.Subject;
        
        var builder = new BodyBuilder ();

        StringBuilder body = new StringBuilder();

        body.Append(
            "<!DOCTYPE html>\n<html>\n<head>\n<style>\ntable, th, td {\n  border: 1px solid black;\n  border-collapse: collapse;\n}\n</style>\n</head>\n<body>\n\n<h2>Your clients</h2>\n<p>The table displays all your clients (with names and email), as well as email confirmation status.</p>\n\n<table style=\"width:100%\">\n  <tr>\n    <th>Name</th>\n    <th>Email</th> \n    <th>Status</th>\n  </tr>");
        
        foreach (var client in clients)
        {
            body.Append(string.Format("<tr>\n    <td>{0}</td>\n    <td>{1}</td>\n    <td>{2}</td>\n  </tr>", client.Name, client.Email, client.IsEmailConfirm)); 
        }
        body.Append("</table>\n\n</body>\n</html>");

        builder.HtmlBody = body.ToString();
        message.Body = builder.ToMessageBody();
        await mailConnectConnect.Send(message);
    }
}
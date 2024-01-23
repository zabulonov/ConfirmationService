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
        
            message.Body = new TextPart("plain")
            {
                Text = emailModel.Body +
                       @$" Dear {userClientModel.Name}, to confirm your email, please follow the link: 
http://localhost:5277/email-confirmation/confirm?token={token}"
            };
            await mailConnectConnect.Send(message);
    }
}
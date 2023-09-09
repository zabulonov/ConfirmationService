using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.Infrastructure.MailConnectService;
using MimeKit;

namespace ConfirmationService.BusinessLogic.Services;

public class MailSendService
{
    private readonly MailConnect _mailConnect;

    public MailSendService(MailConnect mailConnectConnect)
    {
        _mailConnect = mailConnectConnect;
    }

    public async Task SendEmail(EmailModel emailModel)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(emailModel.FromName, "zabulonov444@yandex.ru"));
        message.To.Add(new MailboxAddress(emailModel.ToName, emailModel.ToAdress));
        message.Subject = emailModel.Subject;

        message.Body = new TextPart("plain")
        {
            Text = emailModel.Body +
                   @"https://localhost:7276/EmailConfirmation/Confirm"
        };
        await _mailConnect.Send(message);
    }
}
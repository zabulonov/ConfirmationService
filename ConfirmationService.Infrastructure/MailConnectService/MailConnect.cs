using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace ConfirmationService.Infrastructure.MailConnectService;

public class MailConnect
{
    private readonly MailConnectConfiguration _mailConnectConfiguration;
    
    public MailConnect(MailConnectConfiguration mailConnectConfiguration)
    {
        _mailConnectConfiguration = mailConnectConfiguration;
    }
    
    public async Task Send(MimeMessage message)
    {
        using var smtpClient = new SmtpClient();
        await ConnectToSmtpServer(smtpClient);
        await smtpClient.SendAsync(message);
        await smtpClient.DisconnectAsync(true);
    }

    private async Task ConnectToSmtpServer(SmtpClient smtpClient)
    {
        await smtpClient.ConnectAsync(_mailConnectConfiguration.Server, _mailConnectConfiguration.Port, true);
        await smtpClient.AuthenticateAsync(_mailConnectConfiguration.Login, _mailConnectConfiguration.Password);

    }
}
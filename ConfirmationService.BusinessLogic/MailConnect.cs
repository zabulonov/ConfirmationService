using MailKit.Net.Smtp;

namespace ConfirmationService.BusinessLogic;

public class MailConnect
{
    // Я не понял как связать их с appsettings в этом проекте чтобы не было циклических зависимостей 
    private string Server = "smtp.yandex.ru";
    private string Login = "zabulonov444@yandex.ru";
    private string Password = "asbhltihcwllxcfz";
    private int Port = 465;

    public SmtpClient client = new SmtpClient();

    public MailConnect()
    {
        client.Connect(Server, Port, true);
        client.Authenticate(Login, Password);
    }
}
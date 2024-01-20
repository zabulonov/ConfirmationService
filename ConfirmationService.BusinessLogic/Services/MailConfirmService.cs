using ConfirmationService.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ConfirmationService.BusinessLogic.Services;

public class MailConfirmService(ConfirmServiceContext confirmServiceContext)
{
    //todo Разобрать, как будто бы нужно возвращать в контроллер строку, а не выкидывать исключение
    public async Task ConfirmMail(Guid token)
    {
        var client = await confirmServiceContext.Clients.FirstOrDefaultAsync(x => x.ConfirmToken == token);

        if (client != null)
        {
            client.IsEmailConfirmSetter(true);
            confirmServiceContext.Update(client);
            await confirmServiceContext.SaveChangesAsync();
        }
        else
            throw new Exception("Invalid token");
    }
    
}
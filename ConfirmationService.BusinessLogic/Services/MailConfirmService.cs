using ConfirmationService.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ConfirmationService.BusinessLogic.Services;

public class MailConfirmService
{
    private readonly ConfirmServiceContext _confirmServiceContext;

    public MailConfirmService(ConfirmServiceContext confirmServiceContext)
    {
        _confirmServiceContext = confirmServiceContext;
    }

    public void ConfirmMail(Guid token)
    {
        var client = _confirmServiceContext.Clients.FirstOrDefaultAsync(x => x.ConfirmToken == token).Result;

        if (client != null)
        {
            client.IsEmailConfirmSetter(true);
            _confirmServiceContext.Update(client);
            _confirmServiceContext.SaveChanges();
        }
        else
            throw new Exception("Invalid token");
    }
    
}
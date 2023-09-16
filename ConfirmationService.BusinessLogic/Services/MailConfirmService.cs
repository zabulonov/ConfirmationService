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

    public async Task ConfirmMail(Guid token)
    {
        var client = await _confirmServiceContext.Clients.FirstOrDefaultAsync(x => x.ConfirmToken == token);

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
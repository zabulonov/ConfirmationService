using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.Core.Entity;

namespace ConfirmationService.BusinessLogic.Services.Interfaces;

public interface IUserService
{
    public Task<Guid> RegisterNewUser(string companyName);

    public Task<bool> DeleteUser(Guid token);

    public Task<User?> GetUser(long id);

    public Task<List<ClientModel>> GetUserClients(Guid token);

    public Task SendConfirmationEmail(UserClientModel userClientModel, Guid userToken);

    public Task<ClientOfUser?> GetClientByMail(Guid userToken, String Mail);

    public Task ResendingConfirmation(String Mail, Guid userToken);

    public Task<bool> GetClientByMailStatus(Guid userToken, String Mail);

    public Task GetClientsStatusByMail(Guid token, String mail);
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfirmationService.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConfirmationService.Infrastructure.EntityFramework;

public class ConfirmServiceContext : DbContext
{
    public ConfirmServiceContext(DbContextOptions<ConfirmServiceContext> options) : base(options)
    {
    }
    
    public DbSet<ClientOfUser> Clients { get; set; }
    public DbSet<User> Users { get; set; }

    public async Task<User> GetUserByToken(Guid token)
    {
        return await Set<User>().FirstOrDefaultAsync(x => x.Token.Equals(token));
    }

    public Task<List<User>> GetAllUsers()
    {
        return Set<User>().ToListAsync();
    }

    public async Task DeleteUserByToken(Guid token)
    {
        var user = await GetUserByToken(token);
        if (user != null)
        {
            Remove(user);
            await SaveChangesAsync();
        }

        throw new ArgumentException($"No user found by token {token}");
    }

    public async Task AddUser(User newUser)
    {
        await AddAsync(newUser);
        await SaveChangesAsync();
    }
    
    public async Task AddClient(ClientOfUser newClient)
    {
        await AddAsync(newClient);
        await SaveChangesAsync();
    }

    public async Task<ICollection<ClientOfUser>> GetUserClients(Guid token)
    {
        var user = await Set<User>()
            .Include(x => x.Clients)
            .FirstOrDefaultAsync(x => x.Token.Equals(token));
        return user != null ? user.Clients : Array.Empty<ClientOfUser>();
    }
}
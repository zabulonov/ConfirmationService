using System;
using System.Threading.Tasks;
using ConfirmationService.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConfirmationService.Infrastructure.EntityFramework;

public class ConfirmServiceContext : DbContext
{
    public ConfirmServiceContext(DbContextOptions<ConfirmServiceContext> options) : base(options)
    {
    }
    
    

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.UseSerialColumns();
    // }

    public DbSet<User> Users { get; set; }
    // public DbSet<ClientModel> Clients { get; set; }

    public Task<User> GetUserByToken(Guid token)
    {
        return Set<User>().FirstOrDefaultAsync(x => x.Token.Equals(token));
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
}
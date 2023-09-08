using ConfirmationService.BusinessLogic.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ConfirmationService.Host;

public class ConfirmServiceContext : DbContext
{
    public ConfirmServiceContext(DbContextOptions<ConfirmServiceContext> options) : base(options)
    {
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.UseSerialColumns();
    // }

    public DbSet<UserModel>? Users { get; set; }
    public DbSet<ClientModel>? Clients { get; set; }
}
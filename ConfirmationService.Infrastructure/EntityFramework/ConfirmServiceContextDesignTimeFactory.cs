using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConfirmationService.Infrastructure.EntityFramework;

[UsedImplicitly]
public class ConfirmServiceContextDesignTimeFactory : IDesignTimeDbContextFactory<ConfirmServiceContext>
{
    public ConfirmServiceContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ConfirmServiceContext>();
        optionsBuilder.UseNpgsql("Server=localhost; Database=ConfirmDb; Port=5432; User Id=postgres; Password=1234");

        return new ConfirmServiceContext(optionsBuilder.Options);
    }
}
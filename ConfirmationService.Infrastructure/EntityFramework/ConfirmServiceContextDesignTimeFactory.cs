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
        return new ConfirmServiceContext(optionsBuilder.Options);
    }
}
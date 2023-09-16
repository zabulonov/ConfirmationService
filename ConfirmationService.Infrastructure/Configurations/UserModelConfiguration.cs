using ConfirmationService.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfirmationService.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable(nameof(User));

        builder.Property(x => x.CompanyName).IsRequired();

        builder.HasMany<ClientOfUser>(x => x.Clients)
            .WithOne(u => u.User)
            .HasForeignKey(k => k.UserId);
    }
}
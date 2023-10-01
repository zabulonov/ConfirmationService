using ConfirmationService.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfirmationService.Infrastructure.Configurations;

public class ClientOfUserConfiguration : IEntityTypeConfiguration<ClientOfUser>
{
    public void Configure(EntityTypeBuilder<ClientOfUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable(nameof(ClientOfUser));
        
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Name).IsRequired();
    }
}
using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.EMail)
                .IsRequired();
            
            builder.Property(u => u.Vorname)
                .IsRequired();
            
            builder.Property(u => u.Nachname)
                .IsRequired();
            
            builder.Property(u => u.Anrede)
                .IsRequired();
            
            builder.Property(u => u.RowVersion)
                .IsRowVersion();

            builder.HasOne(u => u.Staatsangehörigkeit)
                .WithOne()
                .HasForeignKey<User>(u => u.StaatsangehörigkeitId);
        }
    }
}
using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class AdresseConfiguration : IEntityTypeConfiguration<Adresse>
    {
        public void Configure(EntityTypeBuilder<Adresse> builder)
        {
            builder.Property(a => a.Straße)
                .IsRequired();
            
            builder.Property(a => a.Hausnummer)
                .IsRequired();
            
            builder.Property(a => a.Plz)
                .IsRequired();
            
            builder.Property(a => a.Ort)
                .IsRequired();

            builder.HasOne(a => a.Land)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Property(a => a.RowVersion)
                .IsRowVersion();
        }
    }
}
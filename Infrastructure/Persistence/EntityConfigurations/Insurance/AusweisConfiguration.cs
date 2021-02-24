using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class AusweisConfiguration : IEntityTypeConfiguration<Ausweis>
    {
        public void Configure(EntityTypeBuilder<Ausweis> builder)
        {
            builder.Property(a => a.Behörde)
                .IsRequired();
            
            builder.Property(a => a.Geburtsname);
            
            builder.Property(a => a.Ausstellungsdatum)
                .IsRequired();
            
            builder.Property(a => a.Aublaufdatum)
                .IsRequired();
            
            builder.Property(a => a.Ausweisnummer)
                .IsRequired();
            
            builder.Property(a => a.Geburtsort)
                .IsRequired();
            
            builder.HasOne(a => a.Staatsangehörigkeit)
                .WithMany()
                .IsRequired();
            
            builder.Property(a => a.Ausweisart)
                .IsRequired();

            builder.Property(a => a.RowVersion)
                .IsRowVersion();
        }
    }
}
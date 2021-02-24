using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class DokumentConfiguration : IEntityTypeConfiguration<Dokument>
    {
        public void Configure(EntityTypeBuilder<Dokument> builder)
        {
            //Meduimblob offers 16mb of storage in MySQL
            builder.Property(d => d.Data)
                .HasColumnType("MediumBlob")
                .IsRequired();

            builder.HasOne(d => d.DokumentenArt)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            
            builder.Property(d => d.RowVersion)
                .IsRowVersion();
        }
    }
}
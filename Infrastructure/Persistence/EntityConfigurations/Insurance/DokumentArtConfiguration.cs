using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class DokumentArtConfiguration : IEntityTypeConfiguration<DokumentArt>
    {
        public void Configure(EntityTypeBuilder<DokumentArt> builder)
        {
            builder.Property(da => da.Name)
                .IsRequired();

            builder.Property(da => da.RowVersion)
                .IsRowVersion();
        }
    }
}
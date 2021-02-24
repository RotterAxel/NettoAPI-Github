using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class TitelConfiguration : IEntityTypeConfiguration<Titel>
    {
        public void Configure(EntityTypeBuilder<Titel> builder)
        {
            builder.Property(t => t.BezeichnungKurz)
                .IsRequired();
            
            builder.Property(t => t.Beschreibung)
                .IsRequired();
            
            builder.Property(t => t.RowVersion)
                .IsRowVersion();
        }
    }
}
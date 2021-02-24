using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class LandConfiguration : IEntityTypeConfiguration<Land>
    {
        public void Configure(EntityTypeBuilder<Land> builder)
        {
            builder.Property(l => l.Name)
                .IsRequired();
            
            builder.Property(l => l.RowVersion)
                .IsRowVersion();
        }
    }
}
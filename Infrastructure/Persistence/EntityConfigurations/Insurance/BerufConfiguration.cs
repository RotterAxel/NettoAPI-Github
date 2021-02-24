using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class BerufConfiguration : IEntityTypeConfiguration<Beruf>
    {
        public void Configure(EntityTypeBuilder<Beruf> builder)
        {
            builder.Property(b => b.Name)
                .IsRequired();
            
            builder.Property(b => b.RowVersion)
                .IsRowVersion();
        }
    }
}
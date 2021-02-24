using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class EinladecodeConfiguration : IEntityTypeConfiguration<EinladecodeVermittler>
    {
        public void Configure(EntityTypeBuilder<EinladecodeVermittler> builder)
        {
            builder.Property(ec => ec.Code)
                .IsRequired();

            builder.Property(ec => ec.VermittlerId)
                .IsRequired();
        }
    }
}
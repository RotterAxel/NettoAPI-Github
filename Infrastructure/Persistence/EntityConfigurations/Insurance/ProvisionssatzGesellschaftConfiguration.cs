using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class ProvisionssatzGesellschaftConfiguration : IEntityTypeConfiguration<ProvisionssatzGesellschaft>
    {
        public void Configure(EntityTypeBuilder<ProvisionssatzGesellschaft> builder)
        {
            builder.HasOne(pg => pg.Gesellschaft)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            
            builder.HasOne(pg => pg.Vermittler)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
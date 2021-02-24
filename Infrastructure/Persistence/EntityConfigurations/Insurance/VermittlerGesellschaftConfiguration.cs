using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class VermittlerGesellschaftConfiguration : IEntityTypeConfiguration<VermittlerGesellschafft>
    {
        public void Configure(EntityTypeBuilder<VermittlerGesellschafft> builder)
        {
            builder.HasKey(vg => new {vg.VermittlerId, vg.GesellschaftId});
        }
    }
}
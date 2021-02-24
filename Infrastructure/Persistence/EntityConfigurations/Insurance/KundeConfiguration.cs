using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class KundeConfiguration : IEntityTypeConfiguration<Kunde>
    {
        public void Configure(EntityTypeBuilder<Kunde> builder)
        {
            builder.Property(vn => vn.Familienstand)
                .IsRequired();

            builder.HasOne(k => k.User)
                .WithOne()
                .HasForeignKey<Kunde>(k => k.UserId)
                .IsRequired();

            builder.Property(vn => vn.RowVersion)
                .IsRowVersion();
        }
    }
}
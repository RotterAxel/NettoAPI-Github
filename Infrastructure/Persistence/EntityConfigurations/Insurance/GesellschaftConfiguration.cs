using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class GesellschaftConfiguration : IEntityTypeConfiguration<Gesellschaft>
    {
        public void Configure(EntityTypeBuilder<Gesellschaft> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired();
        }
    }
}
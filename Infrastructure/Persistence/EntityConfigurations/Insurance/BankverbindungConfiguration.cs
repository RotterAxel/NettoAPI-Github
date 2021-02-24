using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class BankverbindungConfiguration : IEntityTypeConfiguration<Bankverbindung>
    {
        public void Configure(EntityTypeBuilder<Bankverbindung> builder)
        {
            builder.Property(a => a.IBAN)
                .IsRequired();
            
            builder.Property(a => a.BankName)
                .IsRequired();
            
            builder.Property(a => a.BIC)
                .IsRequired();

            builder.Property(a => a.RowVersion)
                .IsRowVersion();
        }
    }
}
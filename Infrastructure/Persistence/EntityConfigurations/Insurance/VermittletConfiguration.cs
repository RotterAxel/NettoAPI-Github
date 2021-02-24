using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations.Insurance
{
    public class VermittletConfiguration : IEntityTypeConfiguration<Vermittler>
    {
        public void Configure(EntityTypeBuilder<Vermittler> builder)
        {
            builder.Property(v => v.VermittlerNo)
                .IsRequired();
            
            builder.Property(v => v.VermittlerRegistrierungsstatus)
                .IsRequired();
            
            builder.Property(v => v.IhkRegistrierungsnummer)
                .IsRequired();
            
            builder.HasOne(vn => vn.User)
                .WithOne()
                .HasForeignKey<Vermittler>(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(v => v.EingeladenVon)
                .WithOne()
                .HasForeignKey<Vermittler>(v => v.EingeladenVonVermittlerEinladecodeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(v => v.EinladecodeVermittler)
                .WithOne()
                .HasForeignKey<EinladecodeVermittler>(e => e.VermittlerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.RegistrierungsDokumente)
                .WithOne()
                .HasForeignKey(d => d.VermittlerRegistrierungsDokumentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(v => v.RegistrierungsDokumenteHistorie)
                .WithOne()
                .HasForeignKey(d => d.VermittlerRegistrierungsDokumentHistorienId)
                .OnDelete(DeleteBehavior.SetNull);
            
            builder.HasMany(v => v.Kunden)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(v => v.RowVersion)
                .IsRowVersion();
        }
    }
}
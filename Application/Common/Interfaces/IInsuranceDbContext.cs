using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IInsuranceDbContext
    {
        DbSet<Adresse> Adressen { get; set; }
        DbSet<Ausweis> Ausweise { get; set; }
        DbSet<Bankverbindung> Bankverbindungen { get; set; }
        DbSet<Beruf> Berufe { get; set; }
        DbSet<Dokument> Dokumente { get; set; }
        DbSet<DokumentArt> DokumentArtSet { get; set; }
        DbSet<EinladecodeVermittler> EinladecodesVermittler { get; set; }
        DbSet<Gesellschaft> GesellschaftSet { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        DbSet<Land> Länder { get; set; }
        DbSet<ProvisionssatzGesellschaft> ProvisionssätzeGesellschaft { get; set; }
        DbSet<Titel> TitelSet { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Kunde> Versicherungsnehmer { get; set; }
        DbSet<Vermittler> Vermittler { get; set; } 
        DbSet<VermittlerGesellschafft> VermittlerGesellschafften { get; set; }
        
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
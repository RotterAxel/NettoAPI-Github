using System.IO;
using System.Reflection;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.DbContexts.Insurance
{
    public class InsuranceDbContext : BaseContext, IInsuranceDbContext
    {
        public DbSet<Adresse> Adressen { get; set; }
        public DbSet<Ausweis> Ausweise { get; set; }
        public DbSet<Bankverbindung> Bankverbindungen { get; set; }
        public DbSet<Beruf> Berufe { get; set; }
        public DbSet<Dokument> Dokumente { get; set; }
        public DbSet<DokumentArt> DokumentArtSet { get; set; }
        public DbSet<EinladecodeVermittler> EinladecodesVermittler { get; set; }
        public DbSet<Gesellschaft> GesellschaftSet { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Land> Länder { get; set; }
        public DbSet<ProvisionssatzGesellschaft> ProvisionssätzeGesellschaft { get; set; }
        public DbSet<Titel> TitelSet { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Kunde> Versicherungsnehmer { get; set; }
        public DbSet<Vermittler> Vermittler { get; set; }
        public DbSet<VermittlerGesellschafft> VermittlerGesellschafften { get; set; }
        
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options, IDateTime dateTimeService, ICurrentUserService currentUserService)
            : base(options, dateTimeService, currentUserService)
        {
        }

        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Ignore<AuditableEntity>();
            base.OnModelCreating(builder);
        }
    }
    
    public class InsuranceDbContextDesignFactory : IDesignTimeDbContextFactory<InsuranceDbContext>
    {
        public InsuranceDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebUI"))
                .AddJsonFile("appsettings.json")
                .Build();
            
            var optionsBuilder = new DbContextOptionsBuilder<InsuranceDbContext>()
                .UseMySql(config.GetConnectionString("InsuranceConnection"));
    
            return new InsuranceDbContext(optionsBuilder.Options);
            
        }
    }
}
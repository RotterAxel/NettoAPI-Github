using Domain.Common;
using Domain.Enums;

namespace Domain.Entities.Insurance
{
    public class Dokument : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DokumentArt DokumentenArt { get; set; }
        public Bearbeitungsstatus Bearbeitungsstatus { get; set; }
        public FileExtension FileExtension { get; set; }
        public byte[] Data { get; set; }
        public int? VermittlerRegistrierungsDokumentId { get; set; }
        public int? VermittlerRegistrierungsDokumentHistorienId { get; set; }
    }
}
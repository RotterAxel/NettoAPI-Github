using Domain.Common;

namespace Domain.Entities.Insurance
{
    public class DokumentArt : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
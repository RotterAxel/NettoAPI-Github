using Domain.Common;

namespace Domain.Entities.Insurance
{
    public class Titel : AuditableEntity
    {
        public int Id { get; set; }
        public string BezeichnungKurz { get; set; }
        public string Beschreibung { get; set; }
    }
}
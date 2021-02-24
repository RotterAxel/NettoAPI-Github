using Domain.Common;

namespace Domain.Entities.Insurance
{
    public class Adresse : AuditableEntity
    {
        public int Id { get; set; }
        public string Straße { get; set; }
        public string Hausnummer { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public Land Land { get; set; }
    }
}
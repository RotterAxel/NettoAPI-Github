using Domain.Common;
using Domain.Enums;

namespace Domain.Entities.Insurance
{
    public class Kunde : AuditableEntity
    {
        public int Id { get; set; }
        public string SteuerId { get; set; }
        public bool ÖffentlichesAmt { get; set; }
        public bool IstVersicherungsnehmer { get; set; }
        public bool VersichertePerson { get; set; }

        public Familienstand Familienstand { get; set; }
        public Berufsstatus? Berufsstatus { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Titel Titel { get; set; }
        public Beruf Beruf { get; set; }
        public Ausweis Ausweis { get; set; }
    }
}
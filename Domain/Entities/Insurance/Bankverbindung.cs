using Domain.Common;

namespace Domain.Entities.Insurance
{
    public class Bankverbindung : AuditableEntity
    {
        public int Id { get; set; }
        public string Kontoinhaber { get; set; }
        public string IBAN { get; set; }
        public string BankName { get; set; }
        public string BIC { get; set; }
    }
}
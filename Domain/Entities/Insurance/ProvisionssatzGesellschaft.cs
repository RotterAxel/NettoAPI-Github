using Domain.Common;

namespace Domain.Entities.Insurance
{
    public class ProvisionssatzGesellschaft : AuditableEntity
    {
        public int Id { get; set; }
        public int MaxLaufzeitInJahren { get; set; }
        public string VermittlerNr { get; set; }
        public double AbschlussVergütungProzent { get; set; }
        public double BestandsVergütungProzent { get; set; }

        public Vermittler Vermittler { get; set; }
        public Gesellschaft Gesellschaft { get; set; }
    }
}
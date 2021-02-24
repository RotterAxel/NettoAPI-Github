using Domain.Common;

namespace Domain.Entities.Insurance
{
    public class VermittlerGesellschafft : AuditableEntity
    {
        public int VermittlerId { get; set; }
        public Vermittler Vermittler { get; set; }

        public int GesellschaftId { get; set; }
        public Gesellschaft Gesellschaft { get; set; }

        public string VermittlerNo { get; set; }
        public double Abschlussvergütung { get; set; }
        public double Bestandsvergütung { get; set; }
        public int MaxLaufzeitVergütung { get; set; }
    }
}
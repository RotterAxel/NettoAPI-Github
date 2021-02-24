using Domain.Common;

namespace Domain.Entities.Insurance
{
    public class EinladecodeVermittler : AuditableEntity
    {
        public int Id { get; set; }
        public int VermittlerId { get; set; }
        public string Code { get; set; }
    }
}
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities.Insurance
{
    public class Gesellschaft : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<VermittlerGesellschafft> VermittlerGesellschafften { get; set; }
            = new List<VermittlerGesellschafft>();
    }    
}
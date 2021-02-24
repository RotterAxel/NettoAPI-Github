using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities.Insurance
{
    public class Ausweis : AuditableEntity
    {
        public int Id { get; set; }
        public string Behörde { get; set; }
        public string Geburtsname { get; set; }
        public DateTime Ausstellungsdatum { get; set; }
        public DateTime Aublaufdatum { get; set; }
        public string Ausweisnummer { get; set; }
        public string Geburtsort { get; set; }
        public Land Staatsangehörigkeit { get; set; }
        
        public Ausweisart Ausweisart { get; set; }
    }
}
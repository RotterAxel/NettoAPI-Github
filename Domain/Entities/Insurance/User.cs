using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities.Insurance
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }
        public Guid KeycloakIdentifier { get; set; }
        public string EMail { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Telefon { get; set; }
        public string Fax { get; set; }

        public DateTime? Geburtsdatum { get; set; }
        public string Geburtsort { get; set; }

        public int? StaatsangehörigkeitId { get; set; }
        public Land Staatsangehörigkeit { get; set; }
        public Anrede Anrede { get; set; }
        public Adresse Adresse { get; set; }
    }
}
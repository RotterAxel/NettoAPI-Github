using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities.Insurance
{
    public class Vermittler : AuditableEntity
    {
        public int Id { get; set; }
        public string VermittlerNo { get; set; }
        public float BestandsProvisionssatz { get; set; }
        public float AbschlussProvisionssatz { get; set; }

        public bool IstAktiv { get; set; }
        public string IhkRegistrierungsnummer { get; set; }
        
        public VermittlerRegistrierungsstatus VermittlerRegistrierungsstatus { get; set; } 
            = VermittlerRegistrierungsstatus.NeuerVermittler;

        public int UserId { get; set; }
        public User User { get; set; }
        public Bankverbindung Bankverbindung { get; set; }
        public EinladecodeVermittler EinladecodeVermittler { get; set; }
        public int? EingeladenVonVermittlerEinladecodeId { get; set; }
        public EinladecodeVermittler EingeladenVon { get; set; }

        public List<VermittlerGesellschafft> VermittlerGesellschafften { get; set; }
            = new List<VermittlerGesellschafft>();
        public List<Kunde> Kunden { get; set; } 
            = new List<Kunde>();
        public List<Dokument> RegistrierungsDokumente { get; set; } 
            = new List<Dokument>();
        public List<Dokument> RegistrierungsDokumenteHistorie { get; set; } 
            = new List<Dokument>();
    }
}
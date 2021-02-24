using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Insurance;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DbContexts.Insurance.InsuranceSeed
{
    public static class InsuranceDbSeed
    {
        private static int _userId;
        private static int _vermittlerId;
        private static int _adressenId;
        private static int _dokumentId;
        private static int _bankverbindungId;
        private static int _provisionssatzGesellschaft = 0;
        private static int _einladecodeId;
        
        public static void EnsureSeedDataForContext(this InsuranceDbContext context)
        {
            DeleteAllData(context);
            context.SaveChanges();

            //Seed Stammdaten
            if(!context.Länder.Any())
                GetPreconfiguredLänder(context);

            if(!context.Berufe.Any())
                GetPreconfiguredBerufe(context);
            
            if(!context.DokumentArtSet.Any())
                GetPreconfiguredDokumentArt(context);

            if (!context.TitelSet.Any())
                GetPreconfiguredTitelSet(context);
            
            context.SaveChanges();

            GetPreconfiguredGesellschaften(context);
            context.SaveChanges();
            
            GetPreconfiguredVersicherungsnehmer(context);
            context.SaveChanges();
            
            GetPreconfiguredVermittler(context);
            context.SaveChanges();

            GetPreconfiguredVermittlerGesellschaften(context);
            context.SaveChanges();
            
            GetPreconfiguredDokumenteFürVermittler(context);
            context.SaveChanges();
            
            GetPreconfiguredProvisionssätzeGesellschaft(context);
            context.SaveChanges();
        }

        private static void GetPreconfiguredVermittlerGesellschaften(InsuranceDbContext context)
        {
            var vermittlerList = context.Vermittler.ToList();

            var gesellschaftList = context.GesellschaftSet.ToList();

            if (vermittlerList.Count <= 0 || gesellschaftList.Count <= 0)
                return;
            
            foreach (var vermittler in vermittlerList)
            {
                foreach (var gesellschaft in gesellschaftList)
                {
                    context.VermittlerGesellschafften.Add(new VermittlerGesellschafft
                    {

                        VermittlerId = vermittler.Id,
                        GesellschaftId = gesellschaft.Id,
                        VermittlerNo = vermittler.VermittlerNo,
                        Abschlussvergütung = 0.08,
                        Bestandsvergütung = 0.08,
                        MaxLaufzeitVergütung = 40
                    });
                }
            }
        }

        private static void GetPreconfiguredProvisionssätzeGesellschaft(InsuranceDbContext context)
        {
            var vermittlerListeRegistrierungGenehmigt = context.Vermittler.Where(v => v.VermittlerRegistrierungsstatus
                == VermittlerRegistrierungsstatus.RegistrierungGenehmigt);

            foreach (var vermittler in vermittlerListeRegistrierungGenehmigt)
            {
                if (vermittler.Id % 2 == 0)
                {
                    context.ProvisionssätzeGesellschaft.Add(
                        new ProvisionssatzGesellschaft
                        {
                            Id = ++_provisionssatzGesellschaft,
                            MaxLaufzeitInJahren = 12,
                            VermittlerNr = "",
                            AbschlussVergütungProzent = 8.5,
                            BestandsVergütungProzent = 12.5,
                            Vermittler = vermittler,
                            Gesellschaft = context.GesellschaftSet.First(g => g.Id == 1)
                        });
                }
                else
                {
                    context.ProvisionssätzeGesellschaft.Add(
                        new ProvisionssatzGesellschaft
                        {
                            Id = ++_provisionssatzGesellschaft,
                            MaxLaufzeitInJahren = 12,
                            VermittlerNr = "",
                            AbschlussVergütungProzent = 8.5,
                            BestandsVergütungProzent = 12.5,
                            Vermittler = vermittler,
                            Gesellschaft = context.GesellschaftSet.First(g => g.Id == 2)
                        });
                }
                
            }
        }

        private static void GetPreconfiguredGesellschaften(InsuranceDbContext context)
        {
            context.GesellschaftSet.AddRange(new Gesellschaft
            {
                Id = 1,
                Name = "Bsp. Gesellschaft Bayerische"
            }, new Gesellschaft
            {
                Id = 2,
                Name = "Bsp. Gesellschaft Allianz"
            });
        }

        private static void GetPreconfiguredDokumenteFürVermittler(InsuranceDbContext context)
        {
            context.Dokumente.AddRange(new Dokument
            {
                Id = ++_dokumentId,
                Name = "Persönliche Daten",
                DokumentenArt = context.DokumentArtSet.First(das => das.Name == "Persönliche Daten"),
                Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen,
                FileExtension = FileExtension.pdf,
                Data = BeispielDokumente.PersönlicheDaten,
                VermittlerRegistrierungsDokumentId = 1
            }, new Dokument
            {
                Id = ++_dokumentId,
                Name = "Schufa Auskunft",
                DokumentenArt = context.DokumentArtSet
                    .First(das => das.Name == "Zustimmung zur Erteilung der Schufa-Auskunft"),
                Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen,
                FileExtension = FileExtension.pdf,
                Data = BeispielDokumente.Schufa,
                VermittlerRegistrierungsDokumentId = 1
            },new Dokument
            {
                Id = ++_dokumentId,
                Name = "Ausweiskopie",
                DokumentenArt = context.DokumentArtSet
                    .First(das => das.Name == "Ausweiskopie"),
                Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen,
                FileExtension = FileExtension.pdf,
                Data = BeispielDokumente.Schufa,
                VermittlerRegistrierungsDokumentId = 1
            },new Dokument
            {
                Id = ++_dokumentId,
                Name = "34D-Nachweis",
                DokumentenArt = context.DokumentArtSet
                    .First(das => das.Name == "34D-Nachweis"),
                Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen,
                FileExtension = FileExtension.pdf,
                Data = BeispielDokumente.Schufa,
                VermittlerRegistrierungsDokumentId = 1
            },new Dokument
            {
                Id = ++_dokumentId,
                Name = "Gewerbeanmeldung",
                DokumentenArt = context.DokumentArtSet
                    .First(das => das.Name == "Gewerbeanmeldung"),
                Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen,
                FileExtension = FileExtension.pdf,
                Data = BeispielDokumente.Schufa,
                VermittlerRegistrierungsDokumentId = 1
            }, new Dokument
            {
                Id = ++_dokumentId,
                Name = "Persönliche Daten 2",
                DokumentenArt = context.DokumentArtSet.First(das => das.Name == "Persönliche Daten"),
                Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen,
                FileExtension = FileExtension.pdf,
                Data = BeispielDokumente.PersönlicheDaten,
                VermittlerRegistrierungsDokumentId = 2
            }, new Dokument
            {
                Id = ++_dokumentId,
                Name = "Schufa Auskunft 2",
                DokumentenArt = context.DokumentArtSet
                    .First(das => das.Name == "Zustimmung zur Erteilung der Schufa-Auskunft"),
                Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen,
                FileExtension = FileExtension.pdf,
                Data = BeispielDokumente.Schufa,
                VermittlerRegistrierungsDokumentId = 2
            });

            context.SaveChanges();
        }

        //DONT DELETE Bestandsdaten: DokumentArt, Berufe, Berufsstatus, Länder und Titel
        private static void DeleteAllData(InsuranceDbContext context)
        {
            if (context.Adressen.Any())
                context.Adressen.RemoveRange(context.Adressen);
            
            if (context.Ausweise.Any())
                context.Ausweise.RemoveRange(context.Ausweise);

            if (context.Bankverbindungen.Any())
                context.Bankverbindungen.RemoveRange(context.Bankverbindungen);

            if (context.Dokumente.Any())
                context.Dokumente.RemoveRange(context.Dokumente);

            if(context.DokumentArtSet.Any())
                context.DokumentArtSet.RemoveRange(context.DokumentArtSet);

            if (context.GesellschaftSet.Any())
                context.GesellschaftSet.RemoveRange(context.GesellschaftSet);
            
            if (context.Kunden.Any())
                context.Kunden.RemoveRange(context.Kunden);

            if (context.ProvisionssätzeGesellschaft.Any())
                context.ProvisionssätzeGesellschaft.RemoveRange(context.ProvisionssätzeGesellschaft);
            
            if (context.Users.Any())
                context.Users.RemoveRange(context.Users);

            if (context.Versicherungsnehmer.Any())
                context.Versicherungsnehmer.RemoveRange(context.Versicherungsnehmer);

            if (context.Vermittler.Any())
                context.Vermittler.RemoveRange(context.Vermittler);
        }

        private static void GetPreconfiguredVersicherungsnehmer(InsuranceDbContext context)
        {
            context.Versicherungsnehmer.AddRange(
                new Kunde
            {
                Id = 1,
                Familienstand = Familienstand.LE,
                User = new User()
                {
                    Id = ++_userId,
                    EMail = "kunde@localhost",
                    Vorname = "Kunde",
                    Nachname = "KeinVersicherungsnehmer",
                    Anrede = Anrede.Herr
                },
                IstVersicherungsnehmer = true,
                VersichertePerson = false
            }, new Kunde()
            {
                Id = 2,
                Familienstand = Familienstand.EA,
                User = new User()
                {
                    Id = ++_userId,
                    EMail = "Versicherungsnehmer@localhost",
                    Vorname = "Versicherungsnehmer",
                    Nachname = "Nachname",
                    Anrede = Anrede.Herr
                },
                IstVersicherungsnehmer = true,
                VersichertePerson = false
            },new Kunde
            {
                Id = 3,
                Familienstand = Familienstand.LE,
                User = new User()
                {
                    Id = ++_userId,
                    EMail = "Versicherungsnehmer2@localhost",
                    Vorname = "Versicherungsnehmer2",
                    Nachname = "Nachname2",
                    Anrede = Anrede.Herr
                },
                IstVersicherungsnehmer = false,
                VersichertePerson = false
            }, new Kunde()
            {
                Id = 4,
                Familienstand = Familienstand.EA,
                User = new User()
                {
                    Id = ++_userId,
                    EMail = "kunde2@localhost",
                    Vorname = "Kunde2",
                    Nachname = "KeinVersicherungsnehmer",
                    Anrede = Anrede.Herr
                },
                IstVersicherungsnehmer = false,
                VersichertePerson = false
            });
        }

        private static void GetPreconfiguredVermittler(InsuranceDbContext context)
        {
            context.Vermittler.AddRange(
            new Vermittler
            {
                Id = ++_vermittlerId,
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.NeuerVermittler,
                VermittlerNo = "NP-000000",
                AbschlussProvisionssatz = 60,
                BestandsProvisionssatz = 60,
                IhkRegistrierungsnummer = "Registrierungsnummer",
                Kunden = context.Versicherungsnehmer
                    .Where(v => v.Id == 1 || v.Id == 2)
                    .ToList(),
                User = new User()
                {
                    Id = ++_userId,
                    KeycloakIdentifier = new Guid("106ee760-3e54-4fc9-a3b5-f6fc7284842f"),
                    EMail = "KeycloakVermittler2@localhost",
                    Vorname = "KeycloakVermittler",
                    Nachname = "KeycloakMarkler",
                    Anrede = Anrede.Herr,
                    Adresse = new Adresse
                    {
                        Id = ++_adressenId,
                        Straße = "VermittlerStraße2",
                        Hausnummer = "2",
                        Plz = "123",
                        Ort = "Bremen",
                        Land = context.Länder.First(l => l.Name == "Deutschland")
                    }
                },
                Bankverbindung = new Bankverbindung
                {
                    Id = ++_bankverbindungId,
                    IBAN = "DE00000000000000000001",
                    BankName = "Bankname1",
                    BIC = "DEUTDEDB125"
                },
                EinladecodeVermittler = new EinladecodeVermittler
                {
                    Id = ++_einladecodeId,
                    VermittlerId = _vermittlerId,
                    Code = "WgAA55grJGAGagrL2k0fsA=="
                }
            }, new Vermittler
            {
                Id = ++_vermittlerId,
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.NeuerVermittler,
                VermittlerNo = "NP-000001",
                AbschlussProvisionssatz = 60,
                BestandsProvisionssatz = 60,
                IhkRegistrierungsnummer = "Registrierungsnummer",
                User = new User()
                {
                    Id = ++_userId,
                    KeycloakIdentifier = new Guid("d85cb705-dd07-49a1-9f9c-12e1d9b086da"),
                    EMail = "Vermittler3@localhost",
                    Vorname = "Vermittler3",
                    Nachname = "Markler3",
                    Anrede = Anrede.Frau
                },
                Bankverbindung = new Bankverbindung
                {
                    Id = ++_bankverbindungId,
                    IBAN = "DE00000000000000000001",
                    BankName = "Bankname1",
                    BIC = "DEUTDEDB125"
                }
            }, new Vermittler
            {
                Id = ++_vermittlerId,
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.NeuerVermittler,
                VermittlerNo = "NP-000002",
                AbschlussProvisionssatz = 60,
                BestandsProvisionssatz = 60,
                IhkRegistrierungsnummer = "Registrierungsnummer",
                User = new User()
                {
                    Id = ++_userId,
                    KeycloakIdentifier = new Guid("6ea9d190-ff80-4f97-ac3d-b3c462e41410"),
                    EMail = "neuervermittler.mpd@localhost",
                    Vorname = "NeuerVermittler",
                    Nachname = "MitPersönlichenDaten",
                    Anrede = Anrede.Herr,
                    Telefon = "Test0192013810",
                    StaatsangehörigkeitId = 1,
                    Geburtsort = "Azerbadjan",
                    Geburtsdatum = new DateTime(1983, 1,1),
                    Adresse = new Adresse
                    {
                        Id = ++_adressenId,
                        Straße = "VermittlerStraße3",
                        Hausnummer = "3",
                        Plz = "12354",
                        Ort = "Bremen",
                        Land = context.Länder.First(l => l.Name == "Deutschland")
                    }
                },
                Bankverbindung = new Bankverbindung
                {
                    Id = ++_bankverbindungId,
                    IBAN = "DE00000000000000000002",
                    BankName = "Bankname2",
                    BIC = "DEUTDEDB126"
                },
                EinladecodeVermittler = new EinladecodeVermittler
                {
                    Id = ++_einladecodeId,
                    VermittlerId = _vermittlerId,
                    Code = "ITpgwTrw/SwFmg1mHIYNZg=="
                }
            });
        }

        private static void GetPreconfiguredLänder(InsuranceDbContext context)
        {
            string[] länder = Stammdaten.GetLänder();
            
            for (int i = 0; i < länder.Length; ++i)
            {
                context.Länder.Add(new Land()
                {
                    Id = i + 1,
                    Name = länder[i]
                });
            }
        }

        private static void GetPreconfiguredBerufe(InsuranceDbContext context)
        {
            string[] berufe = Stammdaten.GetBerufe();

            for (int i = 0; i < berufe.Length; ++i)
            {
                context.Berufe.Add(new Beruf
                {
                    Id = i + 1,
                    Name = berufe[i]
                });
            }
        }

        private static void GetPreconfiguredDokumentArt(InsuranceDbContext context)
        {
            string[] dokumentArtList = Stammdaten.GetDokumentArtList();
            
            for (int i = 0; i < dokumentArtList.Length; ++i)
            {
                context.DokumentArtSet.Add(new DokumentArt()
                {
                    Id = i + 1,
                    Name = dokumentArtList[i]
                });
            }
        }
        
        private static void GetPreconfiguredTitelSet(InsuranceDbContext context)
        {
            var titel = Stammdaten.GetTitel();

            string firstDimension = "";
            string secondDimension= "";
            
            for (int i = 0; i < titel.GetLength(0); ++i)
            {
                for (int j = 0; j < titel.GetLength(1); ++j)
                {
                    if (j == 0)
                        firstDimension = titel[i, j];
                    else
                        secondDimension = titel[i, j];
                }

                context.TitelSet.Add(new Titel
                {
                    Id = i+1,
                    BezeichnungKurz = firstDimension,
                    Beschreibung = secondDimension
                });
            }
        }
    }
}
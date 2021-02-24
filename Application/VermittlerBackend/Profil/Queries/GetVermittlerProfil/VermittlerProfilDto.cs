using System;
using System.Collections.Generic;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.VermittlerBackend.Profil.Queries.GetVermittlerProfil
{
    public class VermittlerProfilDto : IMapFrom<Vermittler>
    {
        public int Id { get; set; }
        public string Anrede { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Fax { get; set; }
        public int StaatsangehörigkeitId { get; set; }
        public string StaatsangehörigkeitName { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public string Geburtsort { get; set; }
        public string VermittlerRegistrierungsstatus { get; set; }
        public float BestandsProvisionssatz { get; set; }
        public float AbschlussProvisionssatz { get; set; }
        public string Kontoinhaber { get; set; }
        public string IBAN { get; set; }
        public string Bankname { get; set; }
        public string BIC { get; set; }
        public string Straße { get; set; }
        public string Hausnummer { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Land { get; set; }
        public string EingeladenVonEinladecode { get; set; }
        public string Einladecode { get; set; }
        public string IhkRegistrierungsnummer { get; set; }

        public IList<VertragsdokumenteUebersichtDto> VermittlerDokumentenUebersicht { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vermittler, VermittlerProfilDto>()
                .ForMember(dest => dest.Anrede,
                    opt =>
                        opt.MapFrom(src => src.User.Anrede))
                .ForMember(dest => dest.Vorname,
                    opt =>
                        opt.MapFrom(src => src.User.Vorname))
                .ForMember(dest => dest.Nachname,
                    opt =>
                        opt.MapFrom(src => src.User.Nachname))
                .ForMember(dest => dest.Email,
                    opt =>
                        opt.MapFrom(src => src.User.EMail))
                .ForMember(dest => dest.Telefon,
                    opt =>
                        opt.MapFrom(src => src.User.Telefon))
                .ForMember(dest => dest.StaatsangehörigkeitId,
                    opt =>
                        opt.MapFrom(src => src.User.StaatsangehörigkeitId))
                .ForMember(dest => dest.StaatsangehörigkeitName,
                    opt =>
                        opt.MapFrom(src => src.User.Staatsangehörigkeit.Name))
                .ForMember(dest => dest.Geburtsdatum,
                    opt =>
                        opt.MapFrom(src => src.User.Geburtsdatum))
                .ForMember(dest => dest.Geburtsort,
                    opt =>
                        opt.MapFrom(src => src.User.Geburtsort))
                .ForMember(dest => dest.Fax,
                    opt =>
                        opt.MapFrom(src => src.User.Fax))
                .ForMember(dest => dest.Kontoinhaber,
                    opt =>
                        opt.MapFrom(src => src.Bankverbindung.Kontoinhaber))
                .ForMember(dest => dest.IBAN,
                    opt =>
                        opt.MapFrom(src => src.Bankverbindung.IBAN))
                .ForMember(dest => dest.BIC,
                    opt =>
                        opt.MapFrom(src => src.Bankverbindung.BIC))
                .ForMember(dest => dest.Bankname,
                    opt =>
                        opt.MapFrom(src => src.Bankverbindung.BankName))
                .ForMember(dest => dest.Straße,
                    opt =>
                        opt.MapFrom(src => src.User.Adresse.Straße))
                .ForMember(dest => dest.Hausnummer,
                    opt =>
                        opt.MapFrom(src => src.User.Adresse.Hausnummer))
                .ForMember(dest => dest.Ort,
                    opt =>
                        opt.MapFrom(src => src.User.Adresse.Ort))
                .ForMember(dest => dest.Plz,
                    opt =>
                        opt.MapFrom(src => src.User.Adresse.Plz))
                .ForMember(dest => dest.Land,
                    opt =>
                        opt.MapFrom(src => src.User.Adresse.Land.Name))
                .ForMember(dest => dest.VermittlerDokumentenUebersicht,
                    opt =>
                        opt.MapFrom(src => src.RegistrierungsDokumente))
                .ForMember(dest => dest.Einladecode,
                    opt =>
                        opt.MapFrom(src => src.EinladecodeVermittler.Code))
                .ForMember(dest => dest.EingeladenVonEinladecode,
                    opt =>
                        opt.MapFrom(src => src.EingeladenVon.Code))
                .ForMember(dest => dest.IhkRegistrierungsnummer,
                    opt =>
                        opt.MapFrom(src => src.IhkRegistrierungsnummer));
        }
    }
}
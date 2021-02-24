using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.InsuranceAdmin.Query.GetVermittler
{
    public class VermittlerÜbersichtDto : IMapFrom<Vermittler>
    {
        public int Id { get; set; }
        public string EMail { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Anrede { get; set; }
        public string Registrierungsstatus { get; set; }
        public float BestandsProvisionssatz { get; set; }
        public float AbschlussProvisionssatz { get; set; }
        public bool IstAktiv { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vermittler, VermittlerÜbersichtDto>()
                .ForMember(dest => dest.Anrede,
                    opt =>
                        opt.MapFrom(src => src.User.Anrede))
                .ForMember(dest => dest.Vorname,
                    opt =>
                        opt.MapFrom(src => src.User.Vorname))
                .ForMember(dest => dest.Nachname,
                    opt =>
                        opt.MapFrom(src => src.User.Nachname))
                .ForMember(dest => dest.EMail,
                    opt =>
                        opt.MapFrom(src => src.User.EMail))
                .ForMember(dest => dest.Registrierungsstatus,
                    opt =>
                        opt.MapFrom(src => src.VermittlerRegistrierungsstatus));
        }
    }
}
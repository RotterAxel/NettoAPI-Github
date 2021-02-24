using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.VermittlerBackend.Profil.Queries.GetVermittlerPolicy
{
    public class VermittlerPolicyDto : IMapFrom<Vermittler>
    {
        public bool IstAktiv { get; set; }
        public string Registrierungsstatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vermittler, VermittlerPolicyDto>()
                .ForMember(dest => dest.IstAktiv,
                    opt =>
                        opt.MapFrom(src => src.IstAktiv))
                .ForMember(dest => dest.Registrierungsstatus,
                    opt =>
                        opt.MapFrom(src => src.VermittlerRegistrierungsstatus));
        }
    }
}
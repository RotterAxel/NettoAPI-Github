using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.InsuranceAdmin.Query.GetKunden
{
    public class KundenÜbersichtDto : IMapFrom<Kunde>
    {
        public int Id { get; set; }
        public string Familienstand { get; set; }
        
        public UserKundenübersichtDto UserKundenübersichtDto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Kunde, KundenÜbersichtDto>()
                .ForMember(dest => dest.UserKundenübersichtDto,
                    opt =>
                        opt.MapFrom(src => src.User));
        }
    }
}
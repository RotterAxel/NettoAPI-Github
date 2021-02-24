using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.VermittlerBackend.Profil.Queries.GetVermittlerGesellschaften
{
    public class VermittlerGesellschaftDto : IMapFrom<VermittlerGesellschafft>
    {
        public int VermittlerId { get; set; }

        public string GesellschaftName { get; set; }

        public string VermittlerNo { get; set; }
        public double Abschlussvergütung { get; set; }
        public double Bestandsvergütung { get; set; }
        public int MaxLaufzeitVergütung { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VermittlerGesellschafft, VermittlerGesellschaftDto>()
                .ForMember(dest => dest.GesellschaftName,
                    opt =>
                        opt.MapFrom(src => src.Gesellschaft.Name));
        }
    }
}
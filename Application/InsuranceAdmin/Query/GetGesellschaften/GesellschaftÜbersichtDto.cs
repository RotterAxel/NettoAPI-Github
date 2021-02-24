using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.InsuranceAdmin.Query.GetGesellschaften
{
    public class GesellschaftÜbersichtDto : IMapFrom<Gesellschaft>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Gesellschaft, GesellschaftÜbersichtDto>();
        }
    }
}
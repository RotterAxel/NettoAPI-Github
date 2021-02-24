using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.Stammdaten.Queries.GetLänder
{
    public class LandDto : IMapFrom<Land>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Land, LandDto>();
        }
    }
}
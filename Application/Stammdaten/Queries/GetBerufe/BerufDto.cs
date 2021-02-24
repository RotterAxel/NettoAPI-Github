using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.Stammdaten.Queries.GetBerufe
{
    public class BerufDto : IMapFrom<Beruf>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Beruf, BerufDto>();
        }
    }
}
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.Stammdaten.Queries.GetTitel
{
    public class TitelDto : IMapFrom<Titel>
    {
        public int Id { get; set; }
        public string BezeichnungKurz { get; set; }
        public string Beschreibung { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Titel, TitelDto>();
        }
    }
}
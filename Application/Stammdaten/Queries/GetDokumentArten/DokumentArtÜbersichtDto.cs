using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.Stammdaten.Queries.GetDokumentArten
{
    public class DokumentArtÜbersichtDto : IMapFrom<DokumentArt>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DokumentArt, DokumentArtÜbersichtDto>();
        }
    }
}
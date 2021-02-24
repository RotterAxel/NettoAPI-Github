using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.VermittlerBackend.Profil.Queries.GetVermittlerProfil
{
    public class VertragsdokumenteUebersichtDto: IMapFrom<Dokument>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DokuemntenArtId { get; set; }
        public string DokumentenArtName { get; set; }
        public string Bearbeitungsstatus { get; set; }
        public string FileExtension { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Dokument, VertragsdokumenteUebersichtDto>()
                .ForMember(dest => dest.DokuemntenArtId,
                    opt =>
                        opt.MapFrom(src => src.DokumentenArt.Id))
                .ForMember(dest => dest.DokumentenArtName,
                    opt =>
                        opt.MapFrom(src => src.DokumentenArt.Name))
                .ForMember(dest => dest.Bearbeitungsstatus,
                    opt =>
                        opt.MapFrom(src => src.Bearbeitungsstatus));
        }
    }
}
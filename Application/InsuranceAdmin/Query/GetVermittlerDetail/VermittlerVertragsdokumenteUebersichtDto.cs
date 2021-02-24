using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.InsuranceAdmin.Query.GetVermittlerDetail
{
    public class VermittlerVertragsdokumenteUebersichtDto : IMapFrom<Dokument>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DokuemntenArtId { get; set; }
        public string DokumentenArt { get; set; }
        public string Bearbeitungsstatus { get; set; }
        public string FileExtension { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Dokument, VermittlerVertragsdokumenteUebersichtDto>()
                .ForMember(dest => dest.DokuemntenArtId,
                    opt =>
                        opt.MapFrom(src => src.DokumentenArt.Id))
                .ForMember(dest => dest.DokumentenArt,
                    opt =>
                        opt.MapFrom(src => src.DokumentenArt.Name))
                .ForMember(dest => dest.Bearbeitungsstatus,
                    opt =>
                        opt.MapFrom(src => src.Bearbeitungsstatus));
        }
    }
}
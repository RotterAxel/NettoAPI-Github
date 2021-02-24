using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Insurance;

namespace Application.InsuranceAdmin.Query.GetKunden
{
    public class UserKundenübersichtDto : IMapFrom<User>
    {
        public string EMail { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Anrede { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserKundenübersichtDto>();
        }
    }
}
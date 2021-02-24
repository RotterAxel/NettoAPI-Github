using System;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Common;

namespace Application.Common.Models
{
    public class AuditableDto : IMapFrom<AuditableEntity>
    {
        public DateTime RowVersion { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuditableEntity, AuditableDto>()
                .ForMember(dest => dest.RowVersion,
                    opt =>
                        opt.MapFrom(src => src.RowVersion));
        }
    }
}
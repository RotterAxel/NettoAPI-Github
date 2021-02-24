using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Stammdaten.Queries.GetLänder
{
    public class GetLänderQuery : IRequest<IList<LandDto>>{ }
    
    public class GetLänderQueryHandler : IRequestHandler<GetLänderQuery, IList<LandDto>>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;

        public GetLänderQueryHandler(
            IInsuranceDbContext insuranceDbContext,
            IMapper mapper)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
        }

        public async Task<IList<LandDto>> Handle(GetLänderQuery request,
            CancellationToken cancellationToken)
        {
            return await _insuranceDbContext.Länder
                .ProjectTo<LandDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
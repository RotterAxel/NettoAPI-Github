using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Stammdaten.Queries.GetBerufe
{
    public class GetBerufeQuery : IRequest<IList<BerufDto>> { }
    
    public class GetBerufeQueryHandler : IRequestHandler<GetBerufeQuery, IList<BerufDto>>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;

        public GetBerufeQueryHandler(
            IInsuranceDbContext insuranceDbContext,
            IMapper mapper)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
        }

        public async Task<IList<BerufDto>> Handle(GetBerufeQuery request,
            CancellationToken cancellationToken)
        {
            return await _insuranceDbContext.Berufe
                .ProjectTo<BerufDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
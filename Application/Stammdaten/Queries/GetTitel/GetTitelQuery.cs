using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Stammdaten.Queries.GetTitel
{
    public class GetTitelQuery : IRequest<IList<TitelDto>> { }
    
    public class GetTitelQueryHandler : IRequestHandler<GetTitelQuery, IList<TitelDto>>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;

        public GetTitelQueryHandler(
            IInsuranceDbContext insuranceDbContext,
            IMapper mapper)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
        }

        public async Task<IList<TitelDto>> Handle(GetTitelQuery request,
            CancellationToken cancellationToken)
        {
            return await _insuranceDbContext.TitelSet
                .ProjectTo<TitelDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
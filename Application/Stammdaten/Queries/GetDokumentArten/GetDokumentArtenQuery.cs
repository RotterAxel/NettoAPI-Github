using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Stammdaten.Queries.GetDokumentArten
{
    public class GetDokumentArtenQuery : IRequest<IList<DokumentArtÜbersichtDto>> { }
    
    public class GetDokumentArtenQueryHandler : IRequestHandler<GetDokumentArtenQuery, IList<DokumentArtÜbersichtDto>>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;

        public GetDokumentArtenQueryHandler(
            IInsuranceDbContext insuranceDbContext,
            IMapper mapper)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
        }

        public async Task<IList<DokumentArtÜbersichtDto>> Handle(GetDokumentArtenQuery request,
            CancellationToken cancellationToken)
        {
            return await _insuranceDbContext.DokumentArtSet
                .ProjectTo<DokumentArtÜbersichtDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
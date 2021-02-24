using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.VermittlerBackend.Profil.Queries.GetVermittlerGesellschaften
{
    public class GetVermittlerGesellschaftenQuery : IRequest<List<VermittlerGesellschaftDto>> { }

    public class GetVermittlerGesellschaftenQueryHandler : IRequestHandler<GetVermittlerGesellschaftenQuery,
        List<VermittlerGesellschaftDto>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IInsuranceDbContext _insuranceDbContext;

        public GetVermittlerGesellschaftenQueryHandler(
            ICurrentUserService currentUserService,
            IMapper mapper,
            IInsuranceDbContext insuranceDbContext)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _insuranceDbContext = insuranceDbContext;
        }
        
        public async Task<List<VermittlerGesellschaftDto>> Handle(GetVermittlerGesellschaftenQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IstVermittler)
                throw new UnauthorizedAccessException();

            var vermittler =
                await _insuranceDbContext.Vermittler.FirstAsync(v => v.UserId == _currentUserService.ApiUserId);
            
            return await _insuranceDbContext.VermittlerGesellschafften
                .Include(vg => vg.Gesellschaft)
                .ProjectTo<VermittlerGesellschaftDto>(_mapper.ConfigurationProvider)
                .Where(vg => vg.VermittlerId == vermittler.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.InsuranceAdmin.Query.GetGesellschaften
{
    public class GetGesellschaftenQuery : IRequest<IList<GesellschaftÜbersichtDto>> { }

    public class GetGesellschaftenQueryHandler : 
        IRequestHandler<GetGesellschaftenQuery, IList<GesellschaftÜbersichtDto>>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetGesellschaftenQueryHandler(
            IInsuranceDbContext insuranceDbContext,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _insuranceDbContext = insuranceDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        
        public async Task<IList<GesellschaftÜbersichtDto>> Handle(GetGesellschaftenQuery request, CancellationToken cancellationToken)
        {
            if(!(_currentUserService.IsAdmin || _currentUserService.IsBearbeiter))
                throw new UnauthorizedAccessException();

            return await _insuranceDbContext.GesellschaftSet
                .ProjectTo<GesellschaftÜbersichtDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
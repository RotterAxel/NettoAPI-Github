using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.InsuranceAdmin.Query.GetVermittler
{
    public class GetVermittlerQuery : IRequest<IList<VermittlerÜbersichtDto>> { }

    public class GetVermittlerQueryHandler : IRequestHandler<GetVermittlerQuery, IList<VermittlerÜbersichtDto>>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetVermittlerQueryHandler(IInsuranceDbContext insuranceDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IList<VermittlerÜbersichtDto>> Handle(GetVermittlerQuery request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAdmin || _currentUserService.IsBearbeiter)
            {
                return await _insuranceDbContext.Vermittler
                    .Include(v => v.User)
                    .ProjectTo<VermittlerÜbersichtDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

            throw new UnauthorizedAccessException();
        }
    }
}
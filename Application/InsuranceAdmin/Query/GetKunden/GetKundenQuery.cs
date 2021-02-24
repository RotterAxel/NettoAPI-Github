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

namespace Application.InsuranceAdmin.Query.GetKunden
{
    public class GetKundenQuery : IRequest<IList<KundenÜbersichtDto>> { }

    public class GetKundenQueryHandler : IRequestHandler<GetKundenQuery, IList<KundenÜbersichtDto>>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetKundenQueryHandler(IInsuranceDbContext insuranceDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IList<KundenÜbersichtDto>> Handle(GetKundenQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAdmin || _currentUserService.IsBearbeiter)
            {
                return await _insuranceDbContext.Versicherungsnehmer
                    .Include(vn => vn.User)
                    .ProjectTo<KundenÜbersichtDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

            if (_currentUserService.IstVermittler)
            {
                var kunden = _insuranceDbContext.Vermittler
                    .Include(v => v.Kunden)
                    .ThenInclude(k => k.User)
                    .FirstOrDefault(v => _currentUserService != null && v.User.KeycloakIdentifier ==
                        new Guid(_currentUserService.KeycloakUserId))?.Kunden;

                return _mapper.Map<IList<KundenÜbersichtDto>>(kunden);

            }
            
            throw new UnauthorizedAccessException();
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.VermittlerBackend.Profil.Queries.GetVermittlerPolicy
{
    public class GetVermittlerPolicyQuery : IRequest<VermittlerPolicyDto> { }

    public class GetVermittlerPolicyQueryHandler : IRequestHandler<GetVermittlerPolicyQuery, VermittlerPolicyDto>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetVermittlerPolicyQueryHandler(IInsuranceDbContext insuranceDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<VermittlerPolicyDto> Handle(GetVermittlerPolicyQuery request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IstVermittler)
            {
                //Prüfen ob der Vermittle als User mit der KeycloakID auf unserer DB schon existiert
                //Keine KeycloakId == noch kein Registrierungsprozess abgeschlossen
                var userFromRepo = await _insuranceDbContext.Users.FirstOrDefaultAsync(u =>
                    u.KeycloakIdentifier == Guid.Parse(_currentUserService.KeycloakUserId), cancellationToken);

                if (userFromRepo != null)
                {
                    return _mapper.Map<VermittlerPolicyDto>(await _insuranceDbContext.Vermittler
                        .Include(v => v.User)
                        .FirstOrDefaultAsync(v => v.User.Id == userFromRepo.Id));

                }
                
                return new VermittlerPolicyDto
                {
                    Registrierungsstatus = VermittlerRegistrierungsstatus.NeuerVermittler.ToString()
                };
            }

            throw new UnauthorizedAccessException();
        }
    }
}
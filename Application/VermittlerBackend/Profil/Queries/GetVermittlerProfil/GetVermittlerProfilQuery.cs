using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.VermittlerBackend.Profil.Queries.GetVermittlerProfil
{
    public class GetVermittlerProfilQuery : IRequest<VermittlerProfilDto> { }

    public class GetVermittlerProfilQueryHandler : IRequestHandler<GetVermittlerProfilQuery, VermittlerProfilDto>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetVermittlerProfilQueryHandler(
            IInsuranceDbContext insuranceDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<VermittlerProfilDto> Handle(GetVermittlerProfilQuery request, 
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IstVermittler && _currentUserService.ApiUserId != null)
            {
                var vermittlerFromRepo = await _insuranceDbContext.Vermittler
                    .Include(v => v.User)
                    .ThenInclude(u => u.Staatsangehörigkeit)
                    .Include(v => v.User)
                    .ThenInclude(u => u.Adresse)
                    .ThenInclude(a => a.Land)
                    .Include(v => v.Bankverbindung)
                    .Include(v => v.RegistrierungsDokumente)
                    .ThenInclude(d => d.DokumentenArt)
                    .Include(v => v.EinladecodeVermittler)
                    .Include(v => v.EingeladenVon)
                    .FirstAsync(v => v.User.Id == _currentUserService.ApiUserId, cancellationToken);

                //Vermittler dürfen nur andere einladen wenn Ihr Registrierungsstatus == RegistrierungGenehmigt
                if (vermittlerFromRepo.VermittlerRegistrierungsstatus !=
                    VermittlerRegistrierungsstatus.RegistrierungGenehmigt)
                    vermittlerFromRepo.EinladecodeVermittler = null;

                return _mapper.Map<VermittlerProfilDto>(vermittlerFromRepo);
            }
            
            if(_currentUserService.IstVermittler && _currentUserService.ApiUserId == null)
                throw new NotFoundException("Keycloak Vermittler existiert nicht auf der API.");
            
            throw new UnauthorizedAccessException();
        }
    }
}
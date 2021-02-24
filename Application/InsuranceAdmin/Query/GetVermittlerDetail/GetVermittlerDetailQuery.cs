using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.InsuranceAdmin.Query.GetVermittlerDetail
{
    public class GetVermittlerDetailQuery : IRequest<VermittlerDetailansichtDto>
    {
        public int VermittlerId { get; set; }
    }

    public class GetVermittlerDetailQueryHandler : IRequestHandler<GetVermittlerDetailQuery, VermittlerDetailansichtDto>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetVermittlerDetailQueryHandler(IInsuranceDbContext insuranceDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<VermittlerDetailansichtDto> Handle(GetVermittlerDetailQuery request, 
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAdmin || _currentUserService.IsBearbeiter)
            {
                if(!await _insuranceDbContext.Vermittler.AnyAsync(v => v.Id == request.VermittlerId,
                    cancellationToken))
                    throw new NotFoundException($"Vermittler with Id {request.VermittlerId} does not exist.");

                return await _insuranceDbContext.Vermittler
                    .Include(v => v.User)
                    .ThenInclude(u => u.Adresse)
                    .ThenInclude(a => a.Land)
                    .Include(v => v.User)
                    .ThenInclude(u => u.Staatsangehörigkeit)
                    .Include(v => v.Bankverbindung)
                    .Include(v => v.RegistrierungsDokumente)
                    .ThenInclude(d => d.DokumentenArt)
                    .Include(v => v.EinladecodeVermittler)
                    .ProjectTo<VermittlerDetailansichtDto>(_mapper.ConfigurationProvider)
                    .FirstAsync(v => v.Id == request.VermittlerId, cancellationToken);
            }
            
            throw new UnauthorizedAccessException();
        }
    }
}
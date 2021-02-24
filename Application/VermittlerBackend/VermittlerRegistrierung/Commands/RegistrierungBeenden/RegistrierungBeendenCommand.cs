using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.VermittlerBackend.VermittlerRegistrierung.Commands.RegistrierungBeenden
{
    public class RegistrierungBeendenCommand : IRequest<int>
    {
    }

    public class RegistrierungBeendenCommandHandler : IRequestHandler<RegistrierungBeendenCommand, int>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IInsuranceDbContext _insuranceDbContext;

        private readonly List<string> _erfordelicheDokumentArten = new List<string>()
        {
            "34D-Nachweis",
            "Ausweiskopie",
            "Gewerbeanmeldung"
        };

        public RegistrierungBeendenCommandHandler(ICurrentUserService currentUserService,
            IInsuranceDbContext insuranceDbContext)
        {
            _currentUserService = currentUserService;
            _insuranceDbContext = insuranceDbContext;
        }

        public async Task<int> Handle(RegistrierungBeendenCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IstVermittler || _currentUserService.ApiUserId == null)
                throw new UnauthorizedAccessException();

            var vemittlerRegistrierungsstatusToUpdate = await _insuranceDbContext.Vermittler
                .Include(v => v.RegistrierungsDokumente)
                .ThenInclude(d => d.DokumentenArt)
                .Include(v => v.User)
                .FirstAsync(v => v.UserId == _currentUserService.ApiUserId,
                    cancellationToken);

            if(!vemittlerRegistrierungsstatusToUpdate.RegistrierungsDokumente.Any())
                throw new BadRequestException(
                    "Vermittler hat keine Registrierungsdokumente vorhanden");

            foreach (var dokumentArt in _erfordelicheDokumentArten)
            {
                if (vemittlerRegistrierungsstatusToUpdate.RegistrierungsDokumente
                    .Any(rd => rd.DokumentenArt.Name == dokumentArt))
                    continue;
                
                throw new BadRequestException(
                    $"{dokumentArt} fehlt, bitte stellen sie sicher, dass Dokumente 34D-Nachweis, Ausweiskopie " +
                    "und Gewerbeanmeldung hochgeladen worden");
            }

            if (vemittlerRegistrierungsstatusToUpdate.VermittlerRegistrierungsstatus
                != VermittlerRegistrierungsstatus.NeuerVermittler)
                throw new BadRequestException("Nur NeuerVermittler darf seine Registrierung beenden");

            vemittlerRegistrierungsstatusToUpdate.VermittlerRegistrierungsstatus =
                VermittlerRegistrierungsstatus.RegistrierungDurchgeführt;

            return vemittlerRegistrierungsstatusToUpdate.Id;
        }
    }
}
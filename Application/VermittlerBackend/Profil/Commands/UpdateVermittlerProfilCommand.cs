using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities.Insurance;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.VermittlerBackend.Profil.Commands
{
    public class UpdateVermittlerProfilCommand : IRequest<int>
    {
        public string Anrede { get; set; }
        public string Telefon { get; set; }
        public int StaatsangehörigkeitId { get; set; }
        public string Geburtsort { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public string IhkRegistrierungsnummer { get; set; }
        public string Kontoinhaber { get; set; }
        public string Bankname { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
    }

    public class UpdateVermittlerProfilCommandHandler : IRequestHandler<UpdateVermittlerProfilCommand, int>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly ICurrentUserService _currentUserService;

        public UpdateVermittlerProfilCommandHandler(
            IInsuranceDbContext insuranceDbContext,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _currentUserService = currentUserService;
        }
        
        public async Task<int> Handle(UpdateVermittlerProfilCommand command, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IstVermittler)
                throw new UnauthorizedAccessException();
            
            if(_currentUserService.ApiUserId == null)
                throw new BadRequestException("Vermittler existier nicht auf der API.");

            if (!await _insuranceDbContext.Länder.AnyAsync(l => l.Id == command.StaatsangehörigkeitId,
                cancellationToken))
                throw new NotFoundException(nameof(Land), command.StaatsangehörigkeitId);
            
            var vermittlerFromDb = await _insuranceDbContext.Vermittler
                .Include(v => v.User)
                .Include(v => v.Bankverbindung)
                .Include(v => v.RegistrierungsDokumente)
                .FirstAsync(v => v.UserId == _currentUserService.ApiUserId, cancellationToken);

            //Update Fields
            vermittlerFromDb.User.Anrede = EnumExtensions.ParseEnumFromString<Anrede>(command.Anrede);
            vermittlerFromDb.User.Telefon = command.Telefon;
            vermittlerFromDb.User.StaatsangehörigkeitId = command.StaatsangehörigkeitId;
            vermittlerFromDb.User.Geburtsort = command.Geburtsort;
            vermittlerFromDb.User.Geburtsdatum = command.Geburtsdatum;
            vermittlerFromDb.IhkRegistrierungsnummer = command.IhkRegistrierungsnummer;
            vermittlerFromDb.Bankverbindung.Kontoinhaber = command.Kontoinhaber;
            vermittlerFromDb.Bankverbindung.BankName = command.Bankname;
            vermittlerFromDb.Bankverbindung.IBAN = command.Iban;
            vermittlerFromDb.Bankverbindung.BIC = command.Bic;

            await _insuranceDbContext.SaveChangesAsync(cancellationToken);
            
            return vermittlerFromDb.Id;
        }
    }
}
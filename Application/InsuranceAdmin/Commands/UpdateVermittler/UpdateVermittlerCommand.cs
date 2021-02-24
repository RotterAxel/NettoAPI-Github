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

namespace Application.InsuranceAdmin.Commands.UpdateVermittler
{
    public class UpdateVermittlerCommand : IRequest
    {
        public int Id { get; set; }
        public string Anrede { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Telefon { get; set; }
        public string Fax { get; set; }
        public string VermittlerRegistrierungsstatus { get; set; }
        public float BestandsProvisionssatz { get; set; }
        public float AbschlussProvisionssatz { get; set; }
        public bool IstAktiv { get; set; }
        public string IBAN { get; set; }
        public string Bankname { get; set; }
        public string BIC { get; set; }
        public string Straße { get; set; }
        public string Hausnummer { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Land { get; set; }
    }

    public class UpdateVermittlerCommandHandler : IRequestHandler<UpdateVermittlerCommand>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly ICurrentUserService _currentUserService;

        public UpdateVermittlerCommandHandler(IInsuranceDbContext insuranceDbContext,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateVermittlerCommand command, CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAdmin || _currentUserService.IsBearbeiter)
            {
                var entity = await _insuranceDbContext.Vermittler
                    .Include(v => v.User)
                    .ThenInclude(u => u.Adresse)
                    .ThenInclude(a => a.Land)
                    .Include(v => v.Bankverbindung)
                    .FirstOrDefaultAsync(v => v.Id == command.Id,
                        cancellationToken);

                if (entity == null)
                    throw new NotFoundException(nameof(Vermittler), command.Id);

                entity.IstAktiv = command.IstAktiv;
                entity.VermittlerRegistrierungsstatus =
                    EnumExtensions
                        .ParseEnumFromString<VermittlerRegistrierungsstatus>(command.VermittlerRegistrierungsstatus);
                entity.AbschlussProvisionssatz = command.AbschlussProvisionssatz;
                entity.BestandsProvisionssatz = command.BestandsProvisionssatz;
                entity.User.Anrede = EnumExtensions.ParseEnumFromString<Anrede>(command.Anrede);
                entity.User.Vorname = command.Vorname;
                entity.User.Nachname = command.Nachname;
                entity.User.Telefon = command.Telefon;
                entity.User.Fax = command.Fax;
                entity.Bankverbindung.IBAN = command.IBAN;
                entity.Bankverbindung.BIC = command.BIC;
                entity.Bankverbindung.BankName = command.Bankname;
                entity.User.Adresse.Straße = command.Straße;
                entity.User.Adresse.Hausnummer = command.Hausnummer;
                entity.User.Adresse.Plz = command.Plz;
                entity.User.Adresse.Ort = command.Ort;
                entity.User.Adresse.Land.Name = command.Land;

                await _insuranceDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

            throw new UnauthorizedAccessException();
        }
    }
}
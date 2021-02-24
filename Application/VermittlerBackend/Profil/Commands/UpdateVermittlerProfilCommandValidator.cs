using Application.Common.Validators;
using Domain.Enums;
using FluentValidation;

namespace Application.VermittlerBackend.Profil.Commands
{
    public class UpdateVermittlerProfilCommandValidator : AbstractValidator<UpdateVermittlerProfilCommand>
    {
        public UpdateVermittlerProfilCommandValidator()
        {
            RuleFor(v => v.Anrede)
                .IsEnumName(typeof(Anrede)).WithMessage("Existiert nicht");

            RuleFor(v => v.Telefon)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
            
            RuleFor(v => v.Geburtsort)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
            
            RuleFor(v => v.Geburtsdatum)
                .ValidesGeburtsdatum("Geburtsdatum liegt zu weit in der Vergangenheit oder in der Zukunft");
            
            RuleFor(v => v.IhkRegistrierungsnummer)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");

            RuleFor(v => v.Bankname)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
            
            RuleFor(v => v.Iban)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
            
            RuleFor(v => v.Bic)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
        }
    }
}
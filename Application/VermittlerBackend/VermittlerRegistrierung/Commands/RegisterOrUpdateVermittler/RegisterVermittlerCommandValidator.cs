using Application.Common.Validators;
using Domain.Enums;
using FluentValidation;

namespace Application.VermittlerBackend.VermittlerRegistrierung.Commands.RegisterOrUpdateVermittler
{
    public class RegisterVermittlerCommandValidator : AbstractValidator<RegisterOrUpdateVermittlerCommand>
    {
        public RegisterVermittlerCommandValidator()
        {
            RuleFor(v => v.Anrede)
                .IsEnumName(typeof(Anrede)).WithMessage("Existiert nicht");
            
            RuleFor(v => v.Vorname)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
            
            RuleFor(v => v.Nachname)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
            
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
            
            RuleFor(v => v.Straße)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
            
            RuleFor(v => v.Hausnummer)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
            
            RuleFor(v => v.Plz)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
            
            RuleFor(v => v.Ort)
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
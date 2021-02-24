using Domain.Enums;
using FluentValidation;

namespace Application.InsuranceAdmin.Commands.UpdateVermittler
{
    public class UpdateVermittlerCommandValidator : AbstractValidator<UpdateVermittlerCommand>
    {
        public UpdateVermittlerCommandValidator()
        {
            RuleFor(v => v.Anrede)
                .IsEnumName(typeof(Anrede)).WithMessage("Existiert nicht");
            
            RuleFor(v => v.Vorname)
                .NotNull().WithMessage("Ist erforderlich.")
                .NotEmpty().WithMessage("Darf nicht leer sein");
            
            RuleFor(v => v.Nachname)
                .NotNull().WithMessage("Ist erforderlich.")
                .NotEmpty().WithMessage("Darf nicht leer sein");

            RuleFor(v => v.AbschlussProvisionssatz)
                .LessThanOrEqualTo(60).WithMessage("Darf nicht höher als 60% sein")
                .GreaterThanOrEqualTo(0).WithMessage("Darf nicht niedrieger als 0% sein");
            
            RuleFor(v => v.BestandsProvisionssatz)
                .LessThanOrEqualTo(60).WithMessage("Darf nicht höher als 60% sein")
                .GreaterThanOrEqualTo(0).WithMessage("Darf nicht niedrieger als 0% sein");

            RuleFor(v => v.VermittlerRegistrierungsstatus)
                .IsEnumName(typeof(VermittlerRegistrierungsstatus))
                .WithMessage("Existiert nicht");

            RuleFor(v => v.IBAN)
                .NotNull().WithMessage("Ist erforderlich.")
                .NotEmpty().WithMessage("Darf nicht leer sein");
            
            RuleFor(v => v.Bankname)
                .NotNull().WithMessage("Ist erforderlich.")
                .NotEmpty().WithMessage("Darf nicht leer sein");
            
            RuleFor(v => v.BIC)
                .NotNull().WithMessage("Ist erforderlich.")
                .NotEmpty().WithMessage("Darf nicht leer sein");
            
            RuleFor(v => v.Straße)
                .NotNull().WithMessage("Ist erforderlich.")
                .NotEmpty().WithMessage("Darf nicht leer sein");
            
            RuleFor(v => v.Hausnummer)
                .NotNull().WithMessage("Ist erforderlich.")
                .NotEmpty().WithMessage("Darf nicht leer sein");
            
            RuleFor(v => v.Plz)
                .NotNull().WithMessage("Ist erforderlich.")
                .NotEmpty().WithMessage("Darf nicht leer sein");
            
            RuleFor(v => v.Ort)
                .NotNull().WithMessage("Ist erforderlich.")
                .NotEmpty().WithMessage("Darf nicht leer sein");
            
            RuleFor(v => v.Land)
                .NotNull().WithMessage("Ist erforderlich.")
                .NotEmpty().WithMessage("Darf nicht leer sein");
        }
    }
}
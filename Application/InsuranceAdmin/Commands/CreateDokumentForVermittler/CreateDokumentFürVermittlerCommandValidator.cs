using Domain.Enums;
using FluentValidation;

namespace Application.InsuranceAdmin.Commands.CreateDokumentForVermittler
{
    public class CreateDokumentFürVermittlerCommandValidator : AbstractValidator<CreateDokumentFürVermittlerCommand>
    {
        public CreateDokumentFürVermittlerCommandValidator()
        {
            RuleFor(v => v.Bearbeitungsstatus)
                .IsEnumName(typeof(Bearbeitungsstatus)).WithMessage("Existiert nicht");

            RuleFor(v => v.FileExtension)
                .IsEnumName(typeof(FileExtension)).WithMessage("Existiert nicht");
            
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
            
            RuleFor(v => v.Data)
                .NotEmpty().WithMessage("Darf nicht leer sein")
                .NotNull().WithMessage("Ist erforderlich");
        }
    }
}
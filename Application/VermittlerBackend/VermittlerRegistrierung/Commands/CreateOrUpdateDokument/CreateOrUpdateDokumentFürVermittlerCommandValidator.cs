using Domain.Enums;
using FluentValidation;

namespace Application.VermittlerBackend.VermittlerRegistrierung.Commands.CreateOrUpdateDokument
{
    public class CreateOrUpdateDokumentFürVermittlerCommandValidator : 
        AbstractValidator<CreateOrUpdateDokumentFürVermittlerCommand>
    {
        public CreateOrUpdateDokumentFürVermittlerCommandValidator()
        {
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
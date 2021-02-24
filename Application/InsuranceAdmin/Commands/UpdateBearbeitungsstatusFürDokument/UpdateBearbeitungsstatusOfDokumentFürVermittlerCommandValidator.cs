using Domain.Enums;
using FluentValidation;

namespace Application.InsuranceAdmin.Commands.UpdateBearbeitungsstatusFürDokument
{
    public class UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandValidator : 
        AbstractValidator<UpdateBearbeitungsstatusOfDokumentFürVermittlerCommand>
    {
        public UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandValidator()
        {
            RuleFor(v => v.Bearbeitungsstatus)
                .IsEnumName(typeof(Bearbeitungsstatus)).WithMessage("Existiert nicht");
        }
    }
}
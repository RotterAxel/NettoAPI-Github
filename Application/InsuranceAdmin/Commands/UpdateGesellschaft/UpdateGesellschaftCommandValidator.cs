using FluentValidation;

namespace Application.InsuranceAdmin.Commands.UpdateGesellschaft
{
    public class UpdateGesellschaftCommandValidator : AbstractValidator<UpdateGesellschaftCommand>
    {
        public UpdateGesellschaftCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
        }
    }
}
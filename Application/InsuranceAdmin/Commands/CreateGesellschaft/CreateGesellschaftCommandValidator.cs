using FluentValidation;

namespace Application.InsuranceAdmin.Commands.CreateGesellschaft
{
    public class CreateGesellschaftCommandValidator: AbstractValidator<CreateGesellschaftCommand>
    {
        public CreateGesellschaftCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Darf nicht leer sein.")
                .NotNull().WithMessage("Ist erforderlich");
        }
    }
}
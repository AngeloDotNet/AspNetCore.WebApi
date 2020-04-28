using AEP_WebApi.Models;
using FluentValidation;

namespace AEP_WebApi.Validators
{
    public class ComuniValidator : AbstractValidator<Comuni>
    {
        public ComuniValidator()
        {
            RuleFor(m => m.Comune)
                .NotEmpty().WithMessage("Il comune è obbligatorio");

            RuleFor(m => m.Cap)
                .NotEmpty().WithMessage("Il cap è obbligatorio")
                .MinimumLength(5).WithMessage("Il cap deve avere {MinLength} caratteri")
                .MaximumLength(5).WithMessage("Il cap deve avere {MaxLength} caratteri");
            
            RuleFor(m => m.Provincia)
                .NotEmpty().WithMessage("La provincia è obbligatoria")
                .MinimumLength(2).WithMessage("La provincia deve avere {MinLength} caratteri")
                .MaximumLength(2).WithMessage("La provincia deve avere {MaxLength} caratteri");

            RuleFor(m => m.Regione)
                .NotEmpty().WithMessage("La regione è obbligatoria");
        }
    }
}
using FluentValidation;
using GestaoJogos.Domain.Principal.Dto;
using GestaoJogos.SharedKernel.Infrastructure.Validators;

namespace GestaoJogos.Domain.Principal.Validators
{
    public class AlterarAmigoDtoValidator : Validator<AmigoDto>
    {
        public override void SetRules()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O nome do seu amigo é obrigatório.");
        }
    }
}

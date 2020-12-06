using FluentValidation;
using GestaoJogos.Domain.Email.Dtos;
using GestaoJogos.SharedKernel.Infrastructure.Validators;

namespace GestaoJogos.Domain.Email.Validators
{
    public class EnviarEmailUserDtoValidator : Validator<EmailUserDto>
    {
        public override void SetRules()
        {
            RuleFor(e => e.EmpresaId)
                .NotEmpty().WithMessage("Não possível identificar a empresa configurada para envio do e-mail.");

            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O Nome do usuário não estava presente para enviar o e-mail.");

            RuleFor(e => e.Login)
                .NotEmpty().WithMessage("O Login do usuário não estava presente para enviar o e-mail.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("O E-mail do usuário não estava presente para receber a mensagem.");
        }
    }
}

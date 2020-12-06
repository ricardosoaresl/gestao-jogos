using GestaoJogos.Domain.Principal.Dto;
using GestaoJogos.Domain.Principal.Validators;
using GestaoJogos.SharedKernel.Infrastructure.Entities;
using System;
using static GestaoJogos.SharedKernel.Infrastructure.Validators.DoValidate;

namespace GestaoJogos.Domain.Principal.Entities
{
    public class Jogo : EntityBase
    {
        private Jogo()
        { }

        public Jogo(JogoDto jogoDto)
        {
            if (jogoDto == null) throw new Exception("Jogo não indentificado!");

            Valid<IncluirJogoDtoValidator, JogoDto>.Dispatch(jogoDto);

            Nome = jogoDto.Nome;
            AmigoId = jogoDto.AmigoId;
        }

        public void Change(JogoDto jogoDto)
        {
            if (jogoDto == null) throw new Exception("Jogo não indentificado!");

            Valid<AlterarJogoDtoValidator, JogoDto>.Dispatch(jogoDto);

            Nome = jogoDto.Nome;
            AmigoId = jogoDto.AmigoId;
        }

        public string Nome { get; private set; }
        public Guid? AmigoId { get; private set; }
        public virtual Amigo Amigo { get; private set; }
        public DateTime DataCadastro { get; private set; }
    }
}

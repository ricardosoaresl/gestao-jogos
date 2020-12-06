using GestaoJogos.Domain.Principal.Dto;
using GestaoJogos.Domain.Principal.Validators;
using GestaoJogos.SharedKernel.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using static GestaoJogos.SharedKernel.Infrastructure.Validators.DoValidate;

namespace GestaoJogos.Domain.Principal.Entities
{
    public class Amigo : EntityBase
    {
        private Amigo()
        { }

        public Amigo(AmigoDto amigoDto)
        {
            if (amigoDto == null) throw new Exception("Amigo não indentificado!");

            Valid<IncluirAmigoDtoValidator, AmigoDto>.Dispatch(amigoDto);

            Nome = amigoDto.Nome;
        }

        public void Change(AmigoDto amigoDto)
        {
            if (amigoDto == null) throw new Exception("Amigo não indentificado!");

            Valid<AlterarAmigoDtoValidator, AmigoDto>.Dispatch(amigoDto);

            Nome = amigoDto.Nome;
        }

        public string Nome { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}

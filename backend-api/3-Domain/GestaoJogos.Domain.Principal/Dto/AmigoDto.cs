using GestaoJogos.SharedKernel.Infrastructure.Dto;
using System;

namespace GestaoJogos.Domain.Principal.Dto
{
    public class AmigoDto : DtoBase
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

using GestaoJogos.SharedKernel.Infrastructure.Dto;
using System;

namespace GestaoJogos.Domain.Principal.Dto
{
    public class JogoDto : DtoBase
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid? AmigoId { get; set; }
        public virtual AmigoDto Amigo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

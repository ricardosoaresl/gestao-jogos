using GestaoJogos.SharedKernel.Infrastructure.Dto;
using System;

namespace GestaoJogos.Domain.Email.Dtos
{
    public class EmailUserDto : DtoBase
    {
        public Guid EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}

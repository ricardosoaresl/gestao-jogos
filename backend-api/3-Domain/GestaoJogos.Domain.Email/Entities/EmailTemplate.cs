using GestaoJogos.SharedKernel.Infrastructure.Entities;
using System;

namespace GestaoJogos.Domain.Email.Entities
{
    public class EmailTemplate : EntityBase
    {
        private EmailTemplate()
        { }

        public Guid EmpresaId { get; private set; }
        public string Assunto { get; private set; }
        public string Identificador { get; private set; }
        public string Texto { get; private set; }
    }
}

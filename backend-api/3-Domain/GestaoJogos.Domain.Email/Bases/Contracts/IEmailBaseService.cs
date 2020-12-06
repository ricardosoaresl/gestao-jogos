using GestaoJogos.Domain.Email.Entities;
using System;

namespace GestaoJogos.Domain.Email.Bases.Contracts
{
    public interface IEmailBaseService
    {
        EmailTemplate GetEmailTemplate(Guid empresaId, string identificador);
    }
}

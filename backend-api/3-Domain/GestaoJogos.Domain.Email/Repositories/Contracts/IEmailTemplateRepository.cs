using GestaoJogos.Domain.Email.Entities;
using GestaoJogos.SharedKernel.Infrastructure.Repositories;

namespace GestaoJogos.Domain.Email.Repositories.Contracts
{
    public interface IEmailTemplateRepository : IRepository<EmailTemplate>
    {
    }
}

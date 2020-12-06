using GestaoJogos.Domain.Principal.Entities;
using GestaoJogos.SharedKernel.Infrastructure.Repositories;

namespace GestaoJogos.Domain.Principal.Repositories.Contracts
{
    public interface IJogoRepository : IRepository<Jogo>
    {
    }
}

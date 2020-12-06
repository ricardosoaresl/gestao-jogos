using GestaoJogos.Domain.Principal.Entities;
using GestaoJogos.Domain.Principal.Repositories.Contracts;
using GestaoJogos.Infrastructure.EntityFramework.Base;
using GestaoJogos.SharedKernel.Infrastructure.DataContext;
using System;

namespace GestaoJogos.Infrastructure.EntityFramework.Persistence.Repositories
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        public JogoRepository(IDataContext context, IServiceProvider provider) : base(context)
        {
            _context = context;
        }
    }
}

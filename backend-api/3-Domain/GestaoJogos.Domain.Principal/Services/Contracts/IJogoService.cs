using GestaoJogos.Domain.Principal.Dto;
using GestaoJogos.Domain.Principal.Entities;
using GestaoJogos.SharedKernel.Application.Services;
using System;
using System.Collections.Generic;

namespace GestaoJogos.Domain.Principal.Services.Contracts
{
    public interface IJogoService : IService<Jogo>
    {
        void Create(JogoDto jogo);

        ICollection<JogoDto> Index();

        JogoDto GetById(Guid Id);

        void Update(Guid id, JogoDto jogo);

        void Delete(Guid id);

    }
}

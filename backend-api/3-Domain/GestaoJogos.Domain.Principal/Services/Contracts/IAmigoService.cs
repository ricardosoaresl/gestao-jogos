using GestaoJogos.Domain.Principal.Dto;
using GestaoJogos.Domain.Principal.Entities;
using GestaoJogos.SharedKernel.Application.Services;
using System;
using System.Collections.Generic;

namespace GestaoJogos.Domain.Principal.Services.Contracts
{
    public interface IAmigoService : IService<Amigo>
    {
        void Create(AmigoDto amigo);

        ICollection<AmigoDto> Index();

        AmigoDto GetById(Guid Id);

        void Update(Guid id, AmigoDto amigo);

        void Delete(Guid id);

    }
}

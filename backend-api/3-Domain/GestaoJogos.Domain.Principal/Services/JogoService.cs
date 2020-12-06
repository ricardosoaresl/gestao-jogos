using AutoMapper;
using GestaoJogos.Domain.Principal.Dto;
using GestaoJogos.Domain.Principal.Entities;
using GestaoJogos.Domain.Principal.Repositories.Contracts;
using GestaoJogos.Domain.Principal.Services.Contracts;
using GestaoJogos.SharedKernel.Application.Services;
using GestaoJogos.SharedKernel.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoJogos.Domain.Principal.Services
{
    public class JogoService : Service<Jogo, IJogoRepository>, IJogoService
    {
        public JogoService(
            IUnitOfWork unitOfWork,
            IJogoRepository repository,
            IMapper mapper
        ) : base(unitOfWork, repository, mapper)
        {

        }

        public void Create(JogoDto jogoDto)
        {
            var jogo = new Jogo(jogoDto);
            Insert(jogo);
        }

        public ICollection<JogoDto> Index()
        {
            var jogos = _repository.GetAllInclude(x => x.Amigo);
            return _mapper.Map<ICollection<JogoDto>>(jogos);
        }

        public JogoDto GetById(Guid Id)
        {
            var jogo = FindByInclude(c => c.Id == Id, x => x.Amigo).FirstOrDefault();
            return _mapper.Map<JogoDto>(jogo);
        }

        public void Update(Guid id, JogoDto jogoDto)
        {
            if (jogoDto == null && id != jogoDto.Id)
            {
                throw new Exception("Id do jogo diferente do informado");
            }

            var jogo = FindBy(x => x.Id == id).FirstOrDefault();
            if (jogo == null)
            {
                throw new Exception("Jogo não encontrado");
            }

            jogo.Change(jogoDto);
            Update(jogo);
        }
        public void Delete(Guid id)
        {
            var jogo = FindBy(x => x.Id == id).FirstOrDefault();
            if (jogo == null)
            {
                throw new Exception("Jogo não encontrado");
            }

            Delete(jogo);
        }
    }
}

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
    public class AmigoService : Service<Amigo, IAmigoRepository>, IAmigoService
    {
        public AmigoService(
            IUnitOfWork unitOfWork,
            IAmigoRepository repository,
            IMapper mapper
        ) : base(unitOfWork, repository, mapper)
        {

        }

        public void Create(AmigoDto amigoDto)
        {
            var amigo = new Amigo(amigoDto);
            Insert(amigo);
        }

        public ICollection<AmigoDto> Index()
        {
            var amigos = _repository.GetAll();
            return _mapper.Map<ICollection<AmigoDto>>(amigos);
        }

        public AmigoDto GetById(Guid Id)
        {
            var amigo = FindBy(c => c.Id == Id).FirstOrDefault();
            return _mapper.Map<AmigoDto>(amigo);
        }

        public void Update(Guid id, AmigoDto amigoDto)
        {
            if (amigoDto == null && id != amigoDto.Id)
            {
                throw new Exception("Id do amigo diferente do informado");
            }

            var amigo = FindBy(x => x.Id == id).FirstOrDefault();
            if (amigo == null)
            {
                throw new Exception("Amigo não encontrado");
            }

            amigo.Change(amigoDto);
            Update(amigo);
        }
        public void Delete(Guid id)
        {
            var amigo = FindBy(x => x.Id == id).FirstOrDefault();
            if (amigo == null)
            {
                throw new Exception("Amigo não encontrado");
            }

            Delete(amigo);
        }
    }
}

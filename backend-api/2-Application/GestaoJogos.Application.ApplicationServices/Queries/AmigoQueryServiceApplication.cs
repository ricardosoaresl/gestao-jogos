using AutoMapper;
using GestaoJogos.Application.ApplicationServices.Queries.Contracts;
using GestaoJogos.Application.ApplicationServices.ViewModel;
using GestaoJogos.Domain.Principal.Services.Contracts;
using System;
using System.Collections.Generic;

namespace GestaoJogos.Application.ApplicationServices.Queries
{
    public class AmigoQueryServiceApplication : IAmigoQueryServiceApplication
    {
        private IAmigoService _amigoService;
        private IMapper _mapper;

        public AmigoQueryServiceApplication(
            IAmigoService amigoService,
            IMapper mapper
            )
        {
            _amigoService = amigoService;
            _mapper = mapper;
        }

        public ICollection<AmigoViewModel> Index()
        {
            var amigos = _amigoService.Index();
            return _mapper.Map<ICollection<AmigoViewModel>>(amigos);
        }

        public AmigoViewModel GetById(Guid Id)
        {
            var amigo = _amigoService.GetById(Id);
            return _mapper.Map<AmigoViewModel>(amigo);
        }
    }
}

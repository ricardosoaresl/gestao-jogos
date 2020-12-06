using AutoMapper;
using GestaoJogos.Application.ApplicationServices.Queries.Contracts;
using GestaoJogos.Application.ApplicationServices.ViewModel;
using GestaoJogos.Domain.Principal.Services.Contracts;
using System;
using System.Collections.Generic;

namespace GestaoJogos.Application.ApplicationServices.Queries
{
    public class JogoQueryServiceApplication : IJogoQueryServiceApplication
    {
        private IJogoService _jogoService;
        private IMapper _mapper;

        public JogoQueryServiceApplication(
            IJogoService jogoService,
            IMapper mapper
            )
        {
            _jogoService = jogoService;
            _mapper = mapper;
        }

        public ICollection<JogoViewModel> Index()
        {
            var jogos = _jogoService.Index();
            return _mapper.Map<ICollection<JogoViewModel>>(jogos);
        }

        public JogoViewModel GetById(Guid Id)
        {
            var jogo = _jogoService.GetById(Id);
            return _mapper.Map<JogoViewModel>(jogo);
        }
    }
}

using AutoMapper;
using GestaoJogos.Application.ApplicationServices.Commands.Contracts;
using GestaoJogos.Application.ApplicationServices.ViewModel;
using GestaoJogos.CrossCutting.Validation.BaseException;
using GestaoJogos.Domain.Principal.Dto;
using GestaoJogos.Domain.Principal.Services.Contracts;
using System;

namespace GestaoJogos.Application.ApplicationServices.Commands
{
    public class JogoCommandServiceApplication : IJogoCommandServiceApplication
    {
        private readonly IJogoService _jogoService;
        private readonly IMapper _mapper;

        public JogoCommandServiceApplication(
            IJogoService jogoService,
            IMapper mapper
        )
        {
            _jogoService = jogoService;
            _mapper = mapper;
        }

        public void Incluir(JogoViewModel jogoViewModel)
        {
            try
            {
                var jogoDto = _mapper.Map<JogoDto>(jogoViewModel);

                _jogoService.Create(jogoDto);
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;
                if (e.InnerException != null)
                {
                    ContentSingleton.AddMessage(e.InnerException.Message);
                    ContentSingleton.Dispatch();
                }

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }
        }

        public void Update(Guid id, JogoViewModel jogoViewModel)
        {
            try
            {
                var jogoDto = _mapper.Map<JogoDto>(jogoViewModel);

                _jogoService.Update(id, jogoDto);
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;
                if (e.InnerException != null)
                {
                    ContentSingleton.AddMessage(e.InnerException.Message);
                    ContentSingleton.Dispatch();
                }

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                _jogoService.Delete(id);
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;
                if (e.InnerException != null)
                {
                    ContentSingleton.AddMessage(e.InnerException.Message);
                    ContentSingleton.Dispatch();
                }

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }
        }
    }
}

using AutoMapper;
using GestaoJogos.Application.ApplicationServices.Commands.Contracts;
using GestaoJogos.Application.ApplicationServices.ViewModel;
using GestaoJogos.CrossCutting.Validation.BaseException;
using GestaoJogos.Domain.Principal.Dto;
using GestaoJogos.Domain.Principal.Services.Contracts;
using System;

namespace GestaoJogos.Application.ApplicationServices.Commands
{
    public class AmigoCommandServiceApplication : IAmigoCommandServiceApplication
    {
        private readonly IAmigoService _amigoService;
        private readonly IMapper _mapper;

        public AmigoCommandServiceApplication(
            IAmigoService amigoService,
            IMapper mapper
        )
        {
            _amigoService = amigoService;
            _mapper = mapper;
        }

        public void Incluir(AmigoViewModel amigoViewModel)
        {
            try
            {
                var amigoDto = _mapper.Map<AmigoDto>(amigoViewModel);

                _amigoService.Create(amigoDto);
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

        public void Update(Guid id, AmigoViewModel amigoViewModel)
        {
            try
            {
                var amigoDto = _mapper.Map<AmigoDto>(amigoViewModel);

                _amigoService.Update(id, amigoDto);
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
                _amigoService.Delete(id);
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

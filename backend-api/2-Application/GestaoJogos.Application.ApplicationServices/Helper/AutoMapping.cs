using AutoMapper;
using GestaoJogos.Application.ApplicationServices.ViewModel;
using GestaoJogos.Domain.Principal.Dto;
using GestaoJogos.Domain.Principal.Entities;

namespace GestaoJogos.Application.ApplicationServices.Helper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            // From Entity to Dto
            CreateMap<Amigo, AmigoDto>();
            CreateMap<Jogo, JogoDto>();

            // From ViewModel to Dto
            CreateMap<AmigoViewModel, AmigoDto>().ReverseMap();
            CreateMap<JogoViewModel, JogoDto>().ReverseMap();
        }
    }
}


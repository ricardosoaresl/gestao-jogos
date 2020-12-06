using GestaoJogos.Application.ApplicationServices.ViewModel;
using System;

namespace GestaoJogos.Application.ApplicationServices.Commands.Contracts
{
    public interface IAmigoCommandServiceApplication
    {
        void Incluir(AmigoViewModel viewModel);

        void Update(Guid id, AmigoViewModel viewModel);

        void Delete(Guid id);
    }
}

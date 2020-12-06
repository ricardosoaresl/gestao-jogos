using GestaoJogos.Application.ApplicationServices.ViewModel;
using System;

namespace GestaoJogos.Application.ApplicationServices.Commands.Contracts
{
    public interface IJogoCommandServiceApplication
    {
        void Incluir(JogoViewModel viewModel);

        void Update(Guid id, JogoViewModel viewModel);

        void Delete(Guid id);
    }
}

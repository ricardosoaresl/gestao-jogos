using GestaoJogos.Application.ApplicationServices.ViewModel;
using System;
using System.Collections.Generic;

namespace GestaoJogos.Application.ApplicationServices.Queries.Contracts
{
    public interface IJogoQueryServiceApplication
    {
        ICollection<JogoViewModel> Index();

        JogoViewModel GetById(Guid Id);
    }
}


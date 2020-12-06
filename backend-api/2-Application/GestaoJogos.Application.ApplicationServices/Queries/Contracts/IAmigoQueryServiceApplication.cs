using GestaoJogos.Application.ApplicationServices.ViewModel;
using System;
using System.Collections.Generic;

namespace GestaoJogos.Application.ApplicationServices.Queries.Contracts
{
    public interface IAmigoQueryServiceApplication
    {
        ICollection<AmigoViewModel> Index();

        AmigoViewModel GetById(Guid Id);
    }
}


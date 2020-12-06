using GestaoJogos.SharedKernel.Infrastructure.ViewModel;
using System;

namespace GestaoJogos.Application.ApplicationServices.ViewModel
{
    public class AmigoViewModel : ViewModelBase
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}

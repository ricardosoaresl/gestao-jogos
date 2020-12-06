using GestaoJogos.SharedKernel.Infrastructure.ViewModel;
using System;

namespace GestaoJogos.Application.ApplicationServices.ViewModel
{
    public class JogoViewModel : ViewModelBase
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid? AmigoId { get; set; }
        public virtual AmigoViewModel Amigo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

using System.Collections.Generic;

namespace GestaoJogos.CrossCutting.Notification.Services.Contracts
{
    public interface INotificationService
    {
        bool EnviarEmail(string to, string assunto, string msg, Dictionary<string, string> chavesMsg, IList<string> copiarPara = null);
    }
}

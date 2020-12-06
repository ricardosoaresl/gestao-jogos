using System.Collections.Generic;

namespace GestaoJogos.CrossCutting.Notification.Services.Contracts
{
    public interface IEmailBus
    {
        void SendToQueue(string to, string subject, string messageBody, IList<string> copiarPara);
    }
}

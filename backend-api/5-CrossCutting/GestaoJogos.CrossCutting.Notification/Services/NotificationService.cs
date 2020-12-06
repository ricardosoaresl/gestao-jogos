using GestaoJogos.CrossCutting.Notification.Services.Contracts;
using System;
using System.Collections.Generic;

namespace GestaoJogos.CrossCutting.Notification.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailBus _emailBus;

        public NotificationService(IEmailBus emailBus)
        {
            _emailBus = emailBus;
        }

        private string Interpolar(string texto, Dictionary<string, string> dados)
        {
            var retorno = texto;
            foreach (var key in dados.Keys)
            {
                retorno = retorno.Replace("{{" + key + "}}", dados[key]);
            }
            return retorno;
        }

        public bool EnviarEmail(string to, string assunto, string msg, Dictionary<string, string> chavesMsg, IList<string> copiarPara = null)
        {
            try
            {
                _emailBus.SendToQueue(to, assunto, Interpolar(msg, chavesMsg), copiarPara);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

    }
}

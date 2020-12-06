using System;
using System.Threading.Tasks;

namespace GestaoJogos.CrossCutting.Notification.Services.Contracts
{
    public interface IMessageBus
    {
        /// <summary>
        /// Publica uma mensagem na fila
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        void SendToQueue(String queue, String message);

        /// <summary>
        /// Publica uma mensagem na fila
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        void SendToQueue<T>(String queue, T message) where T : SerializableMessage;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        Task<String> RemoteCallAsync(String queue, String message);
    }
}

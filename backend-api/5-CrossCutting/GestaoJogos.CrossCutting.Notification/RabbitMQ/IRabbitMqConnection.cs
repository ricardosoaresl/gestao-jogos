using System;

namespace GestaoJogos.CrossCutting.Notification.RabbitMQ
{
    public interface IRabbitMqConnection
    {
        void SendToQueue(String queue, String message);

        string RemoteCall(string queue, string message, int timeout = 10);

        void Dispose();
    }
}

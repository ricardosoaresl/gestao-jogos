using GestaoJogos.CrossCutting.Notification.Config;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace GestaoJogos.CrossCutting.Notification.RabbitMQ
{
    public class RabbitMqConnection : IDisposable, IRabbitMqConnection
    {
        private IModel _channel;
        private IConnection _connection;

        public RabbitMqConnection(IOptions<MessageBusOptions> config)
        {
            var factory = new ConnectionFactory()
            {
                HostName = config.Value.Host,
                UserName = config.Value.Username,
                Password = config.Value.Password,
                Port = config.Value.Port
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void SendToQueue(String queue, String message)
        {

            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            // publicando par ao default exchange e passando como routingKey a fila
            // informada pelo clliente. Dessa forma, não é necessário usar exchange.
            _channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: properties, body: body);

        }

        public string RemoteCall(string queue, string message, int timeout = 10)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            var channelRpc = _connection.CreateModel();

            try
            {
                // callback queue and correlation id
                var replyQueueName = channelRpc.QueueDeclare(exclusive: true).QueueName;
                var corrId = Guid.NewGuid().ToString();

                // propriedades da menasgem (replayTo, corrId)
                var props = channelRpc.CreateBasicProperties();
                props.ReplyTo = replyQueueName;
                props.CorrelationId = corrId;

                // declara fila rpc e publica mensagem
                channelRpc.QueueDeclare(queue: queue, durable: false, autoDelete: false, exclusive: false);
                channelRpc.BasicPublish(exchange: "", routingKey: queue, basicProperties: props, body: messageBytes);


                // aguarda resposta na callback queue
                var consumer = new QueueingBasicConsumer(channelRpc);
                channelRpc.BasicConsume(queue: replyQueueName, autoAck: true, consumer: consumer);

                BasicDeliverEventArgs ea;
                var result = consumer.Queue.Dequeue(timeout * 1000, out ea);

                if (result && ea.BasicProperties.CorrelationId == corrId)
                {
                    return Encoding.UTF8.GetString(ea.Body);
                }

                throw new TimeoutException($"Tempo limite de {timeout} segundos excedido para RPC na fila \"{queue}\".");
            }
            finally
            {
                if (channelRpc.IsOpen)
                {
                    channelRpc.Close();
                }

                if (channelRpc != null)
                {
                    channelRpc.Dispose();
                }
            }

        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }

            if (_channel != null)
            {
                _channel.Dispose();
            }
        }
    }
}

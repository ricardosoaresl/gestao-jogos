using GestaoJogos.CrossCutting.Notification.Config;
using GestaoJogos.CrossCutting.Notification.ObjectNotification;
using GestaoJogos.CrossCutting.Notification.Services.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoJogos.CrossCutting.Notification.Services
{
    public class EmailQueueListener : EmailService, IHostedService, IEmailQueueListener
    {
        private readonly EmailQueueConfig _optionsQueue;
        private readonly MessageBusOptions _messageServerConfig;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public EmailQueueListener(
            IOptions<EmailQueueConfig> optionsQueue,
            IOptions<EmailServerConfig> emailConfig,
            IOptions<MessageBusOptions> messageServerConfig,
            ILogger<EmailService> logger
            ) : base(emailConfig, logger)
        {
            _optionsQueue = optionsQueue.Value ?? throw new ArgumentNullException(nameof(optionsQueue));
            _messageServerConfig = messageServerConfig.Value ?? throw new ArgumentNullException(nameof(messageServerConfig));

            _factory = new ConnectionFactory()
            {
                HostName = _messageServerConfig.Host,
                Port = _messageServerConfig.Port,
                UserName = _messageServerConfig.Username,
                Password = _messageServerConfig.Password,
                RequestedHeartbeat = 60
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Register()
        {
            _channel.ExchangeDeclare(exchange: "message", type: "topic");
            _channel.QueueDeclare(queue: _optionsQueue.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _optionsQueue.QueueName,
                              exchange: "message",
                              routingKey: _optionsQueue.RouteKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                var result = Process(message);

                if (result)
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            _channel.BasicConsume(queue: _optionsQueue.QueueName, consumer: consumer);
        }

        public void DeRegister()
        {
            _connection.Close();
        }

        public bool Process(string message)
        {
            EmailObjectNotification emailTrail = JsonConvert.DeserializeObject<EmailObjectNotification>(message);
            return SendEmail(emailTrail);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}

using GestaoJogos.CrossCutting.Notification.RabbitMQ;
using GestaoJogos.CrossCutting.Notification.Services;
using GestaoJogos.CrossCutting.Notification.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoJogos.CrossCutting.Notification.IoC
{
    public class NotificationInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.AddTransient<IMessageBus, MessageBus>();
            services.AddTransient<IEmailBus, EmailBus>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddHostedService<EmailQueueListener>();
        }
    }
}

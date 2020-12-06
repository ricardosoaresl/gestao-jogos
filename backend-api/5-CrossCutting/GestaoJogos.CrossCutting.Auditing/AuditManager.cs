using GestaoJogos.CrossCutting.Auditing.EventArgs;
using GestaoJogos.CrossCutting.Auditing.Trail;
using GestaoJogos.CrossCutting.Notification.Services.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoJogos.CrossCutting.Auditing
{
    public class AuditManager : IAuditManager
    {
        private AuditEnvironment _environment;
        private readonly IMessageBus _messageBus;
        private AuditManagerOptions _options;

        public AuditManager(
            IMessageBus messageBus,
            IOptions<AuditManagerOptions> options)
        {
            if (messageBus == null)
            {
                throw new ArgumentNullException(nameof(messageBus));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _messageBus = messageBus;
            _options = options.Value;
            _environment = new AuditEnvironment();
        }

        public void ConfigureAuditEnvironment(Action<AuditEnvironment> environment)
        {
            environment(_environment);
        }

        public void OnAuditEntity(AuditEventArgs eventArgs)
        {
            var trail = new EntityAuditTrail
            {
                Acao = EntityAuditTrail.AcaoMap[eventArgs.Action],

                AppId = _options.AppId,
                SessaoId = _environment.SessionId,
                UsuarioId = _environment.Username,

                HttpMethod = _environment.HttpMethod,
                HttpScheme = _environment.HttpScheme,
                HttpHost = _environment.HttpHost,
                HttpPathBase = _environment.HttpPathBase,
                HttpPath = _environment.HttpPath,
                HttpQueryString = _environment.HttpQueryString,

                DataHora = eventArgs.DateTime,
                EntidadeId = eventArgs.EntityId.ToString(),
                EntidadeNome = eventArgs.EntityName,
                EntidadeNamespace = eventArgs.EntityNamespace,
                ValoresAtuais = eventArgs.Fields
            };

            _messageBus.SendToQueue(_options.QueueName, trail);
        }

        public async Task<AuditQueryResponse> ViewRecordsAsync(string EntidadeNome, string EntidadeNamespace, string EntidadeId)
        {
            var values = new Dictionary<string, string>
            {
                {  "\"EntidadeNome\"", EntidadeNome },
                {  "\"EntidadeNamespace\"", EntidadeNamespace },
                {  "\"EntidadeId\"", EntidadeId },
            };

            var arrItens = values.Select(d => $"{d.Key}: \"{d.Value}\"");
            var query = "\"Query\": {" + string.Join(", ", arrItens) + "}";
            var orderBy = "\"OrderBy\": { \"DataHora\": -1 }";
            var message = "{" + string.Join(", ", query, orderBy) + "}";


            var response = await _messageBus.RemoteCallAsync(string.Join(".", _options.QueueName, "query"), message);
            var responseObject = JsonConvert.DeserializeObject<AuditQueryResponse>(response,
                new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

            return responseObject;
        }

    }

}

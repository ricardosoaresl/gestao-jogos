using GestaoJogos.CrossCutting.Auditing.EventArgs;
using GestaoJogos.CrossCutting.Notification.Services;
using System;
using System.Collections.Generic;

namespace GestaoJogos.CrossCutting.Auditing.Trail
{
    public class EntityAuditTrail : SerializableMessage
    {
        public static string ACAO_NOVO = "Novo";
        public static string ACAO_EXCLUIR = "Excluir";
        public static string ACAO_EDITAR = "Editar";

        public static Dictionary<string, string> AcaoMap = new Dictionary<string, string>
        {
            { AuditEventArgs.INSERT, ACAO_NOVO },
            { AuditEventArgs.UPDATE, ACAO_EDITAR },
            { AuditEventArgs.DELETE, ACAO_EXCLUIR }
        };

        public DateTime DataHora { get; set; }
        public string Acao { get; set; }
        public string UsuarioId { get; set; }
        public string AppId { get; set; }
        public string SessaoId { get; set; }
        public string EntidadeNome { get; set; }
        public string EntidadeNamespace { get; set; }
        public string EntidadeId { get; set; }

        public string HttpMethod { get; set; }
        public string HttpScheme { get; set; }
        public string HttpHost { get; set; }
        public string HttpPathBase { get; set; }
        public string HttpPath { get; set; }
        public string HttpQueryString { get; set; }

        public Dictionary<string, string> ValoresAtuais { get; set; }

        public EntityAuditTrail()
        {
            ValoresAtuais = new Dictionary<string, string>();
        }
    }
}
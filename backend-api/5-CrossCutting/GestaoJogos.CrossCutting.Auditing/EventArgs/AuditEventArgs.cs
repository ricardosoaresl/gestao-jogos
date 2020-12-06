using System;
using System.Collections.Generic;

namespace GestaoJogos.CrossCutting.Auditing.EventArgs
{
    public class AuditEventArgs : System.EventArgs
    {
        public static string INSERT = "insert";
        public static string UPDATE = "update";
        public static string DELETE = "delete";

        public string Action { get; set; }
        public DateTime DateTime { get; set; }
        public string EntityNamespace { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public Dictionary<string, string> Fields { get; set; }

        public AuditEventArgs()
        {
            DateTime = DateTime.Now;
            Fields = new Dictionary<string, string>();
        }

    }

}

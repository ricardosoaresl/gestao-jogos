using GestaoJogos.CrossCutting.Notification.Services;
using System.Collections.Generic;

namespace GestaoJogos.CrossCutting.Notification.ObjectNotification
{
    public class EmailObjectNotification : SerializableMessage
    {
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public IList<string> Copias { get; set; }
    }
}

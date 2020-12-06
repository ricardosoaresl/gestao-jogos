namespace GestaoJogos.CrossCutting.Notification.Config
{
    public class EmailServerConfig
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string LocalDomain { get; set; }
        public string MailServerAddress { get; set; }
        public string MailServerPort { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
    }
}

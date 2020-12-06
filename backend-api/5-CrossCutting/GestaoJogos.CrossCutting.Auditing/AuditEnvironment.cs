namespace GestaoJogos.CrossCutting.Auditing
{
    public class AuditEnvironment
    {
        public string Username { get; set; }
        public string SessionId { get; set; }
        public string HttpScheme { get; set; }
        public string HttpHost { get; set; }
        public string HttpPathBase { get; set; }
        public string HttpPath { get; set; }
        public string HttpQueryString { get; set; }
        public string HttpMethod { get; internal set; }
    }

}

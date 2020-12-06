using Newtonsoft.Json;

namespace GestaoJogos.CrossCutting.Notification.Services
{
    public class SerializableMessage
    {
        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

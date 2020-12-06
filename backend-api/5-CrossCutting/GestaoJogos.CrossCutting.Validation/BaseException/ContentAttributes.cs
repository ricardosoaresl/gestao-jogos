using System.Collections.Generic;

namespace GestaoJogos.CrossCutting.Validation.BaseException
{

    public class ContentAttributes
    {
        public ContentAttributes()
        {
            Message = new List<string>();
        }
        public string Name { get; set; }

        public List<string> Message { get; set; }
    }
}

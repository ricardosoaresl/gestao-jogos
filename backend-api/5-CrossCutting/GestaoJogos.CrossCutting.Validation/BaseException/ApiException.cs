using System;

namespace GestaoJogos.CrossCutting.Validation.BaseException
{
    public class ApiException : Exception
    {
        public ContentSingleton Content { get; set; }

        public ApiException(ContentSingleton content) : base()
        {
            Content = content;
        }
    }
}

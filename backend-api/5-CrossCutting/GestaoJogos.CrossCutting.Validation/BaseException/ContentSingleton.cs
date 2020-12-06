using FluentValidation.Results;
using System.Collections.Generic;
using System.Reflection;

namespace GestaoJogos.CrossCutting.Validation.BaseException
{
    public class ContentSingleton
    {
        /**
        * Instancia do Singleton
        */
        private static ContentSingleton Instance = null;

        /** 
         * O nome deste atributo é usado para realizar o parse na camada de apresentacao
         */
        public List<ContentAttributes> Content;

        /**
         * Construtor privado
         */
        private ContentSingleton()
        {
            Content = new List<ContentAttributes>();
        }

        /**
         * Retorna a instancia deste
         */
        public static ContentSingleton GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ContentSingleton();
            }
            return Instance;
        }

        /**
         * Adiciona uma mensagem com atributo null e message igual ao aconteudo do parametro message
         */
        public static void AddMessage(string message)
        {
            ContentSingleton content = ContentSingleton.GetInstance();
            ContentAttributes c = content.Content.Find(item => item.Name == null);

            if (c == null)
            {
                ContentAttributes Attribute = new ContentAttributes();
                Attribute.Name = null;
                Attribute.Message.Add(message);
                content.Content.Add(Attribute);
            }
            else
            {
                c.Message.Add(message);
            }
        }

        /**
         * Adiciona mensagens explicitando uma string (attribute) para preencher o name e message para o conteudo de message
         */
        public static void AddMessage(string attribute, string message)
        {
            ContentSingleton content = ContentSingleton.GetInstance();
            ContentAttributes c = content.Content.Find(item => item.Name == attribute);

            if (c == null)
            {
                ContentAttributes Attribute = new ContentAttributes();
                Attribute.Name = attribute;
                Attribute.Message.Add(message);
                content.Content.Add(Attribute);
            }
            else
            {
                c.Message.Add(message);
            }
        }

        /**
         * Adiciona mensagens para o tipo ValidationResult
         */
        public static ContentSingleton AddMessage(ValidationResult fluentErrors)
        {
            foreach (ValidationFailure errors in fluentErrors.Errors)
            {
                AddMessage(errors.PropertyName, errors.ErrorMessage);
            }

            return GetInstance();
        }

        /**
         * Apaga todas as Mensagens de exeção registradas em memória
         */
        public static void ClearMessages()
        {
            GetInstance().Content.Clear();
        }

        /**
         * Adiciona mensagens do tipo object onde PropertyInfo(data).Name  sera o name, e (string) PropertyInfo(data).value será adicionado em message
         * @important Não é buscado os níveis abaixo do primeiro nó do objeto data
         */
        public static void AddMessage(object data)
        {
            PropertyInfo[] props = data.GetType().GetProperties();

            foreach (var p in props)
            {
                AddMessage(p.Name, (string)p.GetValue(data));
            }
        }

        /**
         * Retorna true caso haja menssagens ou false caso negativo
         */
        public static bool HasContent()
        {
            return (GetInstance().Content.Count > 0);
        }

        /**
         * Retorna um clone da instância da ContentSingleton e limpa o conteúdo atual
         */
        public static ContentSingleton getInstanceAndClear()
        {
            ContentSingleton clone = new ContentSingleton();
            clone.Content = new List<ContentAttributes>();
            clone.Content.AddRange(GetInstance().Content);

            GetInstance().Content.Clear();

            return clone;
        }

        /**
         * Lança uma excessão com o clone do Content, a excessão será capturada pelo filter exception
         */
        public static void Dispatch()
        {
            if (HasContent())
            {
                ContentSingleton clone = getInstanceAndClear();

                throw new ApiException(clone);
            }
        }

        public static void Dispatch(string message)
        {
            AddMessage(message);
            Dispatch();
        }
    }
}

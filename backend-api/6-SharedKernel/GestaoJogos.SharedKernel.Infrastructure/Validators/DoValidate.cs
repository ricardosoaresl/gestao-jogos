using FluentValidation.Results;
using GestaoJogos.CrossCutting.Validation.BaseException;
using GestaoJogos.SharedKernel.Infrastructure.Dto;
using System;

namespace GestaoJogos.SharedKernel.Infrastructure.Validators
{
    public abstract class DoValidate
    {
        public class Valid<V, Dto> where V : Validator<Dto> where Dto : DtoBase
        {
            public static V Instace() => (V)Activator.CreateInstance(typeof(V));

            public static void Dispatch(Dto entity)
            {
                ValidationResult result = Instace().DoValidate(entity);

                if (!result.IsValid)
                {
                    ContentSingleton.AddMessage(result);
                    ContentSingleton.Dispatch();
                }
            }
        }
    }
}

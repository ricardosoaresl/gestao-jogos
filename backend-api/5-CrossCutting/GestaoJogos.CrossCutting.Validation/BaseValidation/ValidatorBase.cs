using FluentValidation;
using FluentValidation.Results;
using GestaoJogos.CrossCutting.Validation.BaseException;

namespace GestaoJogos.CrossCutting.Validation.BaseValidation
{
    public abstract class ValidatorBase<TEntity> : AbstractValidator<TEntity> where TEntity : class
    {
        abstract public void SetRules();
        public ValidatorBase()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            this.SetRules();
        }

        public void DoValidate(TEntity entity)
        {
            ValidationResult result = Validate(entity);

            if (!result.IsValid)
            {
                ContentSingleton.AddMessage(result);
                ContentSingleton.Dispatch();
            }
        }

        public bool IsValid(TEntity entity)
        {
            ValidationResult result = Validate(entity);

            return result.IsValid;
        }
    }
}

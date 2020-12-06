using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using GestaoJogos.CrossCutting.Validation.BaseException;
using System;
using System.Net;


namespace GestaoJogos.Presentation.Api.Base.Filters
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException)
            {
                ApiException exception = (ApiException)context.Exception;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(exception.Content);
            }
            else
            {
                string msg = null;
                string stack = null;

                if (context.Exception is UnauthorizedAccessException)
                {
                    ContentSingleton.AddMessage("Acesso não autorizado!");
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else
                {
#if !DEBUG
                        msg = "Um erro inesperado ocorreu! Atualize a página e tente novamente. Caso o erro persista, por favor, entre em contato com a equipe técnica.";
                        stack = null;
#else
                    msg = context.Exception.GetBaseException().Message;
                    stack = context.Exception.StackTrace;
#endif

                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    ContentSingleton.AddMessage(stack, msg);
                }

                context.Result = new JsonResult(ContentSingleton.getInstanceAndClear());
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace GestaoJogos.CrossCutting.Auditing
{
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;

        public AuditMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var auditManager = context.RequestServices.GetService<IAuditManager>();
            var httpContextAccessor = context.RequestServices.GetService<IHttpContextAccessor>();
            ClaimsPrincipal claimsPrincipal = null;

            if (httpContextAccessor.HttpContext != null)
            {
                claimsPrincipal = httpContextAccessor.HttpContext.User;
            }

            auditManager.ConfigureAuditEnvironment(e => {
                e.SessionId = claimsPrincipal != null && claimsPrincipal.FindFirst("jti") != null ? claimsPrincipal.FindFirst("jti").Value : "usuario-nao-autenticado";
                e.HttpMethod = context.Request.Method;
                e.HttpScheme = context.Request.Scheme;
                e.HttpHost = context.Request.Host.ToString();
                e.HttpPathBase = context.Request.PathBase.ToString();
                e.HttpPath = context.Request.Path.ToString();
                e.HttpQueryString = context.Request.QueryString.ToString();
                e.Username = claimsPrincipal != null && claimsPrincipal.FindFirst("cpf") != null ? claimsPrincipal.FindFirst("cpf").Value : "usuario-nao-autenticado";
            });

            await _next.Invoke(context);
        }
    }

}

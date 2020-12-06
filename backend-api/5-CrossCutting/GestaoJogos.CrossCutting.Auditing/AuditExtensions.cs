using Microsoft.AspNetCore.Builder;

namespace GestaoJogos.CrossCutting.Auditing
{
    public static class AuditExtensions
    {
        public static IApplicationBuilder UseAuditManager(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditMiddleware>();
        }

    }
}

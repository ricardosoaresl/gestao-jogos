using GestaoJogos.CrossCutting.Auditing.EventArgs;
using System;
using System.Threading.Tasks;

namespace GestaoJogos.CrossCutting.Auditing
{
    public interface IAuditManager
    {
        void ConfigureAuditEnvironment(Action<AuditEnvironment> environment);

        void OnAuditEntity(AuditEventArgs eventArgs);

        Task<AuditQueryResponse> ViewRecordsAsync(string EntidadeNome, string EntidadeNamespace, string EntidadeId);
    }

}

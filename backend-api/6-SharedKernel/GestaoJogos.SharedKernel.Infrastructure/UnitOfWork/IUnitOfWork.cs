using GestaoJogos.SharedKernel.Infrastructure.DataContext;

namespace GestaoJogos.SharedKernel.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        void OpenTransaction();

        void Commit();

        void Rollback();

        void AddContext(IDataContext dataContext);
    }
}

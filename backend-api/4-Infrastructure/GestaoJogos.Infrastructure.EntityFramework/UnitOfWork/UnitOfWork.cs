using GestaoJogos.Infrastructure.EntityFramework.Context;
using GestaoJogos.SharedKernel.Infrastructure.DataContext;
using GestaoJogos.SharedKernel.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace GestaoJogos.Infrastructure.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IDataContext _context;

        protected IDbContextTransaction _transaction;
        public UnitOfWork(GestaoJogosContext context)
        {
            _context = context;
        }

        public virtual void Commit()
        {
            if (_transaction == null)
            {
                return;
            }

            try
            {
                _context.SaveChanges();
                _transaction.Commit();
                _transaction = null;
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public virtual void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void AddContext(IDataContext dataContext)
        {
            _context = dataContext;
        }

        public virtual void OpenTransaction()
        {
            if (_transaction == null)
            {
                _transaction = ((GestaoJogosContext)_context).Database.BeginTransaction();
            }
        }
    }
}

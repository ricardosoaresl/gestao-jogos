using GestaoJogos.SharedKernel.Infrastructure.DataContext;
using GestaoJogos.SharedKernel.Infrastructure.Entities;
using GestaoJogos.SharedKernel.Infrastructure.Pagination;
using GestaoJogos.SharedKernel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestaoJogos.Infrastructure.EntityFramework.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected IDataContext _context;
        protected DbSet<TEntity> _dbSet;

        public Repository(IDataContext context)
        {
            _context = context;

            var dbContext = context as DbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public TEntity FindByKey(Guid id)
        {
            return _dbSet.TempFindById(_context as DbContext, id).FirstOrDefault();
        }


        public TEntity FindByKeyAsNoTracking(Guid id)
        {
            return _dbSet.TempFindById(_context as DbContext, id).AsNoTracking().FirstOrDefault();
        }


        public TEntity FindByKeyInclude(Guid id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _dbSet
                        .TempFindByIdInclude(_context as DbContext, id, includeProperties)
                        .FirstOrDefault();
        }

        public TEntity FindByKeyIncludeAsNoTracking(Guid id, Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _dbSet
                        .TempFindByIdInclude(_context as DbContext, id, includeProperties)
                        .AsNoTracking()
                        .FirstOrDefault();
        }


        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public IEnumerable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetlAllIncluding(includeProperties);
            return query.Where(predicate).ToList();
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAllInclude(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetlAllIncluding(includeProperties).ToList();
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void DeleteByKey(Guid id)
        {
            Delete(_dbSet.TempFindById(_context as DbContext, id).First());
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        private IQueryable<TEntity> GetlAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _dbSet.AsQueryable();
            foreach (var prop in includeProperties)
            {
                query = query.Include(prop);
            }
            return query;
        }

        protected IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public PagedResult<TEntity> GetAll(int page, int pageSize)
        {
            var results = new PagedResult<TEntity>
            {
                Page = page,
                PageSize = pageSize,
                Items = _dbSet.Skip(pageSize * (page - 1)).Take(pageSize).ToList(),
                TotalItems = _dbSet.Count()
            };

            return results;
        }

        public PagedResult<TEntity> Select(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            if (includes != null)
            {
                foreach (var prop in includes)
                {
                    query = query.Include(prop);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var results = new PagedResult<TEntity>
            {
                Page = page,
                PageSize = pageSize,
                Items = query.Skip(pageSize * (page - 1)).Take(pageSize).ToList(),
                TotalItems = query.Count()
            };

            return results;
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public void AddContext(IDataContext dataContext)
        {
            _context = dataContext;

            var dbContext = dataContext as DbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
    }
}

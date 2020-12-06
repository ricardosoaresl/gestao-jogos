using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GestaoJogos.Infrastructure.EntityFramework.Base
{
    public static class DbSetExtension
    {

        public static IQueryable<TEntity> TempFindById<TEntity>(this DbSet<TEntity> set, DbContext context, params object[] keyValues) where TEntity : class
        {

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = set.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(keyValues[0])),
                    parameter));

            // Look in the database
            return query;
        }

        public static IQueryable<TEntity> TempFindByIdInclude<TEntity>(this DbSet<TEntity> set, DbContext context, Guid id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = set.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(id)),
                    parameter));

            foreach (var prop in includeProperties)
            {
                query = query.Include(prop);
            }

            // Look in the database
            return query;
        }

    }
}

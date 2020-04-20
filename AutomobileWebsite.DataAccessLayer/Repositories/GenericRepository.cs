using AutomobileWebsite.DataAccessLayer.Interfaces;
using AutomobileWebsite.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutomobileWebsite.DataAccessLayer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AutomobileWebsiteContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AutomobileWebsiteContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<TType> Get<TType>(
            Expression<Func<TEntity, TType>> select,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TType : class
        {
            if (select == null)
            {
                throw new Exception($"Select parameter of {nameof(Get)}() method cannot be null");
            }

            return ExecuteQuery(filter, orderBy, includes).AsQueryable().Select(select).ToList();
        }

        public TType GetSingle<TType>(
            Expression<Func<TEntity, TType>> select,
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TType : class
        {
            if (select == null)
            {
                throw new Exception($"Select parameter of {nameof(Get)}() method cannot be null");
            }

            return ExecuteQuery(filter, null, includes).AsQueryable().Select(select).SingleOrDefault();
        }

        private IEnumerable<TEntity> ExecuteQuery(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes.Count() != 0)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}

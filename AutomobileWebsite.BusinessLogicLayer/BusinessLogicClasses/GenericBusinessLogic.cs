using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutomobileWebsite.BusinessLogicLayer.BusinessLogicsClasses
{
    public class GenericBusinessLogic<TEntity> : IGenericBusinessLogic<TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository;

        public GenericBusinessLogic(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public IEnumerable<TType> Get<TType>(
            Expression<Func<TEntity, TType>> select,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TType : class
        {
            return _repository.Get<TType>(select, filter, orderBy, includes);
        }

        public TType GetSingle<TType>(
            Expression<Func<TEntity, TType>> select,
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TType : class
        {
            return _repository.GetSingle<TType>(select, filter, includes);
        }
    }
}

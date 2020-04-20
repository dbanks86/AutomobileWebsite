using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutomobileWebsite.BusinessLogicLayer.Interfaces
{
    public interface IGenericBusinessLogic<TEntity> where TEntity : class
    {
        IEnumerable<TType> Get<TType>(
            Expression<Func<TEntity, TType>> select,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TType : class;

        TType GetSingle<TType>(
            Expression<Func<TEntity, TType>> select,
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TType : class;
    }
}

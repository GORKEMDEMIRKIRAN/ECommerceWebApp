

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


using Ecommerce.Core.Entities;


namespace Ecommerce.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll(bool trackChanges);
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity,bool>> expression,bool trackChanges);
        void Create(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);


    }
}

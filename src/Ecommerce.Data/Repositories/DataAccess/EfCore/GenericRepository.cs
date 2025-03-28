


using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Ecommerce.Core.Entities;
using Ecommerce.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;

namespace Ecommerce.Data.Repositories.DataAccess.EfCore
{
    public class GenericRepository <TEntity>:IGenericRepository<TEntity>
        where TEntity:class
    {
        protected readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context=context;
        }
        //===============================================
        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id); 
        }
        //===============================================
        public  IQueryable<TEntity> GetAll(bool trackChanges)
        {
            return trackChanges
                ? _context.Set<TEntity>()
                : _context.Set<TEntity>().AsNoTracking();
        }
        //===============================================
        public  IQueryable<TEntity> FindByCondition(Expression<Func<TEntity,bool>> expression,bool trackChanges)
        {
            return trackChanges
                ? _context.Set<TEntity>().Where(expression)
                : _context.Set<TEntity>().Where(expression).AsNoTracking();
        }
        //===============================================
        public void Create(TEntity entity)
        {
     
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            
        }
        //===============================================
        public void Delete(TEntity entity)
        {

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
            
        }
        //===============================================
        public void Update(TEntity entity)
        {

            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            
        }
        //===============================================
    
    }
}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

//===============================
using Ecommerce.Core.Entities;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Data;
using System.Security.Cryptography.X509Certificates;
//===============================

namespace Ecommerce.Data.Repositories.DataAccess.EfCore
{
    public class RepositoryManager:IRepositoryManager
    {
        private readonly DataContext _context;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
       
        public RepositoryManager(DataContext context)
        {
            _context=context;
            _productRepository = new Lazy<IProductRepository>(()=> new ProductRepository(_context));
            _categoryRepository = new Lazy<ICategoryRepository>(()=> new CategoryRepository(_context));
        }
        public IProductRepository productRepository => _productRepository.Value;
        public ICategoryRepository categoryRepository => _categoryRepository.Value;
        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
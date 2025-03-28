
//=========================================
using System;
using System.Collections.Generic;
//=========================================

using Ecommerce.Data.Repositories.DataAccess.EfCore;
using Ecommerce.Data.Repositories.Interfaces;

using Ecommerce.Service.Interfaces;
//=========================================

namespace Ecommerce.Service.Concrete
{
    public class ServiceManager:IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<ICategoryService> _categoryService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _productService= new Lazy<IProductService>(()=> new ProductService(repositoryManager));
            _categoryService= new Lazy<ICategoryService>(()=> new CategoryService(repositoryManager));
        }

        public IProductService ProductService => _productService.Value;
        public ICategoryService CategoryService => _categoryService.Value;
        ICategoryService IServiceManager.categoryService { get => CategoryService; set => throw new NotImplementedException(); }
        IProductService IServiceManager.productService { get => ProductService; set => throw new NotImplementedException(); }
    }
}
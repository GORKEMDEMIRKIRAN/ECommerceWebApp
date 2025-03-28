

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


using Ecommerce.Core.Entities;


namespace Ecommerce.Data.Repositories.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        //===================================================
        List<Product> GetHomePageProducts();
        List<Product> GetAllProducts();
        Product GetProductDetails(int id);
        List<Product> GetSearchResult(string searchString);
        List<Product> GetProductsByCategory(string name,int page,int pageSize);
        int GetCountByCategory(string category);
        //===================================================
        void CreateOneProduct(Product product);
        void UpdateOneProduct(Product product);
        void DeleteOneProduct(Product product);
        //===================================================
        

    }
}
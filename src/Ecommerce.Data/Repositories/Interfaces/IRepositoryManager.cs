


using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


using Ecommerce.Core.Entities;  // model yolu
using Ecommerce.Data.Repositories.DataAccess.EfCore;    //repository yolu
using Ecommerce.Data.SeedData;  //datacontext yolu


namespace Ecommerce.Data.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IProductRepository productRepository {get;}
        ICategoryRepository categoryRepository {get;}
        void Save();
    }
}
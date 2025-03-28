



using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Ecommerce.Data.SeedData;  //datacontext yolu
using Ecommerce.Core.Entities;  // model yolu
using Ecommerce.Data.Repositories.Interfaces; //interface tolu
using Ecommerce.Data;

namespace Ecommerce.Data.Repositories.DataAccess.EfCore
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
         //===========================================
        public CategoryRepository(DataContext context): base(context){}

        //===========================================
        // Veri tabanında bütün kategorileri alıyoruz.
        public List<Category> GetAllCategories()
        {
            return _context.categories.ToList();
        }
        //===========================================
        public int GetCountCategory()
        {
            return _context.categories.Count();
        }
    }
}
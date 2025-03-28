



using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Ecommerce.Core.Entities;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Data;


namespace Ecommerce.Data.Repositories.DataAccess.EfCore
{
    
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
       
        public ProductRepository(DataContext context): base(context){}

        //==================================================================
        // home ana sayfada ürün listeleme
        public List<Product> GetHomePageProducts()
        {
   
            // ürünün satışta ve ana sayfada olmasını temsil ediyor.
            return _context.products
                .Where(p => p.IsApproved && p.IsHome).ToList();
          
        }
        //==================================================================
        public List<Product> GetAllProducts()
        {
            return _context.products.ToList();
        }
        //==================================================================
        // search kısmında aranan ürüne göre veri tabanında ürün çekme
        public List<Product> GetSearchResult(string searchString)
        {
            // arama string olarak ürün ismine ve tanımına göz atıyor.
            var products = _context.products
                                    .Where( p=>p.IsApproved && (p.Name.ToLower().Contains(searchString.ToLower()) || p.Description.ToLower().Contains(searchString.ToLower()))).AsQueryable();
            return products.ToList();
        }
        //==================================================================
        // Bu kısımda categorideki ürünleri alan metot olacaktır
        // name= kategori ismi
        // page= kaçıncı sayfa olduğu
        // pageSize = sayfada kaç ürün olduğu
        public List<Product> GetProductsByCategory(string name,int page,int pageSize)
        {
            // birinci adım satışta olan ürünleri alalım.
            var products= _context.products
                                    .Where(p=>p.IsApproved)
                                    .AsQueryable();
            // eğer verilen name boş değilse
            if(!string.IsNullOrEmpty(name))
            {
                products = products
                            .Include(p=>p.ProductCategories)
                            .ThenInclude(p=>p.category)
                            .Where(p=>p.ProductCategories.Any(a=>a.category.Name.ToLower()==name.ToLower()));
            }
            // eğer sayfa sayısı 2 ise 1 sayfada 20 ürün ileri git ve 20 tane ürünü alıp listeler
            return products.Skip((page-1)*pageSize).Take(pageSize).ToList();
        }
        //==================================================================
        // verdiğimiz kategori türünü database içinde satışta olan ürünleri alıp
        // kaç tane bu kategoriden ürün varsa onların sayısını döndürür.
        public int GetCountByCategory(string category)
        {
            var products = _context.products
                        .Where(p=>p.IsApproved)
                        .AsQueryable();
            if(!string.IsNullOrEmpty(category))
            {
                products = products
                            .Include(p=>p.ProductCategories)
                            .ThenInclude(p=>p.category)
                            .Where(p=>p.ProductCategories.Any(a=>a.category.Name==category));
            }
            return products.Count();
        }
        //==================================================================
        public Product GetProductDetails(string url)
        {
            var prd=_context.products
                            .Where(p => p.Url.ToLower().Trim() == url.ToLower().Trim())
                            .FirstOrDefault();
            return prd;
        }
        //==================================================================
        public void CreateOneProduct(Product product)
        {
            Create(product);
        }
        //==================================================================
        public void UpdateOneProduct(Product product)
        {
            Update(product);
        }
        //==================================================================
        public void DeleteOneProduct(Product product)
        {
            Delete(product);
        }
        //==================================================================

        //==================================================================
    }
}






using Ecommerce.Core.Entities;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Service.Interfaces;


namespace Ecommerce.Service.Concrete
{
    public class ProductService:IProductService
    {
        //===================================================
        private readonly IRepositoryManager _repositorymanager;
        public ProductService(IRepositoryManager repositoryManager)
        {
            _repositorymanager=repositoryManager;
        }
        //===================================================
        public List<Product> GetHomePageProducts()
        {
            // repositorymanager üzerinden productrepository içinden metot aldım.
            return _repositorymanager.productRepository.GetHomePageProducts();
        }
        //===================================================
        public List<Product> GetAllProducts()
        {
            // sadece bütün ürünleri veren metot
            return _repositorymanager.productRepository.GetAllProducts();
        }
        //===================================================
        public int GetCountByCategory(string category)
        {
            return _repositorymanager.productRepository.GetCountByCategory(category);
        }
        //===================================================
        public Product GetProductDetails(string url)
        {
            return _repositorymanager.productRepository.GetProductDetails(url);
        }
        //===================================================
        public List<Product> GetSearchResult(string searchString)
        {
            return _repositorymanager.productRepository.GetSearchResult(searchString);
        }
        //===================================================
        public List<Product> GetProductsByCategory(string name,int page,int pageSize)
        {
            return _repositorymanager.productRepository.GetProductsByCategory(name,page,pageSize);
        }
        //===================================================
        //===================================================
    }

}
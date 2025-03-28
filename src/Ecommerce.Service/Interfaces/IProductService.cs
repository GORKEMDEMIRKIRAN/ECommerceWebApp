








using Ecommerce.Core.Entities;

namespace Ecommerce.Service.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<Product> GetHomePageProducts();
        Product GetProductDetails(string url);
        List<Product> GetSearchResult(string searchString);
        List<Product> GetProductsByCategory(string name,int page,int pageSize);

        int GetCountByCategory(string category);
    }
}
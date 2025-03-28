

using Ecommerce.Core.Entities;

namespace Ecommerce.Service.Interfaces
{
    public interface IServiceManager
    {
        ICategoryService categoryService {get;set;}
        IProductService productService {get;set;}
    }
}
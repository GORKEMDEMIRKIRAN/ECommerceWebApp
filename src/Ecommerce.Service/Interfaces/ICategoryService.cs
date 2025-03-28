



using Ecommerce.Core.Entities;

namespace Ecommerce.Service.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        public int GetCountCategory();
    }
}
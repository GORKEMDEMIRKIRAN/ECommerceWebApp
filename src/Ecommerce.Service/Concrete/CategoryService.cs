

using Ecommerce.Core.Entities;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Service.Interfaces;


namespace Ecommerce.Service.Concrete
{
    public class CategoryService:ICategoryService
    {
        //======================================================
        private readonly IRepositoryManager _repositoryManager;
        public CategoryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager=repositoryManager;
        }
        //======================================================
        // Repodan bütün kategorileri getiren metot çeker.
        public List<Category> GetAllCategories()
        {
            return _repositoryManager.categoryRepository.GetAllCategories();
        }
        //======================================================
        public int GetCountCategory()
        {
            return _repositoryManager.categoryRepository.GetCountCategory();
        }
        //======================================================
        //======================================================
    }
}
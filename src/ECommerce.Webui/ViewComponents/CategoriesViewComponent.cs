



//=================================
using Ecommerce.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
//=================================

//=================================

namespace ECommerce.Webui.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly IServiceManager _serviceManager;
        public CategoriesViewComponent(IServiceManager serviceManager)
        {
            _serviceManager=serviceManager;
        }

        //=========================================
        public IViewComponentResult Invoke()
        {
            return View(_serviceManager.categoryService.GetAllCategories());
        }
        //=========================================
        //=========================================
        //=========================================
    }
}
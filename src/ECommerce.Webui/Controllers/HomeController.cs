

//===================================
using Microsoft.AspNetCore.Mvc;
//===================================

using Ecommerce.Data;
using Ecommerce.Core;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Service.Interfaces;
//===================================

namespace ECommerce.Webui.Controllers
{
    public class HomeController:Controller
    {
        private IServiceManager _serviceManager;
        public HomeController(IServiceManager serviceManager)
        {
            _serviceManager=serviceManager;
        }
        public IActionResult Index()
        {
            var products = _serviceManager.productService.GetHomePageProducts();
            return View(products);
        }
    }
}




//======================================
using Microsoft.AspNetCore.Mvc;
//======================================
using Ecommerce.Service.Interfaces; //service connected
using ECommerce.Webui.Models; // webui models connected
//======================================

namespace ECommerce.Webui.Controllers
{
    public class ShopController:Controller
    {
        private IServiceManager _serviceManager;
        public ShopController(IServiceManager serviceManager)
        {
            _serviceManager=serviceManager;
        }
        //=============================================
        public IActionResult List(string category,int page=1)
        {
            const int pageSize=20; // bir sayfada kaç ürün olacağını temsil ediyor.
            var sendModel= new ProductListViewModel()
            {
                productsData=_serviceManager.productService.GetProductsByCategory(category,page,pageSize),
                PageInfo = new PageInfo()
                {
                    TotalItems=_serviceManager.productService.GetCountByCategory(category),
                    ItemsPerPage=pageSize,
                    CurrentPage=page,
                    CurrentCategory=category
                }
            };
            return View(sendModel);
        }
        //=============================================
        // productId ile o ürünün detayını getirelim.
        public IActionResult Details(int id)
        {
            var prdt= _serviceManager.productService.GetProductDetails(id);
            if(prdt == null)
            {
                return NotFound();
            }
            var sendModel = new ProductDetailModel()
            {
                product = prdt,
                //categories = prdt.ProductCategories.Select(c=>c.category).ToList()
            };
            return View(sendModel);
        }  
        //=============================================


        
    }
}
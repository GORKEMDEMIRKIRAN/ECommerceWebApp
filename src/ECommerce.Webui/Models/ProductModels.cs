



/*
BU KISIMDA VIEWS İÇİNDE VERİ AKTARIMI İÇİN PRODUCT ÜZERİNDEKİ MODELLER BULUNUCAK
*/

//================================
using System.Collections.Generic;
//================================
using Ecommerce.Core.Entities;
//================================


namespace ECommerce.Webui.Models
{



    public class ProductListViewModel
    {
        public List<Product>? productsData {get;set;}
        public PageInfo? PageInfo {get;set;}
    }

    public class PageInfo
    {
        public int TotalItems {get;set;}
        public int ItemsPerPage {get;set;}
        public int CurrentPage {get;set;}
        public string? CurrentCategory {get;set;}

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems/ItemsPerPage);
        }
    }




}
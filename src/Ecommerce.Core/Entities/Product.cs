



using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.Entities
{
    public class Product
    {
        [Key]
        public int ProductId {get;set;}
        public string? Name {get;set;}
        public string? Url {get;set;}
        public double Price {get;set;}
        public string? Description {get;set;}
        public string? ImageUrl {get;set;}
        // true ise satışta
        public bool IsApproved {get;set;}
        public bool IsHome {get;set;} 
        // one to many ilişkisi
        // bir ürün birden fazla kategori içermesi
        public List<ProductCategory>? ProductCategories {get;set;}
    }

    public class Category
    {
        [Key]
        public int CategoryId {get;set;}
        public string? Name {get;set;}
        // one to many ilişkisi
        // bir kateogori de birden fazla ürün bulunması
        public List<ProductCategory>? ProductCategories {get;set;}
        
    }

    public class ProductCategory
    {
        public int ProductId {get;set;}
        public Product? product {get;set;}
        public int CategoryId {get;set;}
        public Category? category {get;set;}
    }


}
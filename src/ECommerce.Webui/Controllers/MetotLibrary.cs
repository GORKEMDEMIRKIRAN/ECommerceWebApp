



using System.Globalization;

namespace ECommerce.Webui.Controllers
{
    public class MetotLibrary
    {
        // txt dönüştürüp database aktardığımız decimal price değişkenini string olarak verir.
        // Türkiye para birimine dönüştürür. 
        public static string PriceCreate(decimal price)
        {
            CultureInfo turkeyCulture = new CultureInfo("tr-TR");
            return price.ToString("N2",turkeyCulture);
            
        }
    }
}
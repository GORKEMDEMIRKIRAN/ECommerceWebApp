

//================================
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//================================
using ECommerce.Webui.Extensions;
using Ecommerce.Data;
//================================




var builder = WebApplication.CreateBuilder(args);





// MVC Hizmetlerini ekleme
builder.Services.AddControllersWithViews();

// servicesextensions ekleyelim.
// mysql bağlantısı için serviceextensions kullanımı

// MySql.EntityFrameworkCore.Extensions namespace'i artık gerekli değil, çünkü güncel sürümlerde
// UseMySQL metodu doğrudan Microsoft.EntityFrameworkCore namespace'i altında extension metod olarak tanımlanmıştır.

builder.Services.ConfigureSqlContext(builder.Configuration);


var app = builder.Build();



// Geliştirme ortamında hata sayfalarını etkinleştirme
if(app.Environment.IsDevelopment())
{

    app.UseDeveloperExceptionPage();
}else{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


// varsayılan yönlendirme ekleme
app.MapDefaultControllerRoute();

// özel bir kontrolcü ve eylem ekleme
app.MapControllerRoute(
    name:"homepage",
    pattern:"Home/{action}/{id?}",
    defaults: new {controller="Home",action="Index"}
);
app.MapControllerRoute(
    name:"shoplist",
    pattern:"Shop/{action}/{category}/{page?}",
    defaults: new {controller="Shop",action="List"}
); 
app.MapControllerRoute(
    name:"shopdetails",
    pattern:"Shop/{action}/{id}",
    defaults: new {controller="Shop",action="Details"}
);

//====================================================
// Verileri seedleyin
try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        // Bağlantı dizesini yazdır
        Console.WriteLine($"Kullanılan bağlantı dizesi: {context.Database.GetConnectionString()}");
        
        // Migration'ları uygulayın
        Console.WriteLine("Migrations uygulanıyor...");
        context.Database.Migrate();
        
        // Veritabanı boşsa veya belirli bir koşul sağlanıyorsa SeedData() metodunu çağırın
        if (!context.products.Any()) // Örnek koşul: Products tablosu boşsa
        {
            Console.WriteLine("Veritabanı seed ediliyor...");
            context.SeedData();
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Veritabanı işlemleri sırasında hata oluştu: {ex.Message}");
    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
    
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
    }
}
//====================================================
app.Run();
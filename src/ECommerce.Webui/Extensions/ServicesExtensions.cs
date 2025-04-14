


//=========================
using System;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
//=========================
using Ecommerce.Data;
using Ecommerce.Data.Repositories.DataAccess.EfCore;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Service.Interfaces;
using Ecommerce.Service.Concrete;

//=========================

namespace ECommerce.Webui.Extensions
{
    public static class ServicesExtensions
    {

        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration){
            

            // services.AddDbContext<DataContext>(options =>
            //     options.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection")));
            
            // services.AddScoped<IServiceManager,ServiceManager>();
            // services.AddScoped<IRepositoryManager,RepositoryManager>();


            // PostgreSQL bağlantısı için yapılandırma
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgreSqlConnection") 
                                  ?? configuration.GetConnectionString("PostgreSqlConnection");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("PostgreSQL bağlantı dizesi bulunamadı.");
            }
            
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(connectionString, 
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly("ECommerce.Webui")));
            
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }

}
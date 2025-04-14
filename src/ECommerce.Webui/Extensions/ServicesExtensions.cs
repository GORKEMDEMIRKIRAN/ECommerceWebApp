


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

            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection")));
            
            services.AddScoped<IServiceManager,ServiceManager>();
            services.AddScoped<IRepositoryManager,RepositoryManager>();

        }
    }

}
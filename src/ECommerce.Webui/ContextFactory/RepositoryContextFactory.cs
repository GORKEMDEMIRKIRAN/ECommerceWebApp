




//=============================
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
//=============================
using Ecommerce.Data;
//=============================


// Bu bölümde migration webui katmanında oluşmasını sağlıyoruz.

namespace ECommerce.Webui.ContextFactory
{
    public class RepositoryContextFactory:IDesignTimeDbContextFactory<DataContext>
    {   
        public DataContext CreateDbContext(string[] args)
        {
            //configuration Builder
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // //DbContextOptionsBuilder
            // var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
            //DbContextOptionsBuilder
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgreSqlConnection") 
                                  ?? configuration.GetConnectionString("PostgreSqlConnection");

            if(string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Bağlatı dizesi bulunamadı.");
            }

            // güncel pomelo.EntityFrameworkcore.mysql 9.0.3 için doğru kullanım
            var builder = new DbContextOptionsBuilder<DataContext>();

            builder.UseNpgsql(connectionString, proje => proje.MigrationsAssembly("ECommerce.Webui"));
            //.UseSqlite(connectionString, proje => proje.MigrationsAssembly("ECommerce.Webui"));
                        
            return new DataContext(builder.Options);
        }
    }

}
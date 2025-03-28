




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

            //DbContextOptionsBuilder
            var connectionString = configuration.GetConnectionString("SqLite");
            if(string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Bağlatı dizesi bulunamadı.");
            }

            var builder = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(connectionString, proje => proje.MigrationsAssembly("ECommerce.Webui"));

            return new DataContext(builder.Options);
        }
    }

}
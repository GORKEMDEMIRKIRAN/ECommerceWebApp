


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
            
            //============================
            // services.AddDbContext<DataContext>(options =>
            //     options.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection")));
            
            // services.AddScoped<IServiceManager,ServiceManager>();
            // services.AddScoped<IRepositoryManager,RepositoryManager>();


            //============================
            // // PostgreSQL bağlantısı için yapılandırma
            // var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgreSqlConnection") 
            //                       ?? configuration.GetConnectionString("PostgreSqlConnection");
            
            // if (string.IsNullOrEmpty(connectionString))
            // {
            //     throw new InvalidOperationException("PostgreSQL bağlantı dizesi bulunamadı.");
            // }
            
            // services.AddDbContext<DataContext>(options =>
            //     options.UseNpgsql(connectionString, 
            //         npgsqlOptions => npgsqlOptions.MigrationsAssembly("ECommerce.Webui")));
            
            // services.AddScoped<IServiceManager, ServiceManager>();
            // services.AddScoped<IRepositoryManager, RepositoryManager>();
            //============================
            // Tüm olası bağlantı dizesi kaynaklarını kontrol et
            string connectionString = null;
            
            // 1. Doğrudan ortam değişkeni olarak bağlantı dizesi
            connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgreSqlConnection");
            Console.WriteLine($"Ortam değişkeni bağlantı dizesi: {connectionString}");
            
            // 2. Render'ın sağladığı DATABASE_URL formatı
            if (string.IsNullOrEmpty(connectionString))
            {
                var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                Console.WriteLine($"DATABASE_URL: {databaseUrl}");
                
                if (!string.IsNullOrEmpty(databaseUrl))
                {
                    // DATABASE_URL'i Npgsql bağlantı dizesine dönüştür
                    connectionString = ConvertDatabaseUrlToConnectionString(databaseUrl);
                    Console.WriteLine($"Dönüştürülmüş bağlantı dizesi: {connectionString}");
                }
            }
            
            // 3. RENDER_POSTGRES_CONNECTION_STRING kontrolü
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Environment.GetEnvironmentVariable("RENDER_POSTGRES_CONNECTION_STRING");
                Console.WriteLine($"RENDER_POSTGRES_CONNECTION_STRING: {connectionString}");
            }
            
            // 4. Son çare olarak appsettings.json'dan al
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = configuration.GetConnectionString("PostgreSqlConnection");
                Console.WriteLine($"appsettings.json bağlantı dizesi: {connectionString}");
            }
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("PostgreSQL bağlantı dizesi bulunamadı.");
            }
            
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(connectionString, 
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly("ECommerce.Webui")));
            
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            //============================

        }











        // DATABASE_URL formatını Npgsql bağlantı dizesine dönüştür
        private static string ConvertDatabaseUrlToConnectionString(string databaseUrl)
        {
            try
            {
                // Örnek: postgres://username:password@host:port/database
                var uri = new Uri(databaseUrl);
                var userInfo = uri.UserInfo.Split(':');
                
                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = uri.Host,
                    Port = uri.Port > 0 ? uri.Port : 5432,
                    Database = uri.AbsolutePath.TrimStart('/'),
                    Username = userInfo.Length > 0 ? userInfo[0] : "",
                    Password = userInfo.Length > 1 ? userInfo[1] : "",
                    SslMode = SslMode.Prefer,
                    TrustServerCertificate = true
                };
                
                return builder.ConnectionString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DATABASE_URL dönüştürme hatası: {ex.Message}");
                return null;
            }
        }
    }

}
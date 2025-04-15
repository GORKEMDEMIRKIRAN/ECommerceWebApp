




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


            // Tüm olası bağlantı dizesi kaynaklarını kontrol et
            string connectionString = null;

            
            // //DbContextOptionsBuilder
            // var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
            //DbContextOptionsBuilder

            // 1. Doğrudan ortam değişkeni olarak bağlantı dizesi
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgreSqlConnection");
                                 // ?? configuration.GetConnectionString("PostgreSqlConnection");

            //================================
            // 2. Render'ın sağladığı DATABASE_URL formatı
            if (string.IsNullOrEmpty(connectionString))
            {
                var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                if (!string.IsNullOrEmpty(databaseUrl))
                {
                    // DATABASE_URL'i Npgsql bağlantı dizesine dönüştür
                    connectionString = ConvertDatabaseUrlToConnectionString(databaseUrl);
                }
            }
            //================================
            // 3. Son çare olarak appsettings.json'dan al
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = configuration.GetConnectionString("PostgreSqlConnection");
            }
            
            Console.WriteLine($"RepositoryContextFactory - Kullanılan bağlantı dizesi: {connectionString}");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("PostgreSQL bağlantı dizesi bulunamadı.");
            }
            //================================
            // güncel pomelo.EntityFrameworkcore.mysql 9.0.3 için doğru kullanım
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseNpgsql(connectionString, proje => proje.MigrationsAssembly("ECommerce.Webui"));
            //.UseSqlite(connectionString, proje => proje.MigrationsAssembly("ECommerce.Webui"));
                        
            return new DataContext(builder.Options);
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
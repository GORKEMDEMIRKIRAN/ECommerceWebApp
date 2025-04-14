# # Build aşaması
# FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# WORKDIR /src
# COPY ["src/ECommerce.Webui/ECommerce.Webui.csproj", "src/ECommerce.Webui/"]
# RUN dotnet restore "src/ECommerce.Webui/ECommerce.Webui.csproj"
# COPY . .
# WORKDIR "/src/src/ECommerce.Webui"
# RUN dotnet build "ECommerce.Webui.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "ECommerce.Webui.csproj" -c Release -o /app/publish

# # Çalıştırma aşaması
# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .

# # Veritabanı dosyası kopyalama (gerekirse)
# COPY ECommerceDb /app/ECommerceDb

# # Migration'ları çalıştır
# RUN dotnet ef database update --project ECommerce.Webui.csproj --startup-project ECommerce.Webui.csproj
# ENTRYPOINT ["dotnet", "ECommerce.Webui.dll"]






# Temel çalışma ortamı - .NET 9.0 ASP.NET Core runtime imajı
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80  
# HTTP traf
EXPOSE 443  
# HTTPS trafiği için 443 portunu da açabilirsiniz

# Derleme ortamı - .NET 9.0 SDK imajı
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Önce proje dosyalarını kopyala ve bağımlılıkları yükle
# Bu adım, kaynak kodda değişiklik olmadığı sürece önbelleğe alınır ve derleme süresini hızlandırır
COPY ["src/Ecommerce.Core/*.csproj", "src/Ecommerce.Core/"]
COPY ["src/Ecommerce.Data/*.csproj", "src/Ecommerce.Data/"]
COPY ["src/Ecommerce.Service/*.csproj", "src/Ecommerce.Service/"]
COPY ["src/ECommerce.Webui/ECommerce.Webui.csproj", "src/ECommerce.Webui/"]

# Web UI projesini restore et (bağımlılıkları çöz)
RUN dotnet restore "src/ECommerce.Webui/ECommerce.Webui.csproj"

# Tüm kaynak kodları kopyala
COPY . .

# Web UI projesini derle
WORKDIR "/src/src/ECommerce.Webui"
RUN dotnet build "ECommerce.Webui.csproj" -c Release -o /app/build

# Entity Framework Core tools yükle (migration'lar için)
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Yayınlama aşaması - derlenen uygulamayı yayınla
FROM build AS publish
RUN dotnet publish "ECommerce.Webui.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Çalışma aşaması - yayınlanan uygulamayı temel imaja kopyala
FROM base AS final
WORKDIR /app

# Yayınlanan dosyaları kopyala
COPY --from=publish /app/publish .

# PostgreSQL istemci kütüphanelerini yükle
RUN apt-get update && apt-get install -y postgresql-client bash

# Uygulama için gerekli ortam değişkenleri
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Başlangıç betiği oluştur
RUN echo '#!/bin/bash \n\
# Veritabanı bağlantısını bekle \n\
echo "Veritabanı bağlantısı bekleniyor..." \n\
sleep 10 \n\
# Uygulamayı başlat \n\
dotnet ECommerce.Webui.dll' > /app/start.sh && chmod +x /app/start.sh

# Başlangıç betiğini çalıştır
ENTRYPOINT ["/app/start.sh"]





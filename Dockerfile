# Build aşaması
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/ECommerce.Webui/ECommerce.Webui.csproj", "src/ECommerce.Webui/"]
RUN dotnet restore "src/ECommerce.Webui/ECommerce.Webui.csproj"
COPY . .
WORKDIR "/src/src/ECommerce.Webui"
RUN dotnet build "ECommerce.Webui.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Webui.csproj" -c Release -o /app/publish

# Çalıştırma aşaması
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Migration'ları çalıştır
RUN dotnet ef database update --project ECommerce.Webui.csproj --startup-project ECommerce.Webui.csproj
ENTRYPOINT ["dotnet", "ECommerce.Webui.dll"]
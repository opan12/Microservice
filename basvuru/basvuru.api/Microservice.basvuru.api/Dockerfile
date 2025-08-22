FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1?? Tüm solution klasörünü kopyala
COPY . .

# 2?? Proje klasörüne geç
WORKDIR "/src/basvuru/basvuru.api/Microservice.basvuru.api"

# 3?? Restore, build, publish
RUN dotnet restore "Microservice.basvuru.api.csproj"
RUN dotnet build "Microservice.basvuru.api.csproj" -c Release -o /app/build
RUN dotnet publish "Microservice.basvuru.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4?? Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Microservice.basvuru.api.dll"]

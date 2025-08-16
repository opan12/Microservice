# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyasýný kopyala ve restore et
COPY Microservice.basvuru.api/Microservice.basvuru.api.csproj ./Microservice.basvuru.api/
WORKDIR /app/Microservice.basvuru.api
RUN dotnet restore

# Tüm kaynaklarý kopyala ve publish et
COPY Microservice.basvuru.api/ ./ 
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/Microservice.basvuru.api/out ./

ENTRYPOINT ["dotnet", "Microservice.basvuru.api.dll"]

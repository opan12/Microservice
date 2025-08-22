FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

WORKDIR "/src/Microservice.ApiGetAway"
RUN dotnet restore "Microservice.ApiGetAway.csproj"
RUN dotnet build "Microservice.ApiGetAway.csproj" -c Release -o /app/build
RUN dotnet publish "Microservice.ApiGetAway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Microservice.ApiGetAway.dll"]

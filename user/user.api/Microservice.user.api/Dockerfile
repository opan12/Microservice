FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/user/user.api/Microservice.user.api"
RUN dotnet restore "Microservice.user.api.csproj"
RUN dotnet build "Microservice.user.api.csproj" -c Release -o /app/build
RUN dotnet publish "Microservice.user.api.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Microservice.user.api.dll"]

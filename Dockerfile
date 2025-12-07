FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DDD.API/DDD.API.csproj", "DDD.API/"]
COPY ["DDD.Application/DDD.Application.csproj", "DDD.Application/"]
COPY ["DDD.Domain/DDD.Domain.csproj", "DDD.Domain/"]
COPY ["DDD.Infrastructure/DDD.Infrastructure.csproj", "DDD.Infrastructure/"]
RUN dotnet restore "DDD.API/DDD.API.csproj"
COPY . .
WORKDIR "/src/DDD.API"
RUN dotnet build "DDD.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DDD.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDD.API.dll"]

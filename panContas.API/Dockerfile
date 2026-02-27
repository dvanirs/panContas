FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["panContas.API/panContas.API.csproj", "panContas.API/"]
COPY ["panContas.Application/panContas.Application.csproj", "panContas.Application/"]
COPY ["panContas.Domain/panContas.Domain.csproj", "panContas.Domain/"]
COPY ["panContas.Infrastructure/panContas.Infrastructure.csproj", "panContas.Infrastructure/"]
RUN dotnet restore "./panContas.API/panContas.API.csproj"
COPY . .
WORKDIR "/src/panContas.API"
RUN dotnet build "./panContas.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./panContas.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "panContas.API.dll"]

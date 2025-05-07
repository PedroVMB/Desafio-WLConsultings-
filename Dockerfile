# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WLConsultings.API/WLConsultings.API.csproj", "WLConsultings.API/"]
COPY ["WLConsultings.Infrastructure/WLConsultings.Infrastructure.csproj", "WLConsultings.Infrastructure/"]
COPY ["WLConsultings.Application/WLConsultings.Application.csproj", "WLConsultings.Application/"]
COPY ["WLConsultings.Domain.Core/WLConsultings.Domain.Core.csproj", "WLConsultings.Domain.Core/"]
COPY ["WLConsultings.Domain/WLConsultings.Domain.csproj", "WLConsultings.Domain/"]
COPY ["WLConsultings.Domain.Services/WLConsultings.Domain.Services.csproj", "WLConsultings.Domain.Services/"]
RUN dotnet restore "./WLConsultings.API/WLConsultings.API.csproj"
COPY . .
WORKDIR "/src/WLConsultings.API"
RUN dotnet build "./WLConsultings.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WLConsultings.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WLConsultings.API.dll"]
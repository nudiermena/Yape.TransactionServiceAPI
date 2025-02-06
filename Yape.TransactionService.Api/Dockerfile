
# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Yape.TransactionService.Api/Yape.TransactionService.Api.csproj", "Yape.TransactionService.Api/"]
COPY ["Yape.TransactionService.Application/Yape.TransactionService.Application.csproj", "Yape.TransactionService.Application/"]
COPY ["Yape.TransactionService.Consumers/Yape.TransactionService.Consumers.csproj", "Yape.TransactionService.Consumers/"]
COPY ["Yape.TransactionService.Contracts/Yape.TransactionService.Contracts.csproj", "Yape.TransactionService.Contracts/"]
COPY ["Yape.TransactionService.Domain/Yape.TransactionService.Domain.csproj", "Yape.TransactionService.Domain/"]
COPY ["Yape.TransactionService.Infrastructure/Yape.TransactionService.Infrastructure.csproj", "Yape.TransactionService.Infrastructure/"]
RUN dotnet restore "./Yape.TransactionService.Api/Yape.TransactionService.Api.csproj"
COPY . .
WORKDIR "/src/Yape.TransactionService.Api"
RUN dotnet build "./Yape.TransactionService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Yape.TransactionService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yape.TransactionService.Api.dll"]
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Stock.Consumer/Stock.Consumer.csproj", "Stock.Consumer/"]
COPY ["Stock.Business/Stock.Business.csproj", "Stock.Business/"]
COPY ["Stock.Data/Stock.Data.csproj", "Stock.Data/"]
COPY ["Stock.Entity/Stock.Entity.csproj", "Stock.Entity/"]
COPY ["ECommerce.Shared/ECommerce.Shared.csproj", "ECommerce.Shared/"]

RUN dotnet restore "Stock.Consumer/Stock.Consumer.csproj"

COPY . .
WORKDIR /src
RUN dotnet build "Stock.Consumer/Stock.Consumer.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Stock.Consumer/Stock.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stock.Consumer.dll"]

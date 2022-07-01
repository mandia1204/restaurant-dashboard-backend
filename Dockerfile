FROM mcr.microsoft.com/dotnet/sdk:3.1.420-1-alpine3.16 AS builder

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/*.csproj ./

RUN dotnet restore

# Copy everything else and build
COPY ./src ./

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1.26-alpine3.16

WORKDIR /app

COPY --from=builder /app/out .

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "restaurant-dashboard-backend.dll"]
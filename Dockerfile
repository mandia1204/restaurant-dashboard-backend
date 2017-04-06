FROM microsoft/dotnet:1.1-sdk

RUN mkdir -p /var/www/aspnetcoreapp

WORKDIR /var/www/aspnetcoreapp

# copy csproj and restore as distinct layers
COPY ./src/restaurant-dashboard-backend.csproj /var/www/aspnetcoreapp
RUN dotnet restore

ENTRYPOINT ["dotnet", "run"]
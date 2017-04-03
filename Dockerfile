FROM microsoft/dotnet:1.1-sdk
WORKDIR /var/www/aspnetcoreapp

# copy csproj and restore as distinct layers
COPY ./src/restaurant-dashboard-backend.csproj .
RUN dotnet restore

# copy and build everything else
COPY ./src .

#RUN dotnet build
RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "out/restaurant-dashboard-backend.dll"]
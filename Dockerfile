FROM microsoft/aspnetcore:1.1
WORKDIR /app
ENTRYPOINT ["dotnet", "restaurant-dashboard-backend.dll"]
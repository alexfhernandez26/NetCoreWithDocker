#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV DB_CONECTION_STRING="Server=10.0.0.7,1433;Database=NetWithDocker;User Id=FernandoHernandez;Password=8299123705;TrustServerCertificate=true;"
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApiDocker.csproj", "."]
RUN dotnet restore "./WebApiDocker.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebApiDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiDocker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiDocker.dll"]
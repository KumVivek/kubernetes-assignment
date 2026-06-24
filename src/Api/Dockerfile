FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Api/Api.csproj", "./Api/"]
COPY ["Application/Application.csproj", "./Application/"]
COPY ["Domain/Domain.csproj", "./Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "./Infrastructure/"]
RUN dotnet restore "./Api/Api.csproj"

COPY . .
RUN dotnet publish "./Api/Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]


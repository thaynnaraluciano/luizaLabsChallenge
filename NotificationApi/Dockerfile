FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /

COPY *.sln ./
COPY Api/Api.csproj Api/
COPY CrossCutting/CrossCutting.Configuration/CrossCutting.Configuration.csproj CrossCutting/CrossCutting.Configuration/
COPY CrossCutting/CrossCutting.Exceptions/CrossCutting.Exceptions.csproj CrossCutting/CrossCutting.Exceptions/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.Data/Infrastructure.Data.csproj Infrastructure/Infrastructure.Data/
COPY Infrastructure/Infrastructure.Services/Infrastructure.Services.csproj Infrastructure/Infrastructure.Services/
COPY Tests/Tests.csproj Tests/

RUN dotnet restore

COPY . ./

WORKDIR /Api
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /out ./

EXPOSE 8080
EXPOSE 443

ENTRYPOINT ["dotnet", "Api.dll"]

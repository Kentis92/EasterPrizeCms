FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY EasterPrizeCms.Api/EasterPrizeCms.Api.csproj EasterPrizeCms.Api/
COPY EasterPrizeCms.Application/EasterPrizeCms.Application.csproj EasterPrizeCms.Application/
COPY EasterPrizeCms.Domain/EasterPrizeCms.Domain.csproj EasterPrizeCms.Domain/

RUN dotnet restore EasterPrizeCms.Api/EasterPrizeCms.Api.csproj

COPY . .

RUN dotnet publish EasterPrizeCms.Api/EasterPrizeCms.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "EasterPrizeCms.Api.dll"]
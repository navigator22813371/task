
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 7000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Api/Rick-and-Morty.WebApi/Rick-and-Morty.WebApi.csproj", "Api/Rick-and-Morty.WebApi/"]
COPY ["Infrastructure/Rick-and-Morty.Infrastructure.Persistence/Rick-and-Morty.Infrastructure.Persistence.csproj", "Infrastructure/Rick-and-Morty.Infrastructure.Persistence/"]
COPY ["Application/Rick-and-Morty.Application/Rick-and-Morty.Application.csproj", "Application/Rick-and-Morty.Application/"]
COPY ["Core/Rick-and-Morty.Domain/Rick-and-Morty.Domain.csproj", "Core/Rick-and-Morty.Domain/"]
RUN dotnet restore "Api/Rick-and-Morty.WebApi/Rick-and-Morty.WebApi.csproj"
COPY . .
WORKDIR "/src/Api/Rick-and-Morty.WebApi"
RUN dotnet build "Rick-and-Morty.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rick-and-Morty.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:7000
ENTRYPOINT ["dotnet", "Rick-and-Morty.WebApi.dll"]
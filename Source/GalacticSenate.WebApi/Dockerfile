#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Source/GalacticSenate.WebApi/GalacticSenate.WebApi.csproj", "Source/GalacticSenate.WebApi/"]
RUN dotnet restore "Source/GalacticSenate.WebApi/GalacticSenate.WebApi.csproj"
COPY . .
WORKDIR "/src/Source/GalacticSenate.WebApi"
RUN dotnet build "GalacticSenate.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GalacticSenate.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GalacticSenate.WebApi.dll"]
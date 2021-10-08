#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Source/PeopleAndOrganizations.ConsoleApp/PeopleAndOrganizations.ConsoleApp.csproj", "Source/PeopleAndOrganizations.ConsoleApp/"]
RUN dotnet restore "Source/PeopleAndOrganizations.ConsoleApp/PeopleAndOrganizations.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/Source/PeopleAndOrganizations.ConsoleApp"
RUN dotnet build "PeopleAndOrganizations.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PeopleAndOrganizations.ConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PeopleAndOrganizations.ConsoleApp.dll"]

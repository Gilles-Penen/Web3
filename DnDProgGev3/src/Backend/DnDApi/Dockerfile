# Stap 1: Gebruik een SDK image voor het bouwen van je project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Stel de werkdirectory in
WORKDIR /app

#Kopieer alle bestanden
COPY . .

RUN dotnet restore DnDApi.sln

# Kopieer de rest van de applicatie en bouw deze

RUN dotnet publish DnDApi/DnDApi.csproj -c Release -o /out

# Stap 2: Gebruik een runtime image om de applicatie uit te voeren
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# Stel de werkdirectory in
WORKDIR /app

# Kopieer de gepubliceerde applicatie naar de container
COPY --from=build /out .


# Expose poort 80 om toegang te krijgen tot de webapp
EXPOSE 8080



# Stel de startopdracht in voor de applicatie
ENTRYPOINT ["dotnet", "DnDApi.dll"]
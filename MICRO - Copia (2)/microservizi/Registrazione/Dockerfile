# Fase di base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase di build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia solo i file di progetto per il restore
COPY ["Registrazione.Api/Registrazione.Api.csproj", "Registrazione.Api/"]
COPY ["Registrazioni.Business/Registrazioni.Business.csproj", "Registrazioni.Business/"]
COPY ["Registrazioni.Repository/Registrazioni.Repository.csproj", "Registrazioni.Repository/"]
COPY ["Registrazioni.Shared/Registrazioni.Shared.csproj", "Registrazioni.Shared/"]

# Esegui il restore per Registrazione.Api.csproj
RUN dotnet restore "Registrazione.Api/Registrazione.Api.csproj"

# Copia il resto dei file sorgenti
COPY . .

# Compilazione del progetto
WORKDIR "/src/Registrazione.Api"
RUN dotnet build "Registrazione.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase di pubblicazione
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Registrazione.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Fase finale per l'esecuzione
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Registrazione.Api.dll"]

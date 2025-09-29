FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie les fichiers de projet et restaure les dépendances
COPY ["src/TodoApi/TodoApi.csproj", "src/TodoApi/"]
RUN dotnet restore "src/TodoApi/TodoApi.csproj"

# Copie tout le code et build
COPY . .
WORKDIR "/src/src/TodoApi"
RUN dotnet build "TodoApi.csproj" -c Release -o /app/build

# Installe les outils EF Core globalement
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Image finale pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "TodoApi.dll"]

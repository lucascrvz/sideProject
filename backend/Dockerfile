# ==============================
# Etapa 1: Build
# ==============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar la solución
COPY *.sln ./

# Copiar todos los csproj de los proyectos referenciados
COPY backend/Domain/*.csproj ./backend/Domain/
COPY backend/Application/*.csproj ./backend/Application/
COPY backend/Infrastructure/*.csproj ./backend/Infrastructure/
COPY backend/SideProject/*.csproj ./backend/SideProject/
COPY backend/Tests/*.csproj ./backend/Tests/

# Restaurar dependencias (cacheable)
RUN dotnet restore

# Copiar todo el código fuente
COPY backend/. ./backend/

# Publicar proyecto principal
WORKDIR /src/backend/SideProject
RUN dotnet publish -c Release -o /app/publish

# ==============================
# Etapa 2: Runtime
# ==============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "SideProject.dll"]

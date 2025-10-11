# sideProject

Proyecto full-stack de ejemplo (API .NET 9 + Angular 17) para manejo de presupuestos y cuotas.

## Contenido

- `backend/` - Solución y proyectos .NET (Domain, Application, Infrastructure, SideProject).
- `frontend/sideProject` - Aplicación Angular (SSR preparada).
- `Docker/` - `docker-compose.yml` para orquestar servicios.

## Tecnologías principales

- Backend: .NET 9 (ASP.NET Core Web API), Entity Framework Core, Npgsql (PostgreSQL), JWT Auth, Swashbuckle (OpenAPI).
- Frontend: Angular 17, Angular Universal (SSR), Node/Express para servir SSR.
- Contenedores: Docker, docker-compose.

## Requisitos

- .NET SDK 9.x
- Node.js 18+ (recomendado)
- pnpm o npm
- Docker & docker-compose (opcional, para ejecutar en contenedores)

## Estructura relevante

- `backend/SideProject` - Proyecto Web API principal. TargetFramework: net9.0.
- `backend/Application` - Lógica de aplicación y servicios.
- `backend/Domain` - Entidades y modelos de dominio.
- `backend/Infrastructure` - DbContext, migraciones y acceso a datos.
- `frontend/sideProject` - App Angular (scripts: `start`, `build`, `watch`, `test`, `serve:ssr:sideProject`).

## Variables de entorno / appsettings

El backend usa `appsettings.json` y `appsettings.Development.json` en `backend/SideProject`.
Por convención deberías configurar al menos:

- ConnectionStrings: `DefaultConnection` apunta a PostgreSQL.
- JWT: `Issuer`, `Audience`, `Secret` (para autenticación JWT).

Ejemplo mínimo (no guardar secretos en repositorios):

```json
{
	"ConnectionStrings": {
		"DefaultConnection": "Host=localhost;Database=sideproject;Username=postgres;Password=postgres"
	},
	"Jwt": {
		"Issuer": "sideproject",
		"Audience": "sideproject_users",
		"Secret": "REEMPLAZAR_POR_SECRETO_LARGO"
	}
}
```

## Cómo ejecutar (local)

1) Backend (desde `backend/`):

	 - Restaurar y ejecutar con dotnet:

```bash
cd backend/SideProject
dotnet restore
dotnet ef database update --project ../Infrastructure --startup-project . # si usas EF Migrations (opcional)
dotnet run
```

	 - La API por defecto queda escuchando en el puerto configurado (revisa `Properties/launchSettings.json`). Swagger estará disponible en `/swagger` cuando se ejecute en Development.

2) Frontend (desde `frontend/sideProject`):

```bash
cd frontend/sideProject
pnpm install   # o npm install
pnpm start     # o npm start
```

La app Angular por defecto corre en `http://localhost:4200`.

3) Frontend SSR (producción)

```bash
cd frontend/sideProject
pnpm build
node dist/side-project/server/server.mjs
```

## Cómo ejecutar con Docker

Existe un `docker-compose.yml` en `Docker/`. Ejemplo genérico:

```bash
cd Docker
docker-compose up --build
```

Este comando levantará los servicios definidos (API, frontend, base de datos). Revisa el archivo `Docker/docker-compose.yml` para detalles de puertos y variables de entorno.

## Migraciones y base de datos

Las migraciones están en `backend/Infrastructure/Migrations`.
Para aplicar migraciones localmente:

```bash
cd backend/SideProject
dotnet ef database update --project ../Infrastructure --startup-project .
```

Nota: Ajusta los argumentos si tu estructura de proyectos difiere.

## Tests

- Backend: hay un proyecto de tests en `backend/Tests`. Ejecuta:

```bash
cd backend/Tests
dotnet test
```

- Frontend: usar `pnpm test` o `npm test` en `frontend/sideProject`.

## Endpoints principales

Revisa `backend/SideProject/Controllers` para los controladores expuestos. Ejemplos incluidos:

- `AuthController` - rutas de autenticación (login/register).
- `PresupuestoController` - rutas relacionadas con presupuestos y cuotas.

Usa Swagger para ver la documentación interactiva (`/swagger`) cuando ejecutes la API en modo Development.

## Contribuciones

1. Crea un issue describiendo la propuesta o bug.
2. Crea una rama con un nombre claro: `feature/<descripcion>` o `fix/<descripcion>`.
3. Envía un pull request contra `main` con una descripción y pasos para reproducir o validar.

## Notas y supuestos

- El proyecto usa PostgreSQL (Npgsql). Si usas otra DB actualiza la cadena de conexión y paquetes.
- Asumí variables JWT comunes; reemplaza `Secret` por una clave segura en producción.
- Ajusta puertos y URLs en `appsettings` o `launchSettings.json` según necesites.

## Preguntas frecuentes

- ¿Dónde está la lógica de negocio? -> `backend/Application/Services`.
- ¿Dónde están las entidades? -> `backend/Domain/Entities`.

---

Si quieres, puedo:

- Agregar ejemplos concretos de llamadas curl para los endpoints principales.
- Añadir badges (build, coverage) si tienes CI configurado.
- Documentar variables de entorno exactas tomando los valores de `appsettings.Development.json`.

Avísame qué prefieres y lo agrego.

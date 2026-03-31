# Taller - Sistema base (Clean Architecture, .NET)

Base de solución para taller de chapa y pintura con flujo:

**Siniestro → Presupuesto → Orden de Trabajo → Tareas por Sector → Facturación**

## Proyectos
- `Taller.API`: ASP.NET Core Web API + JWT + Swagger.
- `Taller.Application`: casos de uso y DTOs.
- `Taller.Domain`: entidades y enums.
- `Taller.Infrastructure`: EF Core + SQL Server + repositorios + seguridad.
- `Taller.Desktop`: cliente WPF base (Login + Dashboard).
- `Taller.Worker`: worker IMAP base para aprobación/rechazo por email.
- `Taller.Tests`: pruebas unitarias iniciales.

## Qué está implementado
- Estructura `.sln` con separación por capas (Clean Architecture).
- Entidades clave de dominio.
- `DbContext` con relaciones mínimas y unicidad para patente/nro de siniestro.
- JWT básico con roles.
- **Controller real de Siniestro**:
  - `POST /api/siniestros`
  - `GET /api/siniestros/{id}`
- Caso de uso real: `CrearSiniestroUseCase`.
- WPF básico: Login y Dashboard inicial.
- Worker base que parsea `APROBADO #ID` / `RECHAZADO #ID` usando `ApprovalEmailParser`.
- Tests unitarios para `ApprovalEmailParser` y caso de uso de creación de siniestro.

## Variables de entorno sugeridas
- `ConnectionStrings__Default`
- `Jwt__Key`
- `Jwt__Issuer`
- `Jwt__Audience`
- `Imap__Host`
- `Imap__User`
- `Imap__Password`

## Comandos (entorno con .NET SDK instalado)
```bash
dotnet restore Taller.sln
dotnet build Taller.sln -c Debug
dotnet test tests/Taller.Tests/Taller.Tests.csproj -c Debug
dotnet ef migrations add Initial --project src/Taller.Infrastructure --startup-project src/Taller.API
dotnet ef database update --project src/Taller.Infrastructure --startup-project src/Taller.API
dotnet run --project src/Taller.API
```

## Prueba rápida API
1. `POST /api/auth/login` con `{ "username": "jefe", "password": "123" }`.
2. Tomar `access_token`.
3. `POST /api/siniestros` con `Authorization: Bearer <token>`.
4. `GET /api/siniestros/{id}` para verificar persistencia.

## Troubleshooting de tests
Si `dotnet test` falla por SDK ausente (`dotnet: command not found`), usá el script:

```bash
./scripts/run-tests.sh
```

El script valida prerequisitos y muestra pasos de instalación/ejecución.

## Entorno recomendado para correr tests (SDK incluido)
Si tu máquina/CI no tiene `dotnet`, podés usar cualquiera de estas opciones:

### Opción A: Dev Container (VS Code)
1. Abrí el repo en VS Code.
2. Ejecutá: **Dev Containers: Reopen in Container**.
3. El contenedor instala .NET 8 y ejecuta `dotnet restore` automáticamente.
4. Luego corré:
   ```bash
   dotnet test tests/Taller.Tests/Taller.Tests.csproj -c Debug
   ```

### Opción B: Docker Compose para tests
```bash
docker compose -f docker-compose.test.yml run --rm tests
```

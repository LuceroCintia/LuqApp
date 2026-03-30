# Sistema para Taller de Chapería y Pintura

Este documento consolida y ordena la propuesta funcional/técnica del sistema para el taller.

## 1) Objetivo
Ordenar el flujo del taller, reducir tiempos muertos, evitar errores administrativos y tener control integral del negocio.

## 2) Flujo actual y problemas
### Flujo típico
1. Ingreso del vehículo (cliente o aseguradora).
2. Registro del siniestro.
3. Generación de presupuesto.
4. Aprobación (cliente/aseguradora).
5. Asignación de trabajo.
6. Reparación (chapa + pintura).
7. Entrega del vehículo.

### Problemas detectados
- Falta de control de estados.
- Presupuestos inconsistentes.
- Desorden de stock.
- Mala comunicación con clientes.
- Retrasos sin seguimiento.
- Pérdida de información.

## 3) Módulos del sistema
1. Clientes y vehículos.
2. Siniestros.
3. Presupuestos.
4. Órdenes de trabajo.
5. Seguimiento (Kanban/estados).
6. Stock.
7. Agenda.
8. Facturación.
9. Reportes.

## 4) Actores y responsabilidades
- **Administrador**: acceso total.
- **Recepcionista**: ingresos de vehículos, siniestros, facturación, pagos, stock/gastos administrativos.
- **Jefe de taller**: presupuestos, asignación y control de órdenes/tareas.
- **Operario (chapista/pintor)**: ejecución de tareas, actualización de estado, consumo de materiales.
- **Cliente**: consulta de estado/notificaciones.
- **Aseguradora**: recepción/aprobación/rechazo de presupuestos, pagos.

## 5) Casos de uso principales
### Ingreso
- Registrar cliente.
- Registrar vehículo.
- Registrar siniestro.
- Cargar fotos del daño.

### Presupuestos
- Crear/editar presupuesto.
- Enviar a aseguradora/cliente.
- Registrar aprobación/rechazo/revisión.

### Órdenes y tareas
- Crear orden desde presupuesto.
- Asignar sector por ítem (chapa/pintura/etc.).
- Generar tareas por sector.
- Agregar tareas manuales.

### Seguimiento
- Cambiar estado del vehículo/orden.
- Ver tablero Kanban.
- Detectar retrasos.

### Stock
- Alta/actualización de ítems.
- Movimientos de entrada/salida.
- Alertas por stock mínimo.

### Facturación y pagos
- Emitir factura sobre orden finalizada.
- Enviar factura (cliente/aseguradora).
- Registrar pagos y comprobantes.

### Reportes
- Volumen de trabajos por período.
- Tiempos por sector.
- Productividad y rentabilidad.

## 6) Estados sugeridos
### Estado del trabajo/siniestro
- Ingresado
- En presupuesto
- Esperando aprobación
- Presupuesto aprobado
- Esperando repuestos
- En reparación
- En pintura
- Finalizado
- Entregado
- Facturado
- Cobrado

### Estado de tarea
- Pendiente
- En proceso
- Hecha

## 7) Modelo de datos (entidades)
- Cliente
- Vehiculo
- Aseguradora
- Siniestro
- Presupuesto
- DetallePresupuesto
- OrdenTrabajo
- Sector
- Tarea
- Operario
- Proveedor
- Stock
- MovimientoStock
- Factura
- Pago

### Relaciones clave
- Cliente 1..N Siniestro
- Vehiculo 1..N Siniestro
- Aseguradora 1..N Siniestro (opcional en siniestro)
- Siniestro 1..N Presupuesto
- Presupuesto 1..N DetallePresupuesto
- Presupuesto 1..1 OrdenTrabajo
- OrdenTrabajo 1..N Tarea
- Sector 1..N Tarea
- Sector 1..N Operario
- Stock 1..N MovimientoStock
- Proveedor 1..N Stock
- OrdenTrabajo 1..1 Factura
- Factura 1..N Pago

## 8) Reglas de negocio críticas
### Generar orden desde presupuesto
1. Tomar `DetallePresupuesto`.
2. Asignar sector por ítem en UI.
3. Crear una tarea por asignación (no replicar a todos los sectores).
4. Permitir tareas manuales adicionales.
5. Estado inicial de cada tarea: `pendiente`.

### Actualización de estado general
- Al avanzar/cerrar tareas por sector, recalcular estado global de la orden.

### Stock
- Al ejecutar tareas con consumo, registrar `MovimientoStock` de tipo `salida`.

## 9) API REST (base `/api`)
### Auth
- `POST /auth/login`
- `POST /auth/logout`

### CRUD y operaciones
- Clientes: `GET/POST/PUT/DELETE /clientes`
- Vehículos: `GET/POST/PUT/DELETE /vehiculos`
- Aseguradoras: `GET/POST/PUT/DELETE /aseguradoras`
- Siniestros: `GET/POST/PUT /siniestros`, `GET /siniestros/{id}/presupuestos`
- Presupuestos: `GET/POST/PUT /presupuestos`, `POST /presupuestos/{id}/enviar|aprobar|rechazar`
- Detalles: `POST /presupuestos/{id}/detalles`, `PUT/DELETE /detalles/{id}`
- Órdenes: `POST /ordenes`, `GET/PUT /ordenes/{id}`, `POST /ordenes/{id}/finalizar`
- Tareas: `GET /ordenes/{id}/tareas`, `POST /ordenes/{id}/tareas`, `PUT /tareas/{id}`, `GET /tareas?sector={id}`
- Sectores: `GET/POST /sectores`
- Operarios: `GET/POST /operarios`, `PUT /operarios/{id}`
- Stock: `GET/POST /stock`, `PUT /stock/{id}`
- Movimientos: `GET/POST /stock/movimientos`
- Facturas: `POST /facturas`, `GET /facturas/{id}`
- Pagos: `POST /pagos`, `GET /pagos/{id}`

## 10) Seguridad
Modelo recomendado: **RBAC**.

Entidades:
- Usuario
- Rol
- Permiso
- RolPermiso

Lineamientos:
- JWT para autenticación.
- Password hasheada con bcrypt.
- Middleware de autorización por rol/permisos.
- Validación de backend obligatoria.
- No guardar contraseñas en texto plano.

## 11) Auditoría y trazabilidad
Tabla `Auditoria` sugerida:
- entidad
- id_entidad
- accion
- valor_anterior (JSON)
- valor_nuevo (JSON)
- id_usuario
- fecha

Registrar obligatoriamente:
- Presupuestos (alta/edición/cambio de estado).
- Órdenes de trabajo.
- Cambios de estado de tareas.
- Movimientos de stock.
- Facturación y pagos.

## 12) Integración de email
### Envío
- Al enviar presupuesto, notificar a aseguradora.

### Recepción automática (IMAP)
1. Identificar `nroSiniestro` o `id_presupuesto`.
2. Detectar intención (`aprobado`, `rechazado`, `modificar`).
3. Actualizar estado.
4. Auditar evento.

Sugerencia: formato forzado en la respuesta de aseguradora (ej. `APROBADO #123`).

Tabla opcional: `EmailLog`.

## 13) Frontend propuesto
- React + TypeScript.
- React Query para datos remotos.
- MUI o Tailwind para UI.
- React Router para navegación.

Vistas:
- Dashboard (KPIs + alertas).
- Kanban de órdenes.
- Detalle de orden por sector.
- Presupuestos.
- Siniestros.
- Stock.

## 14) Arquitectura .NET propuesta
- Backend: ASP.NET Core Web API (.NET 8+).
- ORM: Entity Framework Core.
- DB: SQL Server o PostgreSQL.
- Logging: Serilog.
- Worker aparte para procesamiento de emails (BackgroundService).

Solución sugerida:
- `Taller.API`
- `Taller.Application`
- `Taller.Domain`
- `Taller.Infrastructure`
- `Taller.Desktop` (WPF)
- `Taller.Worker`

## 15) MVP recomendado
1. Alta de siniestro.
2. Alta de presupuesto.
3. Crear orden desde presupuesto.
4. Generar tareas por sector.
5. Vista de tareas por sector (operario).

## 16) Próximos pasos
Opción A: generar esqueleto completo de la solución .NET.

Opción B: implementar primer caso end-to-end:
- endpoint de creación de siniestro,
- endpoint de creación de presupuesto,
- endpoint de creación de orden + tareas,
- pantalla WPF mínima para consultar tareas por sector.

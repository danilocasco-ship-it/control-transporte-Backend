# Application — capa de dominio del módulo

Coloca aquí DTOs, interfaces y servicios del negocio.

- `SQL/Auth` — sesión post-SSO (usuario handoff + DTOs)
- `Configuration` — mapper / xml
- `Utilidades` — correo, excel

Los endpoints de negocio van en `API_CONTROL_TRANSPORTE/Controllers`.
El SSO permanece en `API_CONTROL_TRANSPORTE/IntegracionLogin`.

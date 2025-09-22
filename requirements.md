# 📌 Requerimientos del Proyecto - FinanzasApp

Este documento define los requerimientos funcionales y no funcionales del proyecto **FinanzasApp**, una aplicación para la gestión personal de finanzas.

## 🔹 Requerimientos Funcionales

1. **Gestión de cuentas**
   - Registrar múltiples cuentas (ejemplo: tarjeta principal, tarjeta secundaria, cuenta de ahorros).
   - Permitir transferencias entre cuentas.

2. **Gestión de ingresos y gastos**
   - Registrar ingresos (ejemplo: sueldo, transferencias).
   - Registrar gastos al contado.
   - Registrar compras en cuotas (ejemplo: producto en 3 cuotas).
   - Asociar gastos a categorías (ejemplo: alimentación, transporte, deudas).
   - Marcar el pago de cada cuota en la fecha correspondiente.

3. **Gestión de deudas/compromisos futuros**
   - Visualizar compras pendientes de pagar (cuotas activas).
   - Calcular cuánto falta por pagar en total.
   - Estimar gasto futuro considerando las cuotas programadas.

4. **Visualización y reportes**
   - Mostrar un resumen general de ingresos vs gastos por mes.
   - Gráficos de distribución de gastos por categoría.
   - Gráfico de proyección de pagos futuros.

5. **Usuarios (futuro, opcional)**
   - Registro/Login de usuario (si se requiere multiusuario).
   - Roles básicos (ejemplo: usuario normal, admin).


## 🔹 Requerimientos No Funcionales

1. **Tecnologías base**
   - Backend: ASP.NET Core Web API con arquitectura limpia (API, Application, Domain, Infrastructure).
   - Frontend: Angular.
   - Base de datos: PostgreSQL.

2. **Infraestructura**
   - Contenerización con Docker para frontend, backend y base de datos.
   - Orquestación local con `docker compose`.

3. **Calidad del código**
   - Uso de control de versiones con GitHub.
   - Definición de licencia (MIT recomendado).
   - Implementación de pruebas unitarias en el backend (xUnit).

4. **CI/CD**
   - Pipeline de GitHub Actions que ejecute:
     - Build y test del backend.
     - Build del frontend.
     - Construcción de imágenes Docker.
     - (Opcional) Deploy automático en servidor personal.

5. **Seguridad**
   - Uso de variables de entorno para credenciales y connection strings.
   - Protección básica de la API (CORS, HTTPS).

6. **Escalabilidad**
   - Monorepo con separación clara entre backend, frontend e infraestructura.
   - Posibilidad de migrar a la nube (ej. AWS o Azure) en el futuro.

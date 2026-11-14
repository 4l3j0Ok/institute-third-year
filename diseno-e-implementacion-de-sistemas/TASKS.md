---
status: in progress
---

# TASKS

Consigna formal completa: [CONSIGNA.md](CONSIGNA.md). Fuente original: `resources/tasks/entrega_tp_grupal.txt`. Grupos de hasta 4 personas, 1 entrega por representante. Sin fecha en Classroom.

## Supuestos (la consigna cita secciones 3, 5, 6.1 y 6.2 que no están en el Classroom)

- 5 CU: Autenticar Usuario, Consultar Saldo, Extraer Efectivo, Realizar Transferencia, Emitir Ticket y Devolver Tarjeta (PNG del docente).
- 8 procesos DFD: Leer tarjeta, Validar tarjeta, Validar PIN, Consultar saldo, Extraer efectivo, Realizar transferencia, Registrar transacción, Emitir ticket y devolver tarjeta.
- 3 almacenes: D1 Sesión, D2 Tarjetas leídas, D3 Log de transacciones.
- 8 clases: Cliente, Tarjeta, Cuenta, Sesion, Operacion (abstracta), Consulta, Extraccion, Transferencia.
- «include»: operaciones → Autenticar; «extend»: Emitir Ticket extiende a las operaciones.
- Item 9 usa la versión del 2/11: actividad del SISTEMA completo.

## Pretareas (hechas)

- [x] Recursos descargados en `resources/` y listados en `RESOURCES.md`
- [x] Script de scraping `scripts/scrape_classroom.py`
- [x] Base del proyecto C# + Windows Forms (`src/Cajero`), migrada desde Python + PySide6 porque el equipo conoce C#

## Diagramas (borrador generado por el agente; el usuario debe revisarlos)

Reglas y fuentes en las skills `.agents/skills/dis-*`. Verificación: `python3 scripts/verificar_diagramas.py` (0 errores).

- [x] 1. Diagrama de Contexto (24 flujos de la tabla oficial, uno por flecha)
- [x] 2. DFD Nivel 0 (balanceado con el Contexto; `Datos de sesión` agregado para que los procesos reciban lo que emiten)
- [x] 3. Diccionario de Datos (notación de `DD.pdf`; cubre todos los flujos, almacenes y elementos)
- [x] 4. Casos de Uso (asociaciones de secundarios desde el CU hacia el actor)
- [x] 5. Clases (8 clases, cada relación existe en el código) y 5b. Objetos con nombres subrayados
- [x] 6. Secuencia Autenticar Usuario
- [x] 7. Secuencia Extraer Efectivo
- [x] 8. Estado de Sesion (igual al enum `EstadoSesion`)
- [x] 9. Actividad del sistema (calles, guardas entre corchetes, PIN con 3 intentos)
- [x] Skills por diagrama y de convenciones (`dis-*`)
- [ ] Revisar contra las guías y rehacer en StarUML si el docente lo exige
- [x] `ejemploDiagramaActividad.rar` abierto: contiene un HTML (calles Cliente / Cajero / Sistemas Externos)

## Código (C# + Windows Forms, 3 capas)

- [x] Capa Datos: `SucursalDB`, `CentralDB` simulados (PIN recibido encriptado con SHA-256)
- [x] Capa Lógica: Operacion (abstracta), Consulta, Extraccion, Transferencia, Cliente, Tarjeta, Cuenta, Sesion (campos `_x`, propiedades get/set, constructores explícitos, un tipo por archivo)
- [x] Capa Presentación: `FormCajero` (Designer de Windows Forms) con skin de cajero (tarjeta → PIN → menú → las 3 operaciones → ticket)
- [x] Convenciones de C# exigidas por `dotnet build` (`src/.editorconfig`)
- [x] Pruebas unitarias de Datos y Lógica (`src/Cajero.Tests`, incluye los estados del diagrama y los 3 intentos de PIN)
- [ ] Probar la app a mano y ajustar el código a los diagramas definitivos
- [ ] Abrir `FormCajero` en el diseñador de Visual Studio y ajustar el aspecto (ya se probó que arranca en Linux con Wine (`src/ejecutar-linux.sh`); falta verlo en Windows)

## Entrega

- [ ] Completar integrantes en `README.md`
- [ ] Pedir al docente fecha de entrega y formato

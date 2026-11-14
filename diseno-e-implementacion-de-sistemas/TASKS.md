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
- [x] Base del proyecto Python + PySide6 (`src/cajero`)

## Diagramas (borrador generado por el agente; el usuario debe revisarlos)

- [x] 1. Diagrama de Contexto
- [x] 2. DFD Nivel 0 (verificar balanceo con contexto)
- [x] 3. Diccionario de Datos
- [x] 4. Casos de Uso
- [x] 5. Clases (falta subrayar objetos si se agrega diagrama de objetos)
- [x] 6. Secuencia Autenticar Usuario
- [x] 7. Secuencia Extraer Efectivo
- [x] 8. Estado de Sesion
- [x] 9. Actividad del sistema
- [ ] Revisar contra guías y ejemplos; rehacer en StarUML si el docente lo exige
- [ ] Verificar `ejemploDiagramaActividad.rar` (descargó 3.6 KB)

## Código (Python + PySide6, 3 capas)

- [x] Capa Datos: `SucursalDB`, `CentralDB` simulados
- [x] Capa Lógica: Operacion (abstracta), Consulta, Extraccion, Transferencia, Cliente, Tarjeta, Cuenta, Sesion (atributos `_privados`, propiedades)
- [x] Capa Presentación: `FormCajero` con skin de cajero (tarjeta → PIN → menú → las 3 operaciones → ticket)
- [ ] Probar la app a mano y ajustar el código a los diagramas definitivos
- [ ] Pruebas básicas (opcional)

## Entrega

- [ ] Completar integrantes en `README.md`
- [ ] Pedir al docente fecha de entrega y formato

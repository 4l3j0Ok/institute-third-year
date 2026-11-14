---
name: dis-diagrama-secuencia
description: Crear, revisar o corregir los Diagramas de Secuencia del Cajero Automático (Autenticar Usuario en diagrams/05_secuencia_autenticar.puml y una operación a elección, Extraer Efectivo, en 06_secuencia_extraccion.puml). Usar cuando se hable de secuencia, líneas de vida, mensajes sincrónicos, retornos, focos de control, activaciones o participantes como :AtmForm (la :PantallaCajero de la consigna), :BranchDB (SucursalDB) y :CentralDB.
---

# Diagramas de Secuencia

Ítems 6 y 7 de la consigna. Lee primero `dis-convenciones`; se hacen con los CU y las clases ya definidos.

## Fuentes
- `resources/CU_autenticarUsuario.png`, `CU_extraerefectivo.png` (flujo principal paso a paso).
- Debrauwer cap. 5 (línea de vida, mensajes, retorno no sistemático, autoenvío, escenario). Guía U3 §3.4. Consigna ítems 6 y 7.

## Reglas
1. **Participantes** (consigna ítem 6): `Cliente` (actor) y los objetos `:AtmForm`, `:BranchDB`, `:CentralDB`. La extracción usa `Cliente`, `:AtmForm` y `:CentralDB`. `:AtmForm` es la `:PantallaCajero` de la consigna y representa al cajero (capas `Presentation` y `Logic`: `AtmForm` y `Session`); `BranchDB` es `SucursalDB`. Una nota en el diagrama da la equivalencia.
2. **Nombres de objetos subrayados**: `participant "<u>:AtmForm</u>" as PC`.
3. **Todos los mensajes sincrónicos** (`->`, punta rellena), con **parámetros**: `ValidateCard(number, expirationDate)`. Sin mensajes asincrónicos (`->>`).
4. **Nombres de mensajes** = métodos del código en PascalCase (`ValidatePin`, `AuthorizeDebit`) con los mismos parámetros que el método C#.
5. **Retornos en línea discontinua** (`-->`), en itálica y con el valor devuelto: `PC --> Cliente : //pinRequest//`. Un retorno responde a un mensaje anterior: las solicitudes del cajero al cliente (pedir PIN, pedir monto) se modelan como el retorno de la llamada del cliente, no como llamadas nuevas.
6. **Focos de control**: `activate` cuando el objeto empieza a ejecutar el método y `deactivate` cuando devuelve. Siempre balanceados; el foco del receptor contiene al del emisor que espera.
7. **Autoenvío** para acciones internas del cajero (`DispenseCash(amount)`, `RegisterOperation(...)`) con `PC -> PC`.
8. **Escenario principal**, sin alternativas (el CU lo describe como flujo principal). Las ramas de error van en el diagrama de actividad.
9. Mensajes y valores devueltos en inglés (`pinRequest`, `cardStatus`); `Cliente` queda como actor de la consigna.
10. El orden y los datos siguen el flujo del PNG del CU, paso por paso.

## Sintaxis
```
@startuml 05_secuencia_autenticar
skinparam sequenceMessageAlign center
actor Cliente
participant "<u>:AtmForm</u>" as PC
participant "<u>:BranchDB</u>" as SDB

Cliente -> PC : InsertCard(number, expirationDate)
activate PC
PC -> SDB : ValidateCard(number, expirationDate)
activate SDB
SDB --> PC : //cardStatus//
deactivate SDB
PC --> Cliente : //pinRequest//
deactivate PC
@enduml
```

## Errores frecuentes
- Mensajes sin paréntesis o sin parámetros.
- Retornos con flecha continua, o llamadas a un actor dibujadas como retorno sin que haya un mensaje previo.
- `activate` sin su `deactivate`.
- Nombres de mensaje que no existen en el código.
- Participantes sin subrayar, o más de los pedidos.

## Verificación
`python3 scripts/verificar_diagramas.py` (participantes, paréntesis, no asincrónicos, activate/deactivate balanceados, subrayado). Mirar el PNG: focos bien ubicados y retornos discontinuos.

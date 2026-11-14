---
name: dis-diagrama-estado
description: Crear, revisar o corregir el Diagrama de Estado de la clase Session (Sesion) del Cajero Automático en diagrams/07_estado_sesion.puml (estados Started, Authenticating, Validating, Operating, Closing y Closed, es decir Iniciada, Autenticando, Validando, Operando, Cerrando y Cerrada, con transiciones, eventos, guardas y acciones) y mantenerlo igual al enum SessionState y a los métodos de Session.cs. Usar cuando se hable de estados, transiciones, eventos disparadores, ciclo de vida o máquina de estados.
---

# Diagrama de Estado de `Session` (Sesion)

Ítem 8 de la consigna. Lee primero `dis-convenciones`. Debe coincidir con `SessionState` y con los métodos de `src/Atm/Logic/Session.cs` (checklist de la consigna).

## Fuentes
- Debrauwer cap. 8 (estado inicial único, estados finales, transición = evento + `[guarda]` + `/ actividad`, transición reflexiva). Consigna ítem 8. Reglas de negocio de `CONSIGNA.md` §3.3 (3 intentos de PIN).

## Estados (exactamente estos 6)
Los nombres del diagrama son los del enum, en inglés; cada estado muestra además el nombre de la consigna como texto (`state Validating : Validando (PIN)`):

| Consigna | Enum |
|---|---|
| Iniciada | `Started` |
| Autenticando (tarjeta) | `Authenticating` |
| Validando (PIN) | `Validating` |
| Operando | `Operating` |
| Cerrando | `Closing` |
| Cerrada | `Closed` |

## Reglas
1. **Un estado inicial** (punto negro) que lleva a `Started`, y un estado final tras `Closed`.
2. **Toda transición lleva un evento disparador**; los eventos son los métodos de `Session` en PascalCase con paréntesis (`InsertCard(number, expirationDate)`, `Authenticate(pin)`, `Execute(operation)`, `Finish()`, `Close()`). Las guardas son texto de dominio en español.
3. **Guardas entre corchetes** a la derecha del evento: `[PIN incorrecto e intentos < 3]`. Las guardas de transiciones que salen del mismo estado con el mismo evento son excluyentes y exhaustivas.
4. **Acciones** tras `/`: `/ BlockCard`, `/ ReturnCard`.
5. **Transición reflexiva** para lo que no cambia de estado: reintento de PIN en `Validating`, otra operación en `Operating`.
6. Reglas del negocio visibles: el tercer PIN incorrecto bloquea la tarjeta y cierra; una tarjeta bloqueada o vencida cierra la sesión sin pedir PIN; `Closed` elimina los datos de autenticación.
7. Los nombres y el orden de los estados son los del enum. No agregar estados que el código no tenga.
8. Todo camino de salida del código está dibujado, por ejemplo cancelar durante el PIN (`Finish()` desde `Validating`).

## Sintaxis
```
@startuml 07_estado_sesion
state Validating : Validando (PIN)
[*] --> Started
Started --> Authenticating : InsertCard(number, expirationDate)
Authenticating --> Validating : [tarjeta válida]
Validating --> Validating : Authenticate(pin) [PIN incorrecto e intentos < 3]
Validating --> Closing : Authenticate(pin) [intentos = 3] / BlockCard
Closed --> [*]
@enduml
```

## Errores frecuentes
- Transición sin evento (solo las automáticas, desde el estado de actividad, no llevan evento: aclarar).
- Estados con nombres distintos al enum.
- Olvidar el camino de cancelación o de tarjeta inválida.
- Dos transiciones con guardas que se superponen o que dejan un caso sin cubrir.

## Verificación
`python3 scripts/verificar_diagramas.py` (los 6 estados, transiciones con evento). Luego recorrer `Session.cs` y comprobar que cada asignación de `_state` está dibujada.

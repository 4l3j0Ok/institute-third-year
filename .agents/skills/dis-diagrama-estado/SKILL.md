---
name: dis-diagrama-estado
description: Crear, revisar o corregir el Diagrama de Estado de la clase Sesion del Cajero Automático en diagrams/07_estado_sesion.puml (estados Iniciada, Autenticando, Validando, Operando, Cerrando y Cerrada con transiciones, eventos, guardas y acciones) y mantenerlo igual al enum EstadoSesion y a los métodos de Sesion.cs. Usar cuando se hable de estados, transiciones, eventos disparadores, ciclo de vida o máquina de estados.
---

# Diagrama de Estado de `Sesion`

Ítem 8 de la consigna. Lee primero `dis-convenciones`. Debe coincidir con `EstadoSesion` y con los métodos de `src/Cajero/Logica/Sesion.cs` (checklist de la consigna).

## Fuentes
- Debrauwer cap. 8 (estado inicial único, estados finales, transición = evento + `[guarda]` + `/ actividad`, transición reflexiva). Consigna ítem 8. Reglas de negocio de `CONSIGNA.md` §3.3 (3 intentos de PIN).

## Estados (exactamente estos 6)
`Iniciada`, `Autenticando` (tarjeta), `Validando` (PIN), `Operando`, `Cerrando`, `Cerrada`.

## Reglas
1. **Un estado inicial** (punto negro) que lleva a `Iniciada`, y un estado final tras `Cerrada`.
2. **Toda transición lleva un evento disparador**; los eventos son los métodos de `Sesion` en PascalCase con paréntesis (`IngresarTarjeta()`, `Autenticar(pin)`, `Ejecutar(operacion)`, `Finalizar()`, `Cerrar()`).
3. **Guardas entre corchetes** a la derecha del evento: `[PIN incorrecto e intentos < 3]`. Las guardas de transiciones que salen del mismo estado con el mismo evento son excluyentes y exhaustivas.
4. **Acciones** tras `/`: `/ BloquearTarjeta`, `/ DevolverTarjeta`.
5. **Transición reflexiva** para lo que no cambia de estado: reintento de PIN en `Validando`, otra operación en `Operando`.
6. Reglas del negocio visibles: el tercer PIN incorrecto bloquea la tarjeta y cierra; una tarjeta bloqueada o vencida cierra la sesión sin pedir PIN; `Cerrada` elimina los datos de autenticación.
7. Los nombres y el orden de los estados son los del enum. No agregar estados que el código no tenga.
8. Todo camino de salida del código está dibujado, por ejemplo cancelar durante el PIN (`Finalizar()` desde `Validando`).

## Sintaxis
```
@startuml 07_estado_sesion
[*] --> Iniciada
Iniciada --> Autenticando : IngresarTarjeta()
Autenticando --> Validando : [tarjeta válida]
Validando --> Validando : Autenticar(pin) [PIN incorrecto e intentos < 3]
Validando --> Cerrando : Autenticar(pin) [intentos = 3] / BloquearTarjeta
Cerrada --> [*]
@enduml
```

## Errores frecuentes
- Transición sin evento (solo las automáticas, desde el estado de actividad, no llevan evento: aclarar).
- Estados con nombres distintos al enum.
- Olvidar el camino de cancelación o de tarjeta inválida.
- Dos transiciones con guardas que se superponen o que dejan un caso sin cubrir.

## Verificación
`python3 scripts/verificar_diagramas.py` (los 6 estados, transiciones con evento). Luego recorrer `Sesion.cs` y comprobar que cada asignación de `Estado` está dibujada.

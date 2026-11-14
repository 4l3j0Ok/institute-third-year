---
name: dis-casos-de-uso
description: Crear, revisar o corregir el Diagrama de Casos de Uso del Cajero Automático en diagrams/03_casos_de_uso.puml (5 casos de uso, 3 actores, relaciones include y extend, actores secundarios a la derecha). Usar cuando se hable de casos de uso, actores primarios o secundarios, include, extend, puntos de extensión o frontera del sistema.
---

# Diagrama de Casos de Uso

Ítem 4 de la consigna. Lee primero `dis-convenciones`.

## Fuentes
- `resources/CU_*.png` (actor primario, secundarios, precondición, flujo de cada CU).
- Debrauwer cap. 4 (actores, asociaciones, include, extend, frontera). Guía U3 §3.2. Consigna ítem 4.

## Contenido fijo
- **Actores**: `Cliente` (primario); `Sucursal` y `Red Central` (secundarios).
- **Casos de uso** (nombres de los PNG): `Autenticar Usuario`, `Consultar Saldo`, `Extraer Efectivo`, `Realizar Transferencia`, `Emitir Ticket y Devolver Tarjeta`.
- **Asociaciones con actores secundarios** (de los PNG): Autenticar → Sucursal y Red Central; Consultar, Extraer y Transferir → Red Central; Emitir Ticket → ninguno.

## Reglas
1. **Frontera del sistema**: rectángulo `Cajero Automático` con los 5 CU adentro; los actores afuera.
2. **Actores secundarios a la derecha** del sistema; el primario a la izquierda.
3. **Dirección de las asociaciones** (Debrauwer cap. 4): el actor primario inicia, así que su línea va del actor al CU. La del secundario «posee, necesariamente, un sentido que va desde el caso de uso hacia el actor»: `CU --> Actor`.
4. **`«include»`**: obligatorio y sistemático. Flecha discontinua del CU base hacia el incluido. Consultar, Extraer y Transferir lo incluyen a Autenticar Usuario (su precondición es «Autenticar Usuario completado»).
5. **`«extend»`**: opcional, en un punto de extensión previsto. Flecha discontinua del CU que extiende hacia el base. Emitir Ticket extiende a las tres operaciones (el cliente decide si quiere comprobante). Anotar el punto de extensión con una nota.
6. Nombres en infinitivo + objeto, iguales a los de los PNG. Solo los 5 CU: los pasos internos no son casos de uso.

## Sintaxis
```
@startuml 03_casos_de_uso
left to right direction
actor "Cliente" as CLI
actor "Sucursal" as SUC
actor "Red Central" as RED
rectangle "Cajero Automático" {
  usecase "Autenticar Usuario" as CU1
}
CLI --> CU1
CU1 --> SUC
CU2 ..> CU1 : <<include>>
CU5 ..> CU2 : <<extend>>
@enduml
```
El script exige que `SUC` y `RED` no inicien ninguna asociación (`SUC -->` es error).

## Errores frecuentes
- `include` y `extend` con la flecha invertida o intercambiados.
- Línea sin flecha hacia el actor secundario.
- Actores secundarios a la izquierda del sistema.
- CU inventados (por ejemplo «Ingresar PIN») que son pasos de otro CU.

## Verificación
`python3 scripts/verificar_diagramas.py` (5 CU, 3 actores, include/extend, asociaciones de secundarios). Mirar el PNG para confirmar que Sucursal y Red Central quedaron a la derecha.

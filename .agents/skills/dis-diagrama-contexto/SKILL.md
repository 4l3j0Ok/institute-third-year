---
name: dis-diagrama-contexto
description: Crear, revisar o corregir el Diagrama de Contexto (DFD nivel 0 de una sola burbuja) del Cajero Automático en diagrams/01_contexto.puml. Usar cuando se pida el diagrama de contexto, los terminadores Cliente/Sucursal/Red Central, los flujos de entrada y salida del sistema, o al verificar el balanceo entre Contexto y DFD. Aplica notación Yourdon con Graphviz dentro de PlantUML.
---

# Diagrama de Contexto

Ítem 1 de la consigna. Lee primero `dis-convenciones`.

## Fuentes
- `resources/DiagramaDeContexto_CA.png`: tabla oficial con los flujos. **Usar esos nombres exactos.**
- `resources/tasks/entrega_tp_grupal.txt` ítem 1: 3 terminadores, todos los flujos con nombre.
- Guía U2 §2.3: una sola burbuja, sin almacenes ni procesos internos, define la frontera (Pressman cap. 7).
- `DFD.pdf`: terminadores externos, no analizables; sus relaciones entre sí no se muestran.

## Reglas
1. **Una sola burbuja** numerada `0`, con el nombre del sistema (`Cajero Automático`).
2. **Tres terminadores** (rectángulos): Cliente, Sucursal, Red Central. Sin almacenes ni procesos.
3. **Un flujo por flecha**, cada uno con su nombre, en singular y como sustantivo. No agrupar varios datos separados por comas en una flecha.
4. Los nombres son los de la tabla oficial. Flujos (24):
   - Cliente → sistema: `Datos de tarjeta`, `PIN`, `Opción elegida`, `Monto`, `CBU destino`, `Confirmación`.
   - Sistema → Cliente: `Solicitud de PIN`, `Menú de operaciones`, `Saldo consultado`, `Efectivo dispensado`, `Comprobante transferencia`, `Ticket impreso`, `Tarjeta devuelta`.
   - Sucursal → sistema: `Estado de tarjeta`. Sistema → Sucursal: `Solicitud de validación de tarjeta`.
   - Red Central → sistema: `Confirmación de PIN`, `Saldo disponible`, `Autorización de débito`, `Confirmación de transferencia`, `Número de comprobante`.
   - Sistema → Red Central: `Solicitud de validación de PIN`, `Solicitud de consulta de saldo`, `Instrucción de débito`, `Instrucción de transferencia`.
5. Nada de casos excepcionales ni flujos de control (Herramientas DFD, reglas 2 y 3): por eso el bloqueo de tarjeta no es un flujo del Contexto.
6. Cada flujo del Contexto debe reaparecer en el DFD Nivel 0 con el mismo nombre (balanceo).

## Sintaxis (PlantUML + Graphviz)
El script de verificación reconoce la forma de los nodos: `circle` = proceso, `box` = terminador.

```
@startdot 01_contexto
digraph contexto {
  rankdir=LR; nodesep=0.5; ranksep=2.4;
  node [fontname="Helvetica", fontsize=11];
  edge [fontname="Helvetica", fontsize=10];

  CLI [shape=box, label="Cliente"];
  SUC [shape=box, label="Sucursal"];
  RED [shape=box, label="Red Central"];
  SYS [shape=circle, width=1.8, label="0\nCajero\nAutomático"];

  CLI -> SYS [label="Datos de tarjeta"];
  SYS -> SUC [label="Solicitud de validación de tarjeta"];
}
@enddot
```
Los identificadores `CLI`, `SUC`, `RED` los usa el script y el DFD: no cambiarlos.

## Errores frecuentes
- Flecha sin etiqueta o con varios datos en una sola.
- Agregar un almacén o un proceso interno.
- Dibujar flujos entre terminadores.
- Renombrar un flujo en el Contexto y olvidarse del DFD y del diccionario.

## Verificación
`python3 scripts/verificar_diagramas.py` (burbuja única, 3 terminadores, flujos con nombre, balanceo) y revisar el PNG: ningún texto tapado.

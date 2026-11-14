---
name: dis-dfd-nivel0
description: Crear, revisar o corregir el DFD Nivel 0 (Figura 0) del Cajero Automático en diagrams/02_dfd_nivel0.puml, con sus 8 procesos, almacenes D1-D3 y balanceo con el Diagrama de Contexto. Usar cuando se hable de DFD, diagrama de flujo de datos, procesos, almacenes, flujos, balanceo, explosión del contexto o Yourdon. Aplica las reglas de DFD.pdf y Herramientas de análisis y diseño cap. IV.
---

# DFD Nivel 0

Ítem 2 de la consigna. Lee primero `dis-convenciones` y haz antes el Contexto (`dis-diagrama-contexto`).

## Fuentes
- `DFD.pdf`, `Herramientas_de_analisis_Y_diseño_DFD.pdf` (reglas 1-7 de trazado y 1-9 de explosión), Guía U2 §2.2-2.3, consigna ítem 2.

## Estructura (inferida; la sección 6.2 no está en Classroom)
- **8 procesos**: `1 Leer tarjeta`, `2 Validar tarjeta`, `3 Validar PIN`, `4 Consultar saldo`, `5 Extraer efectivo`, `6 Realizar transferencia`, `7 Registrar transacción`, `8 Emitir ticket y devolver tarjeta`.
- **3 almacenes**: `D1 Sesiones`, `D2 Tarjetas leídas`, `D3 Transacciones`.
- Terminadores y ids: `CLI` Cliente, `SUC` Sucursal, `RED` Red Central.

## Reglas
1. **Proceso**: verbo en infinitivo + objeto, numerado, sin abreviaturas. Cada uno con al menos una entrada y una salida (máximo 8 procesos).
2. **Flujo**: sustantivo en singular, siempre con nombre. Los de terminadores usan los nombres de la tabla oficial (ver `dis-diagrama-contexto`).
3. **Almacén**: sustantivo plural con código `Dn`. Se conecta solo con procesos: nunca con terminadores ni con otro almacén.
4. **Balanceo vertical**: los 24 flujos externos del Contexto aparecen en el Nivel 0, sin sobrar ni faltar, con el mismo nombre y sentido. Un flujo que entra a varios procesos (por ejemplo `Opción elegida`) se repite como flecha por proceso.
5. **Los datos que salen deben haber entrado** (Herramientas DFD, regla 7): si un proceso envía `Número de tarjeta` a Red Central, tiene que recibirlo. Por eso `4`, `5` y `6` leen `Datos de sesión` de `D1`.
6. **Los datos que salen de un almacén deben haber entrado** (regla 6): cada almacén tiene escrituras y lecturas.
7. Ignorar inicialización, terminación, casos excepcionales y flujos de control (regla 1-3). El bloqueo de tarjeta no se dibuja.
8. Todo flujo y todo almacén debe figurar en `diagrams/DICCIONARIO_DE_DATOS.md` (`dis-diccionario-datos`).

## Sintaxis
Mismos ids y formas que el Contexto: `circle` = proceso, `box` = terminador, `none` con tabla HTML = almacén (la tabla con `SIDES="TB"` dibuja las dos líneas paralelas de Yourdon).

```
P3 [shape=circle, width=1.3, label="3\nValidar\nPIN"];
D1 [shape=none, margin=0, label=<<TABLE BORDER="0" CELLBORDER="1" CELLSPACING="0" CELLPADDING="6">
    <TR><TD SIDES="TB">D1</TD><TD SIDES="TB">Sesiones</TD></TR></TABLE>>];
CLI -> P3 [label="PIN"];
P3 -> D1  [label="Sesión autenticada"];
D1 -> P4  [label="Datos de sesión"];
```
Cada arista va en **una línea** con `label="..."` (el script las lee por línea). Usar `\n` solo para partir etiquetas largas.

## Errores frecuentes
- Etiquetas compuestas («Opción elegida, monto»): rompen el balanceo y el diccionario.
- Flecha terminador ↔ almacén o almacén ↔ almacén.
- Proceso «sumidero» (solo entradas) o «generador» (solo salidas).
- Renombrar un flujo en un solo diagrama.

## Verificación
`python3 scripts/verificar_diagramas.py` comprueba 8 procesos, D1-D3, entradas/salidas, balanceo y que todo esté en el diccionario. Luego mirar el PNG: sin flechas ilegibles.

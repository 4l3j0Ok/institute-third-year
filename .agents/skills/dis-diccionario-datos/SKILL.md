---
name: dis-diccionario-datos
description: Crear, completar o verificar el Diccionario de Datos (DD) del Cajero Automático en diagrams/DICCIONARIO_DE_DATOS.md, con nombre, descripción, composición y valores permitidos de cada flujo, almacén y elemento de datos, usando la notación = + () {} [ | ] de DD.pdf. Usar cuando se hable de diccionario de datos, DD, composición de flujos, valores permitidos o al verificar que el DFD esté completamente definido.
---

# Diccionario de Datos

Ítem 3 de la consigna. Lee primero `dis-convenciones`; se construye después del DFD.

## Fuentes
- `DD.pdf`: notación y checklist de corrección. Guía U2 §2.4 (ejemplo 2.3). Consigna ítem 3: nombre, descripción, composición y valores permitidos.

## Notación (DD.pdf)
| Símbolo | Significado |
|---|---|
| `=` | está compuesto de |
| `+` | y |
| `( )` | optativo |
| `{ }` | iteración (cero o más); `1{x}10` acota (de 1 a 10) |
| `[ a \| b ]` | selección de una alternativa |
| `* texto *` | comentario (unidades, rango) |
| `@` | identificador |

Ejemplos: `Datos de tarjeta = Número de tarjeta + Vencimiento + Entidad emisora`; `PIN = 4{Dígito}4`; `Estado de tarjeta = [ Válida | Bloqueada | Vencida ]`.

## Formato del archivo
Tabla Markdown con **4 columnas**: `Nombre | Descripción | Composición | Valores permitidos`. Tres secciones: `Flujos de datos`, `Almacenes` (nombre en negrita con su código `Dn`) y `Elementos de datos`. Las filas son una por nombre. Un elemento primitivo se compone de sí mismo: `Dígito = [0-9]`.

## Reglas (checklist de DD.pdf)
1. Cada flujo del Contexto y del DFD está definido, con el mismo nombre. Cada almacén también.
2. Cada componente que aparece en una composición está definido en alguna fila (hasta llegar a primitivos).
3. Ningún dato se define dos veces.
4. La notación es la correcta (`=`, `+`, `()`, `{}`, `[ | ]`).
5. No hay datos en el diccionario que no se usen en el DFD ni en las composiciones.
6. Los valores permitidos son concretos y verificables con el código: `Número de tarjeta = 16{Dígito}16`, `CBU = 22{Dígito}22`, `PIN = 4{Dígito}4`, límite diario de extracción `$100.000`. Si el código cambia una regla, cambiar el diccionario.
7. Mismos nombres que el DFD y que los atributos de las clases: `Número de comprobante` ⇔ `_numeroComprobante`.
8. Sin abreviaturas.

## Errores frecuentes
- Describir el flujo pero dejar sin definir sus componentes.
- Valores permitidos vagos («texto», «número»).
- Incluir datos internos del código que el DFD no muestra.
- Olvidar los flujos internos entre procesos y almacenes (`Datos de sesión`, `Transacción registrada`).

## Verificación
`python3 scripts/verificar_diagramas.py` cruza el diccionario con los flujos y almacenes del Contexto y del Nivel 0, detecta duplicados, filas incompletas y componentes sin definir (aviso).

---
name: dis-convenciones
description: Convenciones obligatorias para construir diagramas (Contexto, DFD, Diccionario de Datos, Casos de Uso, Clases, Objetos, Secuencia, Estado, Actividad) y su código C# en el TP del Cajero Automático de Diseño e Implementación de Sistemas (ISFT N° 93). Usar SIEMPRE antes de crear, revisar o refactorizar cualquier diagrama .puml, el diccionario de datos o el código de src/ del proyecto diseno-e-implementacion-de-sistemas, y cuando se pregunte qué fuente, nombre, notación o regla de nomenclatura C# corresponde. Cada diagrama tiene su propia skill dis-*; esta es la base común.
---

# Convenciones del TP Cajero Automático (DIS 2026)

Proyecto: `diseno-e-implementacion-de-sistemas/`. Los diagramas viven en `diagrams/*.puml` (PlantUML) y el código en `src/Atm/` (C# + Windows Forms, 3 capas: `Data`, `Logic`, `Presentation`).

## 1. Fuentes y su jerarquía

Ante un conflicto manda la fuente de mayor jerarquía. Detalle y citas en `references/fuentes.md`.

1. **Consigna del docente**: `resources/tasks/entrega_tp_grupal.txt`, `resources/CU_*.png` (los 5 casos de uso), `resources/DiagramaDeContexto_CA.png` (flujos del Contexto), `resources/ejemploDiagramaActividad.rar` (HTML con el diagrama de actividad modelo).
2. **Guías de la cátedra**: `Guia_DiagramaActividad_DIS_2026.pdf`, `Guia_TeoricoPractica_U2_U3_U4_DIS_2026.pdf`, `DFD.pdf`, `DD.pdf`, `Herramientas_de_analisis_Y_diseño_DFD.pdf`.
3. **Bibliografía**: Debrauwer (UML 2.5), EIDOS/Alarcón. Para extraer texto usar `pdftotext -layout`. Yourdon, Larman y «UML 24hs» son PDF escaneados: no tienen texto extraíble.

La consigna cita secciones 3, 5, 6.1 y 6.2 de un documento que no está en Classroom. Lo que no figura en las fuentes (8 procesos, 3 almacenes, 8 clases) es una **inferencia**: registrarla en `TASKS.md` y no presentarla como dato del docente.

## 2. Orden de construcción y trazabilidad

`Contexto → DFD Nivel 0 → Diccionario de Datos → Casos de Uso → Clases → Objetos → Secuencia → Estado → Actividad → Código`

Cada diagrama toma sus nombres del anterior y no los renombra. Un nombre se define una sola vez y se reutiliza igual:

| Concepto | Se define en | Se reutiliza en |
|---|---|---|
| Terminadores | Contexto | DFD, Casos de Uso (actores) |
| Flujos de datos | Contexto / DFD | Diccionario de Datos |
| Casos de uso | `resources/CU_*.png` | Casos de Uso, Secuencia, Actividad |
| Clases, atributos, métodos | Clases | Objetos, Secuencia, Estado, código |
| Estados de `Session` | Estado | enum `SessionState` |

Los diagramas y el código se mantienen sincronizados: un cambio en uno exige revisar el otro.

## 2b. Idioma: identificadores en inglés, dominio en español

- **Todo identificador está en inglés**: namespaces, clases, enums y sus miembros, métodos, propiedades, campos, parámetros, variables locales, constantes, controles de Windows Forms y pruebas. Lo mismo en los diagramas que los dibujan (Clases, Objetos, Secuencia, Estado).
- **Queda en español**: los textos que ve el usuario (pantalla, errores, ticket), los comentarios sobre reglas del dominio argentino y el vocabulario de análisis que fija el docente (nombres de flujos, procesos, almacenes, casos de uso, actores y el diccionario de datos).
- Los nombres que impone la consigna (`FormCajero`, `Sesion`, `Operacion`, `PantallaCajero`…) se traducen según `references/glosario.md`. Toda clase del diagrama cuyo nombre difiera de la consigna lleva en el diagrama una nota con la equivalencia, para que el docente pueda mapearla.
- Términos sin traducción útil se conservan como siglas: `Cbu`, `DB`, `Pin`.
- Inglés simple y descriptivo; ningún identificador mezcla idiomas (`CardStatus`, no `EstadoCard`).

## 3. Reglas comunes de todos los diagramas

- Texto en español, con tildes. Sin abreviaturas (`Número`, no `Nro`).
- Archivo `diagrams/NN_nombre_en_snake_case.puml`; `@startuml`/`@startdot` con el mismo nombre que el archivo; `title` descriptivo.
- Un diagrama debe caber en una página y ser legible (DFD.pdf). Máximo 8 procesos por nivel.
- Modelar el **flujo normal**; los casos excepcionales no se dibujan en el DFD (Herramientas DFD, regla 2). En UML sí se modelan las alternativas relevantes del negocio.
- Toda condición de guarda va entre corchetes `[condición]` y las guardas de una bifurcación son excluyentes y exhaustivas (Guía de Actividad §3.4).
- Cada elemento debe poder justificarse con una fuente. Si no, es una suposición: anotarla.

## 4. Nomenclatura de elementos de los diagramas

| Elemento | Regla | Ejemplo |
|---|---|---|
| Proceso (DFD) | Verbo en infinitivo + objeto, numerado | `3 Validar PIN` |
| Flujo de datos | Sustantivo, en singular, con nombre; el mismo nombre en todos los niveles | `Autorización de débito` |
| Almacén | `Dn` + sustantivo plural | `D1 Sesiones` |
| Terminador / actor | Nombre propio o sustantivo | `Red Central` |
| Caso de uso | Infinitivo + objeto, tal como en `CU_*.png` | `Extraer Efectivo` |
| Clase | Singular, PascalCase, inglés | `Transfer` |
| Atributo | `- _camelCase : tipoCSharp` | `- _availableBalance : decimal` |
| Método | `+ PascalCase(param : tipo) : retorno` | `+ Execute() : bool` |
| Objeto | `nombre : Clase`, camelCase, subrayado | `session1 : Session` |
| Participante de secuencia | `:Clase` subrayado | `:CentralDB` |
| Mensaje | `PascalCase(args)` sincrónico; retorno discontinuo en itálica | `AuthorizeDebit(cardNumber, amount)` |
| Estado | PascalCase, gerundio o participio en inglés; la descripción en español de la consigna va como texto del estado | `Validating` (Validando) |
| Evento de transición | `Evento() [guarda] / acción` | `Authenticate(pin) [intentos = 3] / BlockCard` |
| Actividad | Verbo en infinitivo + objeto específico | `Validar tarjeta` |

## 5. Convenciones de C#

Fuente: Microsoft «Convenciones de nomenclatura» y «Convenciones de código de C#», más la guía U4 de la cátedra.

| Elemento | Estilo | Ejemplo |
|---|---|---|
| Namespace, clase, record, enum, método, propiedad, miembro de enum, constante | PascalCase | `Session`, `Execute`, `DailyLimit` |
| Interfaz | `I` + PascalCase | `IOperation` |
| Parámetro y variable local | camelCase | `cardNumber` |
| Campo privado/protegido de instancia | `_camelCase` (la consigna exige el guión bajo) | `_balance` |
| Campo estático privado | `s_camelCase` | `s_random` |
| Controles WinForms | prefijo de 3 letras + PascalCase, los declara el diseñador | `btnAccept`, `lblMessage` |
| Manejador de evento | `On` + control + evento, PascalCase | `OnDigitClick` |
| Booleano | `Esta…`, `Es…`, `Tiene…` | `EstaVigente()` |

Además:
- Siglas de 2 letras en mayúsculas (`DB`); de 3 o más como palabra (`Cbu`, `Pin`, `Dni`).
- Nada de abreviaturas (`ReferenceNumber`, no `RefNum`) ni tildes o eñes en identificadores.
- Un tipo por archivo; el archivo se llama como el tipo.
- Consigna ítem 10: **campos privados `_x`, propiedades públicas con get/set, constructores explícitos** y métodos. `Operation` es abstracta y las concretas hacen `override` de `Execute()`.
- Capas: Presentación → Lógica → Datos, nunca al revés. La Presentación no contiene reglas de negocio.
- Se fuerza con `src/.editorconfig` (`dotnet build` marca las violaciones de nombres como error).

### UML → C#

| UML | C# |
|---|---|
| `+` / `-` / `#` | `public` / `private` / `protected` |
| Atributo `- _x : T` | campo `private T _x;` + propiedad `public T X { get => _x; set => _x = value; }` |
| `{readOnly}` | propiedad solo con `get` |
| Clase abstracta, método `{abstract}` | `abstract class`, `public abstract T Metodo();` |
| Herencia `<\|--` | `class Hija : Padre`, `override` |
| Composición `*--` con multiplicidad `0..*` | campo `List<T>` expuesto como `IReadOnlyList<T>` |
| Asociación `0..1` / `1` | `T?` / `T` |
| Tipos | `string`, `bool`, `int`, `decimal` (dinero), `DateTime` |

## 6. Flujo de trabajo para cada diagrama

1. Leer la skill `dis-*` del diagrama y las fuentes que cita.
2. Escribir o editar `diagrams/NN_*.puml` (el diagrama canónico del repositorio es el ejemplo).
3. Renderizar: `scripts/render_diagrams.sh` (usa `plantuml` o el jar de la extensión de VS Code; Contexto y DFD requieren Graphviz `dot`).
4. Verificar reglas automáticas: `python3 scripts/verificar_diagramas.py`. Revisa cada diagrama contra sus reglas, el balanceo Contexto ↔ DFD ↔ Diccionario, y que clases, atributos, métodos, estados y mensajes de los diagramas existan en `src/Atm/` (y que no haya clases de más en el código). Cierra con código 1 si falla.
   Para el código: `dotnet build Atm.slnx && dotnet test Atm.slnx` en `src/`; el build falla si se rompe una convención de nombres.
5. **Mirar el PNG** con la herramienta de lectura de imágenes: el script no detecta solapamientos, flechas cruzadas ni lo que quede ilegible.
6. Actualizar `DIAGRAMS.md`, `TASKS.md` y, si cambia un nombre, el código y las pruebas.

## 7. Limitaciones de PlantUML y cómo se resuelven

| Necesidad | Solución |
|---|---|
| Notación Yourdon (burbuja, almacén de dos líneas paralelas) | `@startdot` con Graphviz dentro del `.puml` (skills de Contexto y DFD) |
| Objetos subrayados en diagrama de clases | PlantUML no lo hace: usar un **diagrama de objetos** aparte, con `object "<u>nombre : Clase</u>"` |
| Participantes de secuencia subrayados | `participant "<u>:Clase</u>" as X` |
| Rombos de fusión sin texto en actividad | PlantUML los dibuja solo en cada `endif`; son válidos («fusión de alternativa», Debrauwer cap. 9). Dejar una leyenda |
| El docente recomienda StarUML | Los `.puml` sirven de guía para recrearlos; confirmar con el docente si se acepta PlantUML (punto abierto en `CONSIGNA.md`) |

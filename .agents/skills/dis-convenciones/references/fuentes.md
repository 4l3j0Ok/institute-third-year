# Fuentes del TP y qué aporta cada una

Todas las rutas son relativas a `diseno-e-implementacion-de-sistemas/resources/`. Texto extraído con `pdftotext -layout`.

## Consigna y material del docente

| Archivo | Qué aporta |
|---|---|
| `tasks/entrega_tp_grupal.txt` | Los 11 ítems del TP: Contexto, DFD Nivel 0 (8 procesos, D1-D3, balanceo), DD, Casos de Uso (5 CU, 3 actores, include/extend, secundarios a la derecha), Clases (8 clases, tipo + visibilidad, multiplicidad, **objetos subrayados**), 2 Secuencias (4 participantes, sincrónicos con parámetros, retornos discontinuos, focos), Estado de `Sesion` (6 estados), Actividad del sistema, solución en capas, README |
| `DiagramaDeContexto_CA.png` | Tabla oficial de flujos de entrada y salida por terminador (24 flujos). **Son los nombres exactos a usar** |
| `CU_autenticarUsuario.png`, `CU_ConsultarSaldo.png`, `CU_extraerefectivo.png`, `CU_realizarTransferencia.png`, `CU_emitirTicketYDevolverTarjeta.png` | Actor primario, secundarios, precondición, disparador y flujo principal de cada CU. Base de Secuencia y Actividad |
| `ejemploDiagramaActividad.rar` (contiene `ejemploDiagramaActividad.html`) | Actividad modelo del CU «Operar Cajero»: calles Cliente / Cajero / Sistemas Externos, pregunta dentro del rombo y guarda `[sí]`, `[no, <3 int.]`, `[3° intento]`, leyenda. Se extrae con `bsdtar -xf` |
| `classwork.txt`, `stream.txt`, `manifest.json` | Material crudo de Classroom; sin valor normativo |

## Guías de la cátedra

| Archivo | Secciones relevantes |
|---|---|
| `Guia_DiagramaActividad_DIS_2026.pdf` | §3 notación (nodo inicial único, finales ≥ 1, actividad = verbo infinitivo, guardas exhaustivas y excluyentes, fork/join, calles), §8 errores frecuentes, §9 checklist |
| `Guia_TeoricoPractica_U2_U3_U4_DIS_2026.pdf` | §2.2 componentes del DFD (proceso verbo+objeto, flujo sustantivo, almacén plural), §2.3 balanceo, §2.4 DD, §3.2 CU, §3.3 clases (visibilidad, multiplicidad, relaciones), §3.4 secuencia (nombres subrayados, retorno en itálica), §4 UML → C# y capas |
| `Guia_Teorica_U1_U5_DIS_2026.pdf` | Ciclo de vida, calidad y Git. No define notación de diagramas |
| `DFD.pdf` | Componentes, nombres (verbo + objeto, sin abreviaturas, flujo en singular), burbujas con entrada y salida, 2-3 / 3-6 / 6-8 burbujas por sistema simple / medio / grande, etiqueta de flujo contra almacén |
| `DD.pdf` | Notación del DD: `=` se define como, `+` y, `()` opcional, `{}` iteración, `[ \| ]` selección, `* *` comentario, `@` identificador; `1{x}10` iteración acotada; checklist de corrección |
| `Herramientas_de_analisis_Y_diseño_DFD.pdf` (cap. IV, pp. 52-74) | Pasos y 7 reglas para trazar DFD (ignorar inicialización, terminación, casos excepcionales y flujos de control; datos que salen de un almacén o proceso deben haber entrado), reglas de explosión (≤ 8 procesos, numeración, balanceo vertical y horizontal, mismo nombre de flujo, almacenes visibles si los comparten 2+ burbujas) |
| `2025-A-SIS-3-DISE-OeIMPLEMENTACIONdeSISTEMAS-TOLEDO.pdf` | Programa 2025: la Unidad 3 incluye Diagrama de **Objetos**. Bibliografía: Schmuller «UML en 24 horas», Jacobson/Booch/Rumbaugh |
| `Procesos_Y_Ciclos_de_Vida.pdf` | Unidad 1; sin notación de diagramas |

## Bibliografía

| Archivo | Capítulos útiles (texto extraíble) |
|---|---|
| `uml-25-iniciacion-ejemplos-y-ejercicios-corregidos.pdf` (Debrauwer y Van der Heyde) | Cap. 4 casos de uso (actores primarios y secundarios, asociación del secundario **del CU hacia el actor**, `«include»` sistemática, `«extend»` opcional con puntos de extensión, frontera del sistema). Cap. 5 secuencia (línea de vida `función: Clase`, sincrónico/asincrónico, retorno no sistemático, autoenvío, escenario sin alternativas, fragmentos `alt`/`opt`/`loop`). Cap. 6 clases (singular, `+ - # ~`, tipos, cardinalidad `[inf..sup]`, firma de método, asociaciones con nombre `<`/`>` y cardinalidad en ambos extremos —sin indicar vale 1—, navegación con flecha, composición fuerte, **instancias subrayadas** `nombreInstancia : nombreClase`). Cap. 8 estados (estado inicial único, finales, evento + `[guarda]` + actividad, transición reflexiva). Cap. 9 actividades (alternativa con guardas excluyentes y **fusión de alternativa** como rombo, horquilla y reunión, calles, «conviene anotar dos actividades finales») |
| `eidos uml - diseño orientado a objetos con uml spanish.pdf` (Alarcón) | Cap. 6 pp. 72-78 actividades (bifurcación = rombo con guardas exhaustivas, `else`, calles); Cap. 3 pp. 28-45 clases |
| `ingenieria-de-software-enfoque-practico-7ed-pressman.pdf` | Cap. 7 modelado de análisis: diagrama de contexto. Texto muy grande, buscar con `grep` |
| `analisis Estructurado Moderno - Yourdon.pdf`, `Larman Craig - Uml Y Patrones.PDF`, `UML 24hs.pdf` | **Escaneados, sin texto.** Para consultarlos hay que renderizar páginas a imagen (`pdftoppm -png -f N -l N`) |

## Hallazgos que condicionan el diseño

- Los flujos del Contexto salen de `DiagramaDeContexto_CA.png`; el Nivel 0 debe contenerlos todos (balanceo) con **los mismos nombres**.
- Un proceso que emite un dato debe recibirlo antes (Herramientas DFD, regla 7): por eso los procesos que mandan el número de tarjeta a Red Central leen `Datos de sesión` de `D1`.
- Los casos excepcionales no van en el DFD. Por eso el bloqueo de tarjeta tras 3 PIN fallidos no tiene flujo propio hacia Sucursal en el Contexto: la tabla oficial no lo incluye.
- El diccionario debe definir todos los flujos y almacenes del DFD y cada componente usado en una composición (`DD.pdf`, checklist de corrección).
- El docente modela la actividad con calles, pregunta en el rombo y guardas entre corchetes; la guía exige guardas exhaustivas y excluyentes.

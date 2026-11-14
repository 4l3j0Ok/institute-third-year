# DIAGRAMS

Fuentes `.puml` en `./diagrams`. Regenerar los PNG: `scripts/render_diagrams.sh` (usa `plantuml` o el jar de la extensión de VS Code; Contexto y DFD necesitan Graphviz). Verificar reglas: `python3 scripts/verificar_diagramas.py`.

Las reglas y fuentes de cada diagrama están en las skills `.agents/skills/dis-*`; la base común es `dis-convenciones`.

| # | Diagrama | Fuente | Imagen | Skill |
|---|---|---|---|---|
| 1 | Diagrama de Contexto | [01_contexto.puml](diagrams/01_contexto.puml) | [png](diagrams/01_contexto.png) | `dis-diagrama-contexto` |
| 2 | DFD Nivel 0 | [02_dfd_nivel0.puml](diagrams/02_dfd_nivel0.puml) | [png](diagrams/02_dfd_nivel0.png) | `dis-dfd-nivel0` |
| 3 | Diccionario de Datos | [DICCIONARIO_DE_DATOS.md](diagrams/DICCIONARIO_DE_DATOS.md) | - | `dis-diccionario-datos` |
| 4 | Casos de Uso | [03_casos_de_uso.puml](diagrams/03_casos_de_uso.puml) | [png](diagrams/03_casos_de_uso.png) | `dis-casos-de-uso` |
| 5 | Clases | [04_clases.puml](diagrams/04_clases.puml) | [png](diagrams/04_clases.png) | `dis-diagrama-clases` |
| 5b | Objetos (nombres subrayados) | [09_objetos.puml](diagrams/09_objetos.puml) | [png](diagrams/09_objetos.png) | `dis-diagrama-objetos` |
| 6 | Secuencia: Autenticar Usuario | [05_secuencia_autenticar.puml](diagrams/05_secuencia_autenticar.puml) | [png](diagrams/05_secuencia_autenticar.png) | `dis-diagrama-secuencia` |
| 7 | Secuencia: Extraer Efectivo | [06_secuencia_extraccion.puml](diagrams/06_secuencia_extraccion.puml) | [png](diagrams/06_secuencia_extraccion.png) | `dis-diagrama-secuencia` |
| 8 | Estado: Sesion | [07_estado_sesion.puml](diagrams/07_estado_sesion.puml) | [png](diagrams/07_estado_sesion.png) | `dis-diagrama-estado` |
| 9 | Actividad: Sistema completo | [08_actividad_sistema.puml](diagrams/08_actividad_sistema.puml) | [png](diagrams/08_actividad_sistema.png) | `dis-diagrama-actividad` |

Notas:
- Clases, Objetos, Secuencias y Estado usan los identificadores en inglés del código (`Session`, `CajeroForm`, `Execute()`); cada diagrama lleva la equivalencia con los nombres de la consigna. Contexto, DFD, Casos de Uso, Diccionario y Actividad usan el vocabulario de análisis en español.
- Contexto y DFD usan notación Yourdon (burbujas, almacenes de dos líneas paralelas) con Graphviz embebido en PlantUML (`@startdot`). En el DFD los terminadores repetidos llevan una línea extra.
- Los rombos sin texto del diagrama de actividad son fusiones de alternativa (válidas, Debrauwer cap. 9); el diagrama lo aclara en una leyenda.
- PlantUML no valida UML ni subraya objetos en un diagrama de clases: por eso existe el diagrama de objetos. Si el docente exige StarUML, los `.puml` sirven de guía.

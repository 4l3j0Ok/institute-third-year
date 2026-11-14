# Consigna formal: TP Integrador Grupal - Cajero Automático

**Materia:** Diseño e Implementación de Sistemas (3° año, 2026) - ISFT N° 93, San Vicente  
**Docente:** Mariano Spala  
**Tecnicatura:** Análisis, Desarrollo y Programación de Aplicaciones  
**Origen:** tarea «Entrega de TP grupal DIS2026» (Classroom, publicada el 2/11) y anuncios del 1/9, 16/9, 7/10 y 14/10. Texto original en `resources/tasks/entrega_tp_grupal.txt`.

---

## 1. Objetivo

Modelar y construir el sistema **Cajero Automático**, representando todas sus funcionalidades. El trabajo se compone de:

1. **Documentación y diagramas** de análisis y diseño (DFD, UML).
2. **Código fuente** de una solución en tres capas (Datos, Lógica, Presentación).
3. **README.md** con descripción, integrantes e instrucciones de ejecución.

## 2. Modalidad

| Aspecto                      | Condición                                                                                                                           |
| ---------------------------- | ----------------------------------------------------------------------------------------------------------------------------------- |
| Grupo                        | Hasta 4 personas                                                                                                                    |
| Entrega                      | 1 trabajo por grupo; un representante lo entrega por Classroom                                                                      |
| Fecha de entrega             | **No informada en Classroom** (un compañero la consultó el 16/9 sin respuesta visible). Consultar al docente                        |
| Herramientas de diagramación | Libre (cualquier herramienta CASE). El docente recomienda StarUML y advierte que no todas validan las reglas de UML                 |
| Lenguaje de la solución      | C# sobre .NET con Windows Forms, con `FormCajero` como ventana principal, tal como pide la consigna |
| Evaluación previa            | Examen multiple choice (U1 y U5) y examen de la cursada (ver tablón). Este TP es el trabajo de fin de cursada                       |

## 3. Descripción del sistema

Un cliente inserta su tarjeta en el cajero, se autentica con un PIN y realiza operaciones contra sistemas externos. Al terminar puede pedir un comprobante y recupera su tarjeta.

### 3.1 Actores

| Actor       | Tipo       | Rol                                                                      |
| ----------- | ---------- | ------------------------------------------------------------------------ |
| Cliente     | Primario   | Usa el cajero                                                            |
| Sucursal    | Secundario | Valida el estado de la tarjeta (válida, bloqueada o vencida)             |
| Red Central | Secundario | Valida el PIN, informa saldos, autoriza débitos y ejecuta transferencias |

### 3.2 Casos de uso (5)

| N°  | Caso de uso                      | Actores secundarios   | Resumen                                                                                             |
| --- | -------------------------------- | --------------------- | --------------------------------------------------------------------------------------------------- |
| 1   | Autenticar Usuario               | Sucursal, Red Central | Lee la tarjeta, la valida en Sucursal, pide el PIN, lo valida en Red Central y muestra el menú      |
| 2   | Consultar Saldo                  | Red Central           | Muestra saldo disponible, saldo total y fecha/hora; registra en el log                              |
| 3   | Extraer Efectivo                 | Red Central           | Pide monto, autoriza el débito (saldo y límite diario), dispensa, registra y muestra saldo restante |
| 4   | Realizar Transferencia           | Red Central           | Pide CBU y monto, muestra resumen, confirma, ejecuta, muestra comprobante y registra                |
| 5   | Emitir Ticket y Devolver Tarjeta | -                     | Pregunta si desea comprobante, lo imprime, devuelve la tarjeta y cierra la sesión                   |

Las especificaciones completas (precondiciones, disparadores y flujos paso a paso) están en los PNG `resources/CU_*.png`.

### 3.3 Reglas de negocio

- La tarjeta debe estar vigente (ni bloqueada ni vencida).
- El PIN admite un máximo de **3 intentos**; al tercero fallido se bloquea la tarjeta.
- Una extracción no puede superar el saldo disponible ni el límite diario.
- Toda operación completada se registra en el log de la sesión.
- Al cerrar la sesión se eliminan de memoria los datos de autenticación.

## 4. Entregables y subtareas

Cada ítem de la consigna oficial es una subtarea. «Estado» indica el avance del repositorio; los diagramas generados son un **borrador a revisar**.

### Parte A - Análisis estructurado

| #   | Entregable               | Requisitos que debe cumplir                                                                                                                                                                        | Archivo                            | Estado   |
| --- | ------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------- | -------- |
| 1   | **Diagrama de Contexto** | Una sola burbuja; los 3 terminadores (Cliente, Sucursal, Red Central); todos los flujos de entrada y salida; **todos los flujos con nombre**                                                       | `diagrams/01_contexto.puml`        | Borrador |
| 2   | **DFD Nivel 0**          | 8 procesos; 3 almacenes (D1, D2, D3); flujos que los conectan; **balanceo** con el Contexto (cada flujo externo aparece en el Nivel 0); procesos con verbo + objeto, cada uno con entrada y salida | `diagrams/02_dfd_nivel0.puml`      | Borrador |
| 3   | **Diccionario de Datos** | Por cada flujo y almacén: nombre, descripción, composición y valores permitidos                                                                                                                    | `diagrams/DICCIONARIO_DE_DATOS.md` | Borrador |

### Parte B - Modelado UML

| #   | Entregable                                | Requisitos que debe cumplir                                                                                                                                                                                                                              | Archivo                                 | Estado   |
| --- | ----------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------- | -------- |
| 4   | **Diagrama de Casos de Uso**              | 5 casos de uso; 3 actores; relaciones «include» y «extend» bien aplicadas; **actores secundarios a la derecha** del sistema                                                                                                                              | `diagrams/03_casos_de_uso.puml`         | Borrador |
| 5   | **Diagrama de Clases**                    | 8 clases con atributos (tipo + visibilidad) y métodos (retorno + visibilidad); herencia de Consulta, Extraccion y Transferencia desde Operacion; composición y asociación con **multiplicidad en todos los extremos**; nombres de **objetos subrayados** | `diagrams/04_clases.puml` + `diagrams/09_objetos.puml` | Borrador |
| 6   | **Secuencia: Autenticar Usuario**         | 4 participantes (Cliente, :PantallaCajero, :SucursalDB, :CentralDB); mensajes sincrónicos con parámetros; retornos en línea discontinua; focos de control bien ubicados                                                                                  | `diagrams/05_secuencia_autenticar.puml` | Borrador |
| 7   | **Secuencia de UNA operación a elección** | Mismo criterio que el ítem 6 (Consulta, Extracción o Transferencia). Elegida: Extraer Efectivo                                                                                                                                                           | `diagrams/06_secuencia_extraccion.puml` | Borrador |
| 8   | **Diagrama de Estado de la clase Sesion** | Estados Iniciada, Autenticando (tarjeta), Validando (PIN), Operando, Cerrando y Cerrada; transiciones con eventos disparadores                                                                                                                           | `diagrams/07_estado_sesion.puml`        | Borrador |
| 9   | **Diagrama de Actividad del SISTEMA**     | Flujo completo del sistema (no solo «Operar Cajero»); validación de tarjeta, PIN con contador de intentos y selección de operación                                                                                                                       | `diagrams/08_actividad_sistema.puml`    | Borrador |

### Parte C - Solución en capas (ítem 10)

| Capa             | Requisitos                                                                                                                                                                                                                                    | Ubicación                  | Estado |
| ---------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------- | ------ |
| **Datos**        | `SucursalDB` y `CentralDB` con métodos que simulan la comunicación con los sistemas externos (valores fijos o aleatorios)                                                                                                                     | `src/Cajero/Data/`            | Hecho  |
| **Lógica**       | `Operacion` (abstracta); `Consulta`, `Extraccion` y `Transferencia` (concretas, con override de `Ejecutar()`); `Cliente`, `Tarjeta`, `Cuenta` y `Sesion`. Atributos privados con `_`, propiedades públicas (get/set), constructores y métodos | `src/Cajero/Logic/`           | Hecho  |
| **Presentación** | Formulario principal `FormCajero` que simule la autenticación (número de tarjeta, PIN, menú) y **al menos UNA** operación, mostrando el resultado en pantalla. La capa no contiene lógica de negocio                                          | `src/Cajero/Presentation/`    | Hecho  |

Reglas de dependencia entre capas: Presentación → Lógica → Datos. Nunca al revés.

### Parte D - Documentación de entrega (ítem 11)

| Entregable    | Contenido                                                                                 | Archivo     | Estado                      |
| ------------- | ----------------------------------------------------------------------------------------- | ----------- | --------------------------- |
| **README.md** | Descripción del sistema, **integrantes del equipo (hasta 4)** y cómo ejecutar el proyecto | `README.md` | Falta completar integrantes |

## 5. Cómo trabajar (orden sugerido)

1. Confirmar con el docente los puntos abiertos de la sección 7.
2. Revisar el Contexto y el DFD Nivel 0; verificar el balanceo contra el Diccionario de Datos.
3. Revisar Casos de Uso, Clases y Secuencias; ajustar los nombres de métodos al código.
4. Revisar Estado y Actividad contra el comportamiento real de `Sesion`.
5. Si el docente exige una herramienta concreta (StarUML), recrear los diagramas con ella usando los `.puml` como guía.
6. Probar la aplicación a mano con los escenarios de la sección 6.
7. Completar integrantes en el README, comprobar que el proyecto arranca desde cero y entregar.

Los identificadores del código y de los diagramas de diseño están en inglés (equivalencias con los nombres de esta consigna en el README y en `.agents/skills/dis-convenciones/references/glosario.md`; p. ej. `FormCajero` = `CajeroForm`, `Sesion` = `Session`). Confirmar con el docente que acepta nombres en inglés. Los diagramas se regeneran con `scripts/render_diagrams.sh` y se verifican con `python3 scripts/verificar_diagramas.py`. Las reglas y fuentes de cada diagrama están en las skills `.agents/skills/dis-*` (empezar por `dis-convenciones`). El código sigue las convenciones de C# de esa skill y `dotnet build` las exige (`src/.editorconfig`).

## 6. Criterios de verificación (checklist de autoevaluación)

**Diagramas**

- [ ] Todos los flujos del Contexto y del DFD tienen nombre.
- [ ] Cada flujo externo del Contexto aparece en el DFD Nivel 0.
- [ ] Ningún proceso tiene solo entradas o solo salidas.
- [ ] Todo flujo y almacén del DFD figura en el Diccionario de Datos.
- [ ] Los actores secundarios están a la derecha en Casos de Uso.
- [ ] Las clases tienen tipo y visibilidad en atributos y métodos; todas las relaciones tienen multiplicidad en ambos extremos.
- [ ] Las secuencias usan solo mensajes sincrónicos con parámetros y retornos discontinuos.
- [ ] El diagrama de estado y el código de `Sesion` usan los mismos estados.

**Aplicación** (tarjeta de prueba válida, PIN `1234`)

- [ ] Tarjeta válida + PIN correcto → muestra el menú.
- [ ] 3 PIN incorrectos → tarjeta bloqueada y devuelta.
- [ ] Tarjeta bloqueada o vencida → rechazada.
- [ ] Consulta de saldo muestra disponible, total y fecha/hora.
- [ ] Extracción mayor al saldo o al límite diario → error.
- [ ] Transferencia con CBU de menos de 22 dígitos → no avanza; con datos válidos muestra comprobante.
- [ ] Ticket opcional con el detalle de las operaciones; la sesión se cierra y se puede iniciar otra.

**Entrega**

- [ ] README con integrantes y pasos de ejecución.
- [ ] Un solo envío por grupo, hecho por el representante.

## 7. Puntos abiertos (consultar al docente)

1. **Documento con las secciones 3, 5, 6.1 y 6.2.** La consigna remite a él, pero no está en el Classroom. Las 8 clases, los 8 procesos y los 3 almacenes del repositorio son una **inferencia** (ver `TASKS.md`).
2. **Fecha de entrega** y **formato** (archivo comprimido, enlace a repositorio, PDF de diagramas).
3. **Herramienta de diagramación:** si se aceptan diagramas generados con PlantUML.
4. **Ejemplo de Diagrama de Actividad** (`ejemploDiagramaActividad.rar`): el archivo descargado pesa 3.6 KB y no pudo abrirse; revisarlo desde Classroom.
5. **Objetos subrayados** (ítem 5): PlantUML no los dibuja en un diagrama de clases. Se resolvió con un Diagrama de Objetos aparte (`09_objetos.puml`, nombres subrayados) y con participantes subrayados en las secuencias; confirmar con el docente que alcanza.

## 8. Material de apoyo

Listado completo con enlaces en [RESOURCES.md](RESOURCES.md). Los más útiles:

- Guía teórico-práctica U2-U4 (DFD, UML, implementación): `resources/Guia_TeoricoPractica_U2_U3_U4_DIS_2026.pdf`
- Guía de Diagrama de Actividad: `resources/Guia_DiagramaActividad_DIS_2026.pdf`
- Casos de uso y contexto del cajero: `resources/CU_*.png`, `resources/DiagramaDeContexto_CA.png`
- Bibliografía: Yourdon (DFD), Debrauwer (UML 2.5), EIDOS UML, Larman.

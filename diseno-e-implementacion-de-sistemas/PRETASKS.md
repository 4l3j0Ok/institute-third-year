---
status: completed
---

# Problematica

Se solicitó una tarea para una materia llamada Diseño e Implementación de Sistemas relacionada a la creacion de un proyecto con múltiples diagramas UML y recursos tipo documentación relacionados, pero la consigna fue mas que nada hablada en clase y hay información dispersa en classroom del proyecto. Se sabe hasta el momento que el proyecto debe representar un sistema de cajero automático, representando todas las funcionalidades que este posee, pero hace falta un alcance CLARO.
El proyecto debe ser: Diagramas solicitados + código fuente. Puede ser cualquier tipo de aplicación, pero el usuario prefiere aplicación de escritorio con PySide.

# Objetivo

Se deben relevar las tareas a realizar por el usuario planteando una consigna formal y clara para que el usuario pueda realizar el proyecto de manera correcta y sin inconvenientes. Dividir en subtareas de manera organizada para poder aprobar el trabajo práctico, con cada uno de los diagramas UML solicitados y la documentación correspondiente.

Se debe recopilar TODAS las fuentes de información aportadas por el docente en el classroom y almacenarlas en `./resources` para que el usuario pueda acceder a ellas de manera rápida y sencilla. Generar en `RESOURCES.md` un listado de los recursos con su respectivo link y una breve descripción de cada uno.

# Forma de trabajo

El usuario deberá primero generar los diagramas UML y documentación solicitada. Una vez finalizado, se deberá generar el código fuente de la aplicación. Se recomienda utilizar PySide para la creación de la aplicación de escritorio. Tu como agente debes crear la base del proyecto de Python. Tambien en `TASKS.md`, generar listado de diagramas a solicitar y construirlos en `./diagrams` con sus respectivos archivos `.puml`.

Para la generación de diagramas UML, utilizar la herramienta `plantuml` (cli) para generar los diagramas a partir de archivos `.puml` y almacenarlos en `./diagrams`. Generar un archivo `DIAGRAMS.md` con un listado de los diagramas generados y su respectivo link.

# Scrapeo

Propon una manera de conectarse al classroom y obtener toda la información de manera automatizada para que el usuario no tenga que hacerlo manualmente.

Quizás con playwright con el navegador del usuario, o con algún otro método que permita obtener los recursos de manera automatizada.

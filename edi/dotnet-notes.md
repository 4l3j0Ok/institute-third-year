Aquí tienes un archivo `README.md` que explica la evolución de las plantillas de Microsoft y por qué el comando de la guía generó confusión.

-----

# Notas sobre la Evolución de Blazor en .NET 8

Este documento explica los cambios recientes en el ecosistema de desarrollo de .NET que afectan a la creación de proyectos Blazor, específicamente respecto a la transición de .NET 6/7 hacia **.NET 8**.

## 1\. El Cambio de Nomenclatura (Microsoft y las Plantillas)

Históricamente, Microsoft ofrecía plantillas separadas según el modelo de renderizado:

  * **Blazor Server**: Se creaba con `dotnet new blazorserver`.
  * **Blazor WebAssembly**: Se creaba con `dotnet new blazorwasm`.

Con la llegada de **.NET 8**, Microsoft introdujo el **Blazor Web App**, un modelo unificado que permite combinar renderizado en el servidor y en el cliente en un mismo proyecto. Para reflejar esta unión, la plantilla oficial de C\# pasó a llamarse simplemente:

``` bash
dotnet new blazor

```

## 2\. Cómo seguir la Guía del Curso en .NET 8

Para cumplir con los objetivos de la **Unidad 1** (desarrollo de un Gestor de Tareas basado en componentes Razor) utilizando .NET 8, el comando de creación debe actualizarse para forzar el modelo de servidor que requiere la materia:

### Comando Recomendado:

``` bash
dotnet new blazor -o GestorTareas -int Server

```

### Explicación de los parámetros:

  * **`blazor`**: Utiliza la nueva plantilla unificada oficial de Microsoft para C\#.
  * **`-int Server`**: Configura la "Interactividad" (Interactive Render Mode) específicamente como **Server**. Esto recrea el comportamiento de lo que antes se conocía como "Blazor Server", manteniendo la UI sincronizada mediante SignalR como indica el marco teórico de la Clase 1.

## 3\. Verificación del Entorno

Para asegurar que se está trabajando bajo el estándar correcto del curso:

1.  **SDK**: Verificar que `dotnet --version` devuelva una versión **8.x**.
2.  **Lenguaje**: El proyecto debe contener archivos `.razor` con bloques `@code` escritos en **C\#**.

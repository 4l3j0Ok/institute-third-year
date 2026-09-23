# Paginación en una aplicación WinForms

## Objetivo

Esta guía explica cómo mostrar una lista grande de registros por bloques en una aplicación WinForms. En lugar de cargar todos los clientes en el `DataGridView`, la aplicación consulta únicamente los registros de la página seleccionada y permite navegar entre ellas con `Paginator`.

El ejemplo trabaja con 123 clientes y muestra 10 por página. Por eso genera 13 páginas: las primeras 12 contienen 10 registros y la última contiene 3.

## Conceptos previos

El código usa algunas ideas de C# y de ADO.NET (la forma en que .NET habla con una base de datos) que quizás todavía no viste en la cursada. Antes de seguir, repasemos cada una.

### Conexión y comando SQL (ADO.NET)

ADO.NET es la biblioteca de .NET para hablar con una base de datos relacional. Trabaja con dos objetos principales:

- `SqlConnection`: representa la conexión abierta hacia el servidor de base de datos (dirección, usuario, base a usar, etc.). Sin una conexión abierta no se puede ejecutar ninguna instrucción.
- `SqlCommand` (`command`: "orden" o "instrucción"): representa una instrucción SQL puntual (un `SELECT`, un `INSERT`, etc.) que se ejecuta sobre esa conexión. Se obtiene con `connection.CreateCommand()` y su texto se define en la propiedad `CommandText`.

Una vez armado el comando, existe un método distinto según lo que se espera como resultado:

| Método              | Cuándo se usa                                                | Qué devuelve                                                                |
| ------------------- | ------------------------------------------------------------ | --------------------------------------------------------------------------- |
| `ExecuteScalar()`   | La consulta devuelve un único valor                          | Ese valor (por ejemplo, el resultado de `COUNT(*)`)                         |
| `ExecuteReader()`   | La consulta devuelve varias filas                            | Un `SqlDataReader` para recorrerlas una por una con `while (reader.Read())` |
| `ExecuteNonQuery()` | La instrucción modifica datos (`INSERT`, `UPDATE`, `DELETE`) | La cantidad de filas afectadas; no hay filas para leer                      |

> `Scalar` viene del término matemático "escalar": un único valor, a diferencia de una lista. Por eso `ExecuteScalar` devuelve un solo dato. `NonQuery` es literalmente "no consulta" (`non` = no, `query` = consulta): se usa cuando la instrucción no devuelve filas para leer.

### Consultas parametrizadas

En vez de armar el SQL concatenando texto —por ejemplo `"SELECT * FROM Clientes WHERE Id = " + id`—, se escriben marcadores como `@id` dentro del `CommandText` y se les asigna un valor por separado con `command.Parameters.AddWithValue("@id", id)`.

```csharp
command.CommandText = "SELECT * FROM Clientes WHERE Id = @id";
command.Parameters.AddWithValue("@id", id);
```

Esto tiene dos ventajas frente a concatenar strings:

1. **Seguridad:** evita la _inyección SQL_, un ataque donde un usuario malicioso escribe SQL dentro de un campo de texto para alterar la consulta original.
2. **Correctitud:** evita errores de formato al mezclar texto SQL con comillas, fechas o números, que ADO.NET resuelve automáticamente según el tipo del parámetro.

### `using` e `IDisposable`

Algunos objetos representan recursos "caros" que el sistema operativo debe liberar explícitamente cuando se dejan de usar: conexiones de red, archivos abiertos, handles, etc. En .NET, esos objetos implementan la interfaz `IDisposable`, que define un método `Dispose()` (de "deshacerse de algo", no de "disponer" en el sentido de "tener disponible", un falso amigo frecuente) para liberarlos.

`SqlConnection` es uno de esos objetos. En vez de acordarse de llamar a `Dispose()` a mano (y arriesgarse a olvidarlo si ocurre una excepción en el medio), C# ofrece la palabra clave `using`:

```csharp
using var connection = new SqlConnection(connectionString);
connection.Open();
// ... usar la conexión ...
// Dispose() se llama automáticamente al salir del bloque, incluso si hubo una excepción.
```

Por eso en el código vas a ver `using var connection = ...` y `using var command = ...` en casi todos los métodos del repositorio.

**Ojo con una fuente común de confusión:** la palabra `using` se usa en C# para dos cosas completamente distintas. Arriba de todo del archivo, `using System;` importa un espacio de nombres (namespace). Acá, `using var connection = ...` es otra cosa: un _statement_ que garantiza la liberación del recurso. Se llama igual por casualidad del diseño del lenguaje, pero no tienen relación entre sí.

### Propiedades con campo de respaldo (_backing field_)

Ya conocés las propiedades automáticas, donde el compilador genera el campo privado por vos:

```csharp
public int PageSize { get; set; } = 10;
```

Cuando una propiedad necesita **validar o transformar** el valor antes de guardarlo, hay que declarar el campo privado a mano (el _backing field_ o "campo de respaldo" —`backing` viene de "to back up", respaldar o sostener algo desde atrás—, por convención con guion bajo) y escribir la lógica en el `set`:

```csharp
private int _totalItems;

public int TotalItems
{
    get => _totalItems;
    set { _totalItems = Math.Max(0, value); ClampPage(); Invalidate(); }
}
```

Acá `TotalItems` nunca puede quedar en negativo: cualquier valor que se le asigne pasa primero por `Math.Max(0, value)`. Es la misma idea de una propiedad automática, pero con una regla de negocio agregada.

### Eventos y expresiones lambda

Un **evento** es la forma en que un objeto avisa a otros que "algo pasó", sin necesidad de conocerlos de antemano. Se declara así:

```csharp
public event EventHandler<PageChangedEventArgs>? PageChanged;
```

- `EventHandler<T>` (`handler`: "manejador", de "to handle" = manejar/atender) es el tipo estándar de .NET para eventos: siempre recibe dos parámetros, el objeto que dispara el evento (`sender`, literalmente "quien envía") y un objeto con los datos del evento (en este caso, `PageChangedEventArgs`, que trae la página elegida).
- Otro objeto se **suscribe** al evento con el operador `+=`, pasando un método o una función anónima (**lambda**) que se va a ejecutar cada vez que el evento se dispare.

> `Lambda` no viene del inglés cotidiano sino de la letra griega λ. En los años 30, el matemático Alonzo Church la usó para representar funciones en un sistema lógico llamado "cálculo lambda". Los lenguajes de programación heredaron el nombre para las funciones anónimas y cortas, como la que sigue.

```csharp
_paginator.PageChanged += (_, args) => LoadPage(args.Page);
```

Esa línea se lee así: "cuando `_paginator` dispare `PageChanged`, ejecutá `LoadPage(args.Page)`". La lambda `(_, args) => ...` recibe los dos parámetros de `EventHandler<T>`; el `_` es un _discard_ (descarte, de "to discard": tirar algo que no se va a usar) que indica "no me interesa este parámetro" —en este caso, el `sender`—, mientras que `args` sí se usa para leer `args.Page`.

### Data binding

_Data binding_ (enlace de datos; `binding` viene de "to bind" = atar/vincular) es la técnica por la cual un control visual muestra automáticamente el contenido de una colección de objetos, sin que el programador tenga que agregar filas o columnas a mano.

```csharp
_grid.DataSource = _repository.GetPage(page, PageSize);
```

Al asignar una lista de `Cliente` a `DataSource`, el `DataGridView` inspecciona las propiedades públicas de `Cliente` (`Id`, `Name`, `Email`, `City`) y genera una columna por cada una, más una fila por cada elemento de la lista. El control queda literalmente "atado" a la colección: si la lista cambia, alcanza con volver a asignar `DataSource` para que la grilla se actualice sola.

### Clases parciales (`partial class`)

El modificador `partial` permite dividir la definición de una misma clase en varios archivos `.cs`; en tiempo de compilación, C# los combina como si fueran uno solo.

WinForms aprovecha esto para separar dos responsabilidades:

- `MainForm.Designer.cs`: generado automáticamente por el Diseñador de Visual Studio. Contiene la declaración de los controles y su configuración (posición, tamaño, nombre) dentro de `InitializeComponent()`.
- `MainForm.cs`: donde el desarrollador escribe la lógica propia (qué hacer cuando se hace clic en un botón, cómo cargar una página, etc.).

Ambos archivos empiezan con `public partial class MainForm : Form`, y juntos forman una única clase `MainForm`.

### `sealed`

`Sealed` viene del verbo "to seal": sellar, como sellar un sobre o un frasco. Una vez que algo está sellado, queda cerrado: nadie puede abrirlo para agregarle o sacarle algo por encima. En C#, el modificador `sealed` marca una clase (o un `record`) como **no heredable**: ninguna otra clase puede escribir `class Otra : ClienteRepository`. Se usa así:

```csharp
public sealed class ClienteRepository(string connectionString)
{
    ...
}

public sealed record Cliente(long Id, string Name, string Email, string City);
```

En este proyecto casi todas las clases son `sealed` (`Cliente`, `ClienteRepository`, `PaginationStyle`, `PageChangedEventArgs`) porque ninguna fue diseñada para ser una clase base: no tienen miembros `protected` ni `virtual` pensados para que una subclase los redefina. Marcarlas como `sealed` dos beneficios:

- **Comunica la intención:** cualquiera que lea el código sabe que esa clase se usa "tal cual", sin extenderla.
- **Evita errores de herencia accidental:** nadie puede heredar de `ClienteRepository` y romper su comportamiento interno sin darse cuenta.

Si más adelante una clase necesita ser extendida, simplemente se le quita el `sealed`.

## ¿Para qué sirve paginar?

La paginación mejora la experiencia de uso y reduce el trabajo de la aplicación y de la base de datos cuando existen muchos registros. Sus beneficios principales son:

- Evita traer y enlazar todos los registros al `DataGridView`.
- Mantiene una cantidad de filas manejable para el usuario.
- Permite indicar con precisión qué parte del total se está visualizando.
- Hace que cada consulta solicite solo los datos necesarios mediante `OFFSET` y `FETCH NEXT`.

En esta implementación las páginas se numeran desde 1. Para una página `p` y un tamaño `s`, el desplazamiento de la consulta es `(p - 1) * s`.

## Fundamentos de base de datos

Antes de escribir código de WinForms, conviene separar el problema en dos preguntas que resuelve la base de datos:

1. ¿Cuántos registros existen? El total permite saber cuántas páginas hay y mostrar el estado de la grilla.
2. ¿Qué registros corresponden a la página actual? La consulta devuelve solamente ese bloque, no la tabla completa.

La tabla `Clientes` tiene la siguiente estructura:

| Columna | Tipo                      | Descripción                                       |
| ------- | ------------------------- | ------------------------------------------------- |
| `Id`    | `BIGINT` (PK, `IDENTITY`) | Identificador único del cliente, autoincremental. |
| `Name`  | `NVARCHAR(200)`           | Nombre del cliente.                               |
| `Email` | `NVARCHAR(320)`           | Correo electrónico del cliente.                   |
| `City`  | `NVARCHAR(100)`           | Ciudad del cliente.                               |

Supongamos esa tabla, una página 3 y 10 registros por página. El desplazamiento se calcula una única vez en la aplicación antes de ejecutar la consulta:

```sql
-- page = 3, pageSize = 10
-- offset = (page - 1) * pageSize = 20
```

Primero se consulta el total. Si la grilla permite filtrar, el `WHERE` de esta consulta debe ser el mismo que el de la consulta de la página.

```sql
SELECT COUNT(*) AS TotalRegistros
FROM Clientes;
```

En SQL Server, la página se obtiene con `OFFSET ... FETCH NEXT`. `ORDER BY` define un orden estable y es obligatorio al usar esta forma de paginación; sin él, la noción de "página 3" no sería confiable.

```sql
SELECT Id, Name, Email, City
FROM Clientes
ORDER BY Id
OFFSET @Desplazamiento ROWS
FETCH NEXT @TamanoPagina ROWS ONLY;
```

Con el ejemplo anterior, SQL Server omite los primeros 20 clientes ordenados por `Id` y devuelve los siguientes 10: los registros 21 a 30. Si el total fuera 123, la cantidad de páginas sería `CEILING(123.0 / 10)`, es decir, 13. En una grilla con filtros, el mismo `WHERE` debe aplicarse tanto al `COUNT(*)` como a la consulta paginada.

El flujo se mantiene siempre igual: contar, calcular el desplazamiento, ordenar y solicitar solo el bloque necesario.

Ahora que el concepto de paginación en la base de datos está claro, vamos con el código que conecta esas consultas con la grilla y el control de navegación de WinForms.

## Componentes de la solución

| Componente          | Responsabilidad                                                                 |
| ------------------- | ------------------------------------------------------------------------------- |
| `ClienteRepository` | Cuenta los registros y obtiene los clientes de una página desde SQL Server.     |
| `Paginator`         | Crea los botones de página, calcula las páginas visibles y emite `PageChanged`. |
| `MainForm`          | Coordina la consulta, el `DataGridView`, el paginador y el texto de estado.     |

## Paso 1: preparar la conexión a SQL Server

La aplicación usa el proveedor `Microsoft.Data.SqlClient`. Agregá la referencia a este paquete en el proyecto de la demo:

```xml
<PackageReference Include="Microsoft.Data.SqlClient" Version="6.1.0" />
```

Antes de ejecutar, creá una base SQL Server y configurá su cadena de conexión. La aplicación crea la tabla `Clientes` en esa base cuando se inicia.

```powershell
$env:PAGINATION_DEMO_CONNECTION_STRING = "Server=(localdb)\MSSQLLocalDB;Database=PaginationDemo;Integrated Security=True;TrustServerCertificate=True"
```

`MainForm` obtiene esa configuración y la entrega al repositorio. Si no se define, la aplicación se detiene con un mensaje que indica cómo configurarla.

```csharp
var connectionString = Environment.GetEnvironmentVariable(
    "PAGINATION_DEMO_CONNECTION_STRING");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException(...);

_repository = new ClienteRepository(connectionString);
_repository.Initialize();
```

## Paso 2: definir el tamaño de página

En `MainForm` se declara una constante para que la misma cantidad se use al consultar la base y al calcular las páginas.

```csharp
private const int PageSize = 10;
```

> **[CAPTURA PENDIENTE DEL DESIGNER 1]**
>
> Insertar una captura de `MainForm` en el Diseñador con el `DataGridView` y el panel inferior que contendrá el paginador.

## Paso 3: consultar solo los registros necesarios

El repositorio necesita dos operaciones distintas:

1. `Count()` obtiene el total de filas. Ese valor permite calcular cuántas páginas existen.
2. `GetPage(page, pageSize)` recupera solo el bloque que corresponde a la página solicitada.

La consulta usa parámetros para enviar el tamaño y el desplazamiento. `OFFSET` omite las filas de las páginas anteriores y `FETCH NEXT` restringe las filas devueltas.

`connection.CreateCommand()` crea el `SqlCommand` que va a ejecutar la consulta sobre esa conexión; `command.CommandText` es el texto SQL y `command.Parameters` son los valores que reemplazan a `@offset` y `@pageSize` (ver [Conceptos previos](#conceptos-previos)). El método devuelve `IReadOnlyList<Cliente>`, es decir, una lista que quien la recibe puede leer pero no modificar.

```csharp
public IReadOnlyList<Cliente> GetPage(int page, int pageSize)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();
    using var command = connection.CreateCommand();
    command.CommandText = "SELECT Id, Name, Email, City FROM Clientes " +
                          "ORDER BY Id OFFSET @offset ROWS " +
                          "FETCH NEXT @pageSize ROWS ONLY";
    command.Parameters.AddWithValue("@pageSize", pageSize);
    command.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
    ...
}
```

Si se solicita la página 3 con 10 elementos por página, el desplazamiento será `(3 - 1) * 10`, es decir, 20. SQL Server devolverá los registros 21 a 30 según el orden definido por `ORDER BY Id`.

> **Importante:** el `ORDER BY` es obligatorio en una lista paginada. Sin un orden estable, un mismo registro podría aparecer en distintas páginas entre una consulta y otra.

## Paso 4: agregar y configurar el paginador en el Designer

`Paginator` es un `UserControl` que vive en `src/Pagination.Demo/Views/UserControls`, dentro del mismo proyecto de la aplicación (no es una librería separada). Después de compilar, queda disponible en el cuadro de herramientas para arrastrarlo a cualquier formulario del proyecto, igual que cualquier otro control de WinForms.

Configurá estas propiedades desde la ventana **Propiedades**:

| Propiedad     | Valor        | Propósito                                        |
| ------------- | ------------ | ------------------------------------------------ |
| `Name`        | `_paginator` | Identifica el control desde `MainForm.cs`.       |
| `PageSize`    | `10`         | Indica cuántos registros representa cada página. |
| `CurrentPage` | `1`          | Define la página inicial.                        |
| `Dock`        | `Fill`       | Hace que el control ocupe la celda del pie.      |
| `MinimumSize` | `240, 40`    | Conserva espacio suficiente para los botones.    |

Ubicá el control debajo de la grilla, junto con un `Label` llamado `_status`. En el ejemplo, ambos controles están dentro de un `TableLayoutPanel` con dos columnas: una para el paginador y otra para el estado.

> **[CAPTURA PENDIENTE DEL DESIGNER 2]**
>
> Insertar una captura del `TableLayoutPanel` inferior mostrando `_paginator` y `_status`.

El control expone `TotalItems`, `PageSize` y `CurrentPage`. A partir de los dos primeros calcula `TotalPages`; por ejemplo, 123 elementos con tamaño 10 producen 13 páginas.

```csharp
public partial class Paginator : UserControl
{
    ...
    public int TotalItems { get => _totalItems; set { ... } }
    public int PageSize { get => _pageSize; set { ... } }
    public int CurrentPage { get => _currentPage; set => GoTo(value); }

    public int TotalPages => Math.Max(1,
        (int)Math.Ceiling((double)TotalItems / PageSize));
    ...
}
```

Estas propiedades no son automáticas (`{ get; set; }`): cada una guarda su valor en un campo privado (`_totalItems`, `_pageSize`) y el `set` valida ese valor antes de guardarlo —por ejemplo, `TotalItems` no puede quedar negativo, y `CurrentPage` no puede superar `TotalPages` (ver `ClampPage` y `GoTo` más abajo). Los atributos que aparecen arriba de cada propiedad en el código real (`[Category]`, `[Description]`, `[DefaultValue]`, `[ToolboxItem(true)]`) no afectan el comportamiento del control: son metadatos que usa Visual Studio para mostrar el control en el cuadro de herramientas y organizar sus propiedades en la ventana **Propiedades** del Diseñador.

Igual que `MainForm` y `ClienteEditForm`, `Paginator` está dividido en dos archivos: `Paginator.Designer.cs` (generado por el Diseñador, define las flechas `‹`/`›` y el panel donde se agregan los botones de página dentro de `InitializeComponent()`) y `Paginator.cs` (la lógica: `TotalItems`, `PageSize`, `CurrentPage`, `GetTentativePages`, y la creación dinámica de los botones numéricos según la página actual).

## Paso 5: conectar el evento y cargar la primera página

En el constructor de `MainForm`, asociá el evento `PageChanged` y cargá la página 1 cuando el formulario ya se mostró. Esto evita consultar datos antes de que los controles estén listos.

```csharp
_paginator.PageChanged += (_, args) => LoadPage(args.Page);
Shown += (_, _) => LoadPage(1);
```

La primera línea suscribe una lambda al evento `PageChanged` de `_paginator`: cada vez que el paginador dispare ese evento, se va a ejecutar `LoadPage(args.Page)`, donde `args.Page` es la página que el usuario eligió. El primer parámetro de la lambda (el objeto que disparó el evento) se ignora con `_` porque no se necesita. La segunda línea hace lo mismo con el evento `Shown` del formulario, que se dispara una sola vez cuando la ventana ya es visible; ahí se ignoran los dos parámetros porque tampoco se usan.

Cada vez que el usuario selecciona un número, la flecha anterior o la flecha siguiente, `Paginator` actualiza `CurrentPage` y publica `PageChanged` con el número de destino.

## Paso 6: centralizar la carga de una página

`LoadPage` es el punto central de la paginación. Debe realizar estas tareas, siempre en este orden:

1. Contar los registros actuales.
2. Asignar el total al paginador para que calcule y valide sus páginas.
3. Consultar la página solicitada.
4. Enlazar el resultado al `DataGridView`.
5. Actualizar el texto que informa el rango mostrado.

```csharp
private void LoadPage(int page)
{
    var total = _repository.Count();
    _paginator.TotalItems = total;
    if (_paginator.CurrentPage != page)
    {
        _paginator.CurrentPage = page;
        return;
    }

    _grid.DataSource = _repository.GetPage(page, PageSize);
    var from = total == 0 ? 0 : (page - 1) * PageSize + 1;
    var to = Math.Min(page * PageSize, total);
    _status.Text = $"Mostrando {from}–{to} de {total}";
    ...
}
```

`_grid.DataSource = _repository.GetPage(page, PageSize);` es donde ocurre el _data binding_: al asignar la lista de `Cliente` al `DataSource`, el `DataGridView` genera solo una columna por cada propiedad pública de `Cliente` (`Id`, `Name`, `Email`, `City`) y una fila por cada elemento de la lista, sin que haya que escribir código para dibujar filas o columnas.

La condición sobre `CurrentPage` sincroniza al control cuando se pide una página distinta. Al asignar la propiedad, el paginador dispara `PageChanged`; por ello el método sale con `return` y la segunda invocación realiza la consulta. Así se evita cargar dos veces la misma página.

El rango mostrado se calcula de la siguiente forma:

```csharp
var from = total == 0 ? 0 : (page - 1) * PageSize + 1;
var to = Math.Min(page * PageSize, total);
_status.Text = $"Mostrando {from}–{to} de {total}";
```

Para la última página de 123 clientes, el estado será `Mostrando 121–123 de 123`.

## Paso 7: mostrar una ventana de páginas cuando hay muchas

No es conveniente dibujar un botón por cada página si hay decenas o cientos. `GetTentativePages` mantiene siempre visibles la primera y la última página, muestra una ventana alrededor de la página actual y agrega elipsis cuando hay páginas intermedias ocultas.

```csharp
public IReadOnlyList<int?> GetTentativePages(int maxButtons = 7)
{
    if (TotalPages <= maxButtons)
        return Enumerable.Range(1, TotalPages).Select(x => (int?)x).ToList();

    var pages = new List<int?> { 1 };
    var start = Math.Max(2, CurrentPage - 2);
    var end = Math.Min(TotalPages - 1, CurrentPage + 2);
    if (start > 2) pages.Add(null); // null representa "..."
    for (var page = start; page <= end; page++) pages.Add(page);
    if (end < TotalPages - 1) pages.Add(null);
    pages.Add(TotalPages);
    return pages;
}
```

Por ejemplo, cerca de la página 10 se puede presentar `1, ..., 8, 9, 10, 11, 12, ..., 30`. Las flechas también se deshabilitan en los extremos para impedir que el usuario navegue a una página inválida.

> **[CAPTURA PENDIENTE DEL DESIGNER 3]**
>
> Insertar una captura de la ventana **Propiedades** del control `Paginator`, con `PageSize`, `CurrentPage`, `TotalItems` y `Style` visibles.

## Paso 8: refrescar después de altas, modificaciones y bajas

El total puede cambiar después de insertar o eliminar registros, por lo que la grilla no debe conservar datos antiguos.

- Después de un alta, se cuenta de nuevo y se navega a la última página para mostrar el registro creado.
- Después de una modificación, se recarga la página actual para mostrar los valores editados.
- Después de una baja, se recarga la página actual. Si era la última y quedó sin filas, `TotalItems` ajusta internamente `CurrentPage` a la última página válida.

Este último ajuste es importante: si se eliminan los últimos registros de la página 13, no debe quedar seleccionada una página que ya no existe.

## Verificación manual

1. Creá una base de datos SQL Server, por ejemplo `PaginationDemo`, y configurá una cadena de conexión. Para LocalDB:

   ```powershell
   $env:PAGINATION_DEMO_CONNECTION_STRING = "Server=(localdb)\MSSQLLocalDB;Database=PaginationDemo;Integrated Security=True;TrustServerCertificate=True"
   $env:PAGINATION_DEMO_SEED = "true"
   dotnet run --project .\src\Pagination.Demo
   ```

2. Confirmá que la aplicación cree `Clientes`, se muestren 10 filas y el estado `Mostrando 1–10 de 123`.
3. Navegá a la página 2 y verificá que el estado cambie a `Mostrando 11–20 de 123`.
4. Navegá a la última página y verificá que se muestren 3 filas.
5. Eliminá los registros de la última página y comprobá que el control vuelva automáticamente a la última página válida.

## Resultado

La aplicación mantiene la grilla, el estado y el paginador sincronizados. El repositorio obtiene únicamente la página necesaria, mientras que `Paginator` encapsula la navegación, el cálculo de páginas y la prevención de valores fuera de rango.

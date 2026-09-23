# Paginación en una aplicación WinForms

## Objetivo

Esta guía explica cómo mostrar una lista grande de registros por bloques en una aplicación WinForms. En lugar de cargar todos los clientes en el `DataGridView`, la aplicación consulta únicamente los registros de la página seleccionada y permite navegar entre ellas con `SkiaPaginator`.

El ejemplo trabaja con 123 clientes y muestra 10 por página. Por eso genera 13 páginas: las primeras 12 contienen 10 registros y la última contiene 3.

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

Supongamos la tabla `dbo.Customers`, una página 3 y 10 registros por página. El desplazamiento se calcula una única vez en la aplicación antes de ejecutar la consulta:

```sql
-- page = 3, pageSize = 10
-- offset = (page - 1) * pageSize = 20
```

Primero se consulta el total. Si la grilla permite filtrar, el `WHERE` de esta consulta debe ser el mismo que el de la consulta de la página.

```sql
SELECT COUNT(*) AS TotalRegistros
FROM dbo.Customers;
```

En SQL Server, la página se obtiene con `OFFSET ... FETCH NEXT`. `ORDER BY` define un orden estable y es obligatorio al usar esta forma de paginación; sin él, la noción de "página 3" no sería confiable.

```sql
SELECT Id, Name, Email, City
FROM dbo.Customers
ORDER BY Id
OFFSET @Desplazamiento ROWS
FETCH NEXT @TamanoPagina ROWS ONLY;
```

Con el ejemplo anterior, SQL Server omite los primeros 20 clientes ordenados por `Id` y devuelve los siguientes 10: los registros 21 a 30. Si el total fuera 123, la cantidad de páginas sería `CEILING(123.0 / 10)`, es decir, 13. En una grilla con filtros, el mismo `WHERE` debe aplicarse tanto al `COUNT(*)` como a la consulta paginada.

El flujo se mantiene siempre igual: contar, calcular el desplazamiento, ordenar y solicitar solo el bloque necesario.

Ahora que el concepto de paginación en la base de datos está claro, vamos con el código que conecta esas consultas con la grilla y el control de navegación de WinForms.

## Componentes de la solución

| Componente | Responsabilidad |
| --- | --- |
| `CustomerRepository` | Cuenta los registros y obtiene los clientes de una página desde SQL Server. |
| `SkiaPaginator` | Dibuja los botones de páginas, calcula las páginas visibles y emite `PageChanged`. |
| `MainForm` | Coordina la consulta, el `DataGridView`, el paginador y el texto de estado. |

## Paso 1: preparar la conexión a SQL Server

La aplicación usa el proveedor `Microsoft.Data.SqlClient`. Agregá la referencia a este paquete en el proyecto de la demo:

```xml
<PackageReference Include="Microsoft.Data.SqlClient" Version="6.1.0" />
```

Antes de ejecutar, creá una base SQL Server y configurá su cadena de conexión. La aplicación crea la tabla `dbo.Customers` en esa base cuando se inicia.

```powershell
$env:PAGINATION_DEMO_CONNECTION_STRING = "Server=(localdb)\MSSQLLocalDB;Database=PaginationDemo;Integrated Security=True;TrustServerCertificate=True"
```

`MainForm` obtiene esa configuración y la entrega al repositorio. Si no se define, la aplicación se detiene con un mensaje que indica cómo configurarla.

```csharp
var connectionString = Environment.GetEnvironmentVariable(
    "PAGINATION_DEMO_CONNECTION_STRING");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException(...);

_repository = new CustomerRepository(connectionString);
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

```csharp
public IReadOnlyList<Customer> GetPage(int page, int pageSize)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();
    using var command = connection.CreateCommand();
    command.CommandText = "SELECT Id, Name, Email, City FROM dbo.Customers " +
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

Agregá una referencia al proyecto `SkiaPagination.WinForms` desde la aplicación WinForms. Después de compilar, `SkiaPaginator` estará disponible en el cuadro de herramientas para arrastrarlo al formulario.

Configurá estas propiedades desde la ventana **Propiedades**:

| Propiedad | Valor | Propósito |
| --- | --- | --- |
| `Name` | `_paginator` | Identifica el control desde `MainForm.cs`. |
| `PageSize` | `10` | Indica cuántos registros representa cada página. |
| `CurrentPage` | `1` | Define la página inicial. |
| `Dock` | `Fill` | Hace que el control ocupe la celda del pie. |
| `MinimumSize` | `240, 40` | Conserva espacio suficiente para los botones. |

Ubicá el control debajo de la grilla, junto con un `Label` llamado `_status`. En el ejemplo, ambos controles están dentro de un `TableLayoutPanel` con dos columnas: una para el paginador y otra para el estado.

> **[CAPTURA PENDIENTE DEL DESIGNER 2]**
>
> Insertar una captura del `TableLayoutPanel` inferior mostrando `_paginator` y `_status`.

El control expone `TotalItems`, `PageSize` y `CurrentPage`. A partir de los dos primeros calcula `TotalPages`; por ejemplo, 123 elementos con tamaño 10 producen 13 páginas.

```csharp
public sealed class SkiaPaginator : SKControl
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

## Paso 5: conectar el evento y cargar la primera página

En el constructor de `MainForm`, asociá el evento `PageChanged` y cargá la página 1 cuando el formulario ya se mostró. Esto evita consultar datos antes de que los controles estén listos.

```csharp
_paginator.PageChanged += (_, args) => LoadPage(args.Page);
Shown += (_, _) => LoadPage(1);
```

Cada vez que el usuario selecciona un número, la flecha anterior o la flecha siguiente, `SkiaPaginator` actualiza `CurrentPage` y publica `PageChanged` con el número de destino.

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
> Insertar una captura de la ventana **Propiedades** del control `SkiaPaginator`, con `PageSize`, `CurrentPage`, `TotalItems` y `Style` visibles.

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
   dotnet run --project .\src\SkiaPagination.Demo
   ```

2. Confirmá que la aplicación cree `dbo.Customers`, se muestren 10 filas y el estado `Mostrando 1–10 de 123`.
3. Navegá a la página 2 y verificá que el estado cambie a `Mostrando 11–20 de 123`.
4. Navegá a la última página y verificá que se muestren 3 filas.
5. Eliminá los registros de la última página y comprobá que el control vuelva automáticamente a la última página válida.

## Resultado

La aplicación mantiene la grilla, el estado y el paginador sincronizados. El repositorio obtiene únicamente la página necesaria, mientras que `SkiaPaginator` encapsula la navegación, el cálculo de páginas y la prevención de valores fuera de rango.

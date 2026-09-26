# Paginación en una aplicación WinForms

## Resumen

Esta guía muestra cómo se construyó la paginación de una aplicación WinForms que lista clientes. La tabla `Clientes` tiene 123 filas y se muestran en un `DataGridView`.

Traerlas todas de una sola consulta y dejar que la grilla se las arregle es una opción, pero implica viajar 123 filas por la red cada vez que se abre la ventana, y esa cantidad solo va a crecer con el tiempo. Por eso aplicamos paginación: se piden bloques de 10 registros, se muestran botones para moverse entre páginas, y la base de datos hace el trabajo de recortar el bloque que corresponde en cada momento. Con 123 clientes y 10 por página, el resultado son 13 páginas: las primeras 12 completas y la última con 3.

Antes de entrar en el código, se repasan algunas ideas de C# y ADO.NET que aparecen todo el tiempo más abajo y que quizás no se vieron todavía en la cursada. Si ya son conocidas, se puede saltar directamente a [Pensando primero en la base de datos](#pensando-primero-en-la-base-de-datos).

## Conceptos a tener en cuenta utilizados en este ejemplo

### Conexión y comando SQL (ADO.NET)

Trabajaremos con dos objetos principales de ADO.NET, la librería que nos permite interactuar con bases de datos relacionales:

- `SqlConnection`: representa la conexión abierta hacia el servidor de base de datos, incluida su dirección, usuario y base a usar. Sin una conexión abierta no se puede ejecutar ninguna instrucción.
- `SqlCommand`: representa una instrucción SQL puntual, como un `SELECT` o un `INSERT`, que se ejecuta sobre esa conexión. Se obtiene con `connection.CreateCommand()` y su texto se define en la propiedad `CommandText`.

Una vez armado el comando, existe un método distinto según lo que se espera como resultado:

| Método              | Cuándo se usa                                                                 | Qué devuelve                                                                 |
| ------------------- | ----------------------------------------------------------------------------- | ---------------------------------------------------------------------------- |
| `ExecuteScalar()`   | La consulta devuelve un único valor.                                          | Ese valor, por ejemplo el resultado de `COUNT(*)`.                           |
| `ExecuteReader()`   | La consulta devuelve varias filas.                                            | Un `SqlDataReader` para recorrerlas una por una con `while (reader.Read())`. |
| `ExecuteNonQuery()` | La instrucción modifica datos, por ejemplo con `INSERT`, `UPDATE` o `DELETE`. | La cantidad de filas afectadas; no hay filas para leer.                      |

`ExecuteScalar` devuelve un único valor, como el resultado de `COUNT(*)`. `ExecuteNonQuery` se usa cuando la instrucción no devuelve filas para leer, solo la cantidad de filas afectadas. En este ejemplo de solo lectura se usan principalmente `ExecuteScalar` y `ExecuteReader`.

### Consultas parametrizadas

En vez de armar el SQL concatenando texto, por ejemplo `"SELECT * FROM Clientes WHERE Id = " + id`, se escriben marcadores como `@id` dentro del `CommandText` y se les asigna un valor por separado con `command.Parameters.AddWithValue("@id", id)`.

```csharp
command.CommandText = "SELECT * FROM Clientes WHERE Id = @id";
command.Parameters.AddWithValue("@id", id);
```

Esto tiene tres ventajas frente a concatenar strings:

1. **Es SQL puro:** el uso de variables con `@` es propio del lenguaje SQL.
2. **Seguridad:** evita la inyección SQL, un ataque donde un usuario malicioso escribe SQL dentro de un campo de texto para alterar la consulta original.
3. **Correctitud:** evita errores de formato al mezclar texto SQL con comillas, fechas o números, que ADO.NET resuelve automáticamente según el tipo del parámetro.

### `using` e `IDisposable`

Algunos objetos representan recursos "caros" que el sistema operativo debe liberar explícitamente cuando se dejan de usar: conexiones de red, archivos abiertos o handles, entre otros. En .NET, esos objetos implementan la interfaz `IDisposable`, que define un método `Dispose()` para liberarlos.

`SqlConnection` es uno de esos objetos. En vez de llamar a `Dispose()` a mano, con el riesgo de olvidarlo si ocurre una excepción en el medio, se usa la palabra clave `using`:

```csharp
using var connection = new SqlConnection(connectionString);
connection.Open();
// Usar la conexión.
// Dispose() se llama automáticamente al salir del bloque, incluso si hubo una excepción.
```

Por eso aparece `using var connection = ...` y `using var command = ...` en los métodos del repositorio.

> **Ojo con una fuente común de confusión:** la palabra `using` se usa en C# para dos cosas distintas. Arriba de todo del archivo, `using System;` importa un espacio de nombres. En cambio, `using var connection = ...` garantiza la liberación de un recurso. Se llaman igual, pero no tienen relación entre sí.

### Suscribirse a un evento con una lambda

Una forma habitual de manejar un evento en WinForms es hacer doble clic sobre un control en el Diseñador. Visual Studio genera un método, por ejemplo `button1_Click`, y lo suscribe automáticamente. También se puede hacer la suscripción en el constructor, pasando una expresión lambda en lugar de un método con nombre:

```csharp
_previousButton.Click += (_, _) => LoadPage(_currentPage - 1);
_currentPageButton.Click += PageButtonClicked;
```

La primera línea se lee como "cuando se haga clic en la flecha anterior, cargar la página anterior". La segunda reutiliza `PageButtonClicked` para los botones numéricos: el método lee de `Tag` el número de página que el botón representa en ese momento. Esta reutilización evita tener un manejador distinto para cada número.

Los controles se declaran y configuran en `MainForm.Designer.cs`. La suscripción de los eventos queda en el constructor de `MainForm.cs`, junto con la lógica que decide qué página cargar.

### `sealed`

El modificador `sealed` marca una clase o un `record` como no heredable: ninguna otra clase puede escribir `class Otra : ClienteRepository`. Se usa así:

```csharp
public sealed class ClienteRepository(string connectionString)
{
    // ...
}

public sealed record Cliente(long Id, string Name, string Email, string City);
```

En este proyecto `Cliente` y `ClienteRepository` están marcados como `sealed` porque ninguna fue diseñada para ser una clase base: no tienen miembros `protected` ni `virtual` pensados para que una subclase los redefina. Esto comunica la intención y evita herencias accidentales que podrían romper el comportamiento interno.

## Qué es paginar y cuándo conviene

Paginar es dividir un conjunto grande de datos en bloques, llamados páginas, y trabajar con un bloque por vez, en lugar de traer y mostrar todo de una sola vez. Es una técnica común en listas largas de clientes, productos, pedidos o resultados de búsqueda, tanto en aplicaciones de escritorio como en sitios web y APIs.

Conviene paginar cuando:

- La cantidad de registros es grande o va a seguir creciendo con el tiempo. Una tabla con 100 filas hoy puede tener 100.000 mañana.
- Solo hace falta mostrar una porción a la vez, como ocurre en un `DataGridView`, una lista web o los resultados de un buscador.
- Traer todo junto sería lento o consumiría demasiada memoria, tanto en el cliente como en el servidor.

No tiene mucho sentido paginar cuando el conjunto de datos es chico y estable, por ejemplo una lista fija de 20 categorías, porque ahí el costo de agregar la lógica de paginación supera el beneficio.

## Pensando primero en la base de datos

Antes de tocar WinForms conviene definir qué se le va a pedir a SQL Server, porque toda la paginación depende de eso. Hay dos preguntas para resolver:

1. ¿Cuántos registros hay en total? Sin ese número no se puede calcular cuántas páginas existen ni mostrar el estado de la grilla.
2. ¿Qué registros corresponden a la página que el usuario está mirando? La consulta tiene que devolver solo ese bloque, nunca la tabla completa.

En este ejemplo, la tabla `Clientes` tiene esta estructura:

| Columna | Tipo                      | Descripción                                       |
| ------- | ------------------------- | ------------------------------------------------- |
| `Id`    | `BIGINT` (PK, `IDENTITY`) | Identificador único del cliente, autoincremental. |
| `Name`  | `NVARCHAR(200)`           | Nombre del cliente.                               |
| `Email` | `NVARCHAR(320)`           | Correo electrónico del cliente.                   |
| `City`  | `NVARCHAR(100)`           | Ciudad del cliente.                               |

Para traer el bloque correcto, la aplicación necesita saber cuántos registros hay que saltear antes de empezar a leer la página pedida. Ese número se llama **desplazamiento** u _offset_, y es simplemente la cantidad de registros que ocupan todas las páginas anteriores a la actual:

```text
offset = (page - 1) * pageSize
```

Por ejemplo, con páginas de 10 registros, la página 1 no salta nada (`offset` 0), la página 2 salta la página 1 completa (`offset` 10) y la página 3 salta las dos páginas anteriores (`offset` 20).

Primero se pide el total. Si la grilla tuviera filtros, el `WHERE` de esta consulta tendría que ser idéntico al de la consulta de la página, porque si no los números no coinciden entre sí.

```sql
SELECT COUNT(*) AS TotalRegistros
FROM Clientes;
```

Para traer la página se usa `OFFSET ... FETCH NEXT`, que es la forma que ofrece SQL Server para este tipo de recorte. El `ORDER BY` no es opcional: sin un orden estable, la noción de "página 3" no significa nada, porque el motor podría devolver las filas en un orden distinto entre consultas.

```sql
SELECT Id, Name, Email, City
FROM Clientes
ORDER BY Id
OFFSET @offset ROWS
FETCH NEXT @pageSize ROWS ONLY;
```

Con `offset` 20 y tamaño 10, SQL Server descarta los primeros 20 clientes ordenados por `Id` y devuelve los siguientes 10: los registros 21 a 30. Con un total de 123, la cantidad de páginas sale de `CEILING(123.0 / 10)`, o sea 13.

El patrón se repite siempre igual: se cuenta, se calcula el desplazamiento, se ordena y se pide solo el bloque necesario. Con esto resuelto del lado de la base, se puede pasar a la aplicación.

## Cómo seguir la guía

A partir de ahora se mencionan bloques directos del código fuente de la aplicación. Se puede leer la explicación sin descargar el proyecto, pero conviene tenerlo abierto para ver el contexto completo de cada fragmento y seguir cómo se conectan sus partes desde el IDE.

## Armando las piezas

Para que esto funcione en WinForms se necesitan tres piezas que se reparten el trabajo:

| Componente          | Responsabilidad                                                                                            |
| ------------------- | ---------------------------------------------------------------------------------------------------------- |
| `Cliente`           | Representa una fila de la tabla con sus datos.                                                             |
| `ClienteRepository` | Cuenta los registros y obtiene los clientes de una página desde SQL Server.                                |
| `MainForm`          | Crea la interfaz, recibe los clics de los botones, consulta el repositorio y actualiza la grilla y el pie. |

### Cómo está organizado el proyecto

Antes de entrar en cada pieza, conviene mirar cómo se acomodan los archivos dentro de `PaginationDemo`:

```text
PaginationDemo/
├── Data/
│   └── ClienteRepository.cs
├── Models/
│   └── Cliente.cs
├── Views/
│   └── Forms/
│       ├── MainForm.cs
│       └── MainForm.Designer.cs
└── Program.cs
```

- **`Models/`** guarda las clases que representan datos, sin lógica de acceso a la base ni de interfaz. `Cliente` es un `record` con las propiedades `Id`, `Name`, `Email` y `City`.
- **`Data/`** guarda las clases que saben hablar con la base de datos. `ClienteRepository` sigue el patrón Repository: el resto de la aplicación le pide clientes sin necesidad de saber que detrás hay SQL, `SqlConnection` o `SqlCommand`.
- **`Views/Forms/`** guarda las ventanas completas de la aplicación. `MainForm.Designer.cs` contiene la declaración y la disposición visual de los controles; `MainForm.cs` contiene el estado, los eventos y la carga de datos.
- **`Program.cs`** es el punto de entrada de la aplicación. Carga la configuración y abre `MainForm`.

Estas convenciones no son obligatorias para que el código compile. Son acuerdos que facilitan que cualquier persona pueda ubicar rápido la responsabilidad de cada archivo.

### La conexión a SQL Server

Se usa el proveedor `Microsoft.Data.SqlClient`, por lo que el proyecto incluye esta referencia:

```xml
<PackageReference Include="Microsoft.Data.SqlClient" Version="6.1.0" />
```

Antes de correr nada hace falta una base SQL Server y su cadena de conexión. La aplicación crea la tabla `Clientes` cuando arranca, por lo que no hace falta crear la tabla a mano.

La configuración se guarda en un archivo `src/PaginationDemo/.env`. Se puede crear copiando `src/PaginationDemo/.env.example` y completando los valores:

```dotenv
PAGINATION_DEMO_CONNECTION_STRING="Server=(localdb)\MSSQLLocalDB;Database=PaginationDemo;Integrated Security=True;TrustServerCertificate=True"
PAGINATION_DEMO_SEED=true
```

Un archivo `.env` es un formato simple para guardar configuración como pares `NOMBRE=valor`. Permite separar los datos que cambian según cada computadora, por ejemplo la conexión a la base, del código fuente y evita versionar credenciales.

`Program` lee el archivo antes de abrir el formulario y deja cada valor disponible como una variable de entorno del proceso:

```csharp
LoadEnvironmentFile();
Application.Run(new MainForm());
```

Después, `MainForm` obtiene la cadena ya cargada y se la pasa al repositorio. Si falta, la aplicación falla rápido con un mensaje claro:

```csharp
var connectionString = Environment.GetEnvironmentVariable(
    "PAGINATION_DEMO_CONNECTION_STRING");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Creá src/PaginationDemo/.env con la conexión a la base de datos SQL Server.");

_repository = new ClienteRepository(connectionString);
_repository.Initialize();
```

En `MainForm` también se declara una constante para el tamaño de página, porque la misma cantidad se necesita para consultar la base y para calcular el rango del pie:

```csharp
private const int PageSize = 10;
```

### El repositorio: contar y traer una página

Con la conexión resuelta, el repositorio necesita exactamente dos operaciones de lectura:

1. `Count()`, que devuelve el total de filas para calcular cuántas páginas existen.
2. `GetPage(page, pageSize)`, que trae solo el bloque correspondiente a la página pedida.

Para esta segunda consulta se usan parámetros para enviar el tamaño y el desplazamiento. `connection.CreateCommand()` crea el `SqlCommand`, `command.CommandText` lleva el SQL y `command.Parameters` reemplaza a `@offset` y `@pageSize`. El método devuelve `IReadOnlyList<Cliente>` porque quien lo llama solo necesita leer la lista, no modificarla.

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
    // ...
}
```

Si se pide la página 3 con 10 elementos por página, el desplazamiento sale `(3 - 1) * 10 = 20`, y SQL Server devuelve los registros 21 a 30 según el `ORDER BY Id`.

## Diseño del formulario

El formulario se organiza en dos zonas: la grilla ocupa el espacio principal y, debajo, aparece el pie. La grilla muestra únicamente los clientes de la página actual; el pie contiene los botones y el texto de estado.

```text
[<] [4] [5] [6] [>]
Mostrando 41-50 de 123
```

Los botones son controles `Button` normales de WinForms. No existe un `UserControl` ni se generan controles dinámicamente. Los tres botones numéricos reutilizan los mismos controles, pero su texto se actualiza para mostrar la página anterior, la actual y la siguiente. Esta elección reduce el ejemplo para concentrarse en el recorrido completo: evento de clic, consulta de la página y actualización de la grilla.

`MainForm.Designer.cs` declara los cinco botones como campos. `MainForm.cs` conserva solamente el estado de navegación:

```csharp
private Button _previousButton;
private Button _previousPageButton;
private Button _currentPageButton;
private Button _nextPageButton;
private Button _nextButton;

// En MainForm.cs
private int _currentPage = 5;
```

`MainForm.Designer.cs` contiene el método `InitializeComponent`. Allí se crea un `FlowLayoutPanel`, se agregan los cinco botones y se lo coloca en la primera fila de un `TableLayoutPanel`. La segunda fila contiene la etiqueta `_status`. Ambos paneles son controles estándar de WinForms.

```csharp
_footer.ColumnCount = 3;
_footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
_footer.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
_footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
_footer.Controls.Add(_paginator, 1, 0);

_paginator.AutoSize = true;
_paginator.Controls.Add(_previousButton);
_paginator.Controls.Add(_previousPageButton);
_paginator.Controls.Add(_currentPageButton);
_paginator.Controls.Add(_nextPageButton);
_paginator.Controls.Add(_nextButton);
```

El código del diseñador agrega al panel los cinco botones que ya fueron declarados. El `TableLayoutPanel` tiene tres columnas: las columnas externas ocupan el espacio restante y el `FlowLayoutPanel` queda en la columna central con tamaño automático. Por eso los botones quedan centrados, incluso si cambia el ancho de la ventana.

### Eventos de los botones

En el constructor se conecta cada clic con la página que debe cargar:

```csharp
_previousButton.Click += (_, _) => LoadPage(_currentPage - 1);
_previousPageButton.Click += PageButtonClicked;
_currentPageButton.Click += PageButtonClicked;
_nextPageButton.Click += PageButtonClicked;
_nextButton.Click += (_, _) => LoadPage(_currentPage + 1);
Shown += (_, _) => LoadPage(_currentPage);
```

Las flechas usan `_currentPage` para pedir la anterior o la siguiente. Los tres botones numéricos cargan el número guardado en su propiedad `Tag`. El evento `Shown` carga la página inicial cuando el formulario ya está visible; como `_currentPage` empieza en 5, con los datos de ejemplo los números iniciales son `4`, `5` y `6`, y el rango mostrado es 41 a 50.

## Unificando todo en `LoadPage`

Este método es el corazón de la paginación. Siempre hace el mismo trabajo, en el mismo orden:

1. Cuenta los registros actuales.
2. Calcula cuántas páginas existen.
3. Mantiene la página pedida dentro de un rango válido.
4. Consulta y enlaza el bloque correspondiente al `DataGridView`.
5. Actualiza el texto del pie.
6. Actualiza los tres números de página y habilita o deshabilita las flechas según corresponda.

```csharp
private void LoadPage(int page)
{
    var total = _repository.Count();
    var totalPages = Math.Max(1, (int)Math.Ceiling((double)total / PageSize));
    _currentPage = Math.Clamp(page, 1, totalPages);
    _grid.DataSource = _repository.GetPage(_currentPage, PageSize);
    var from = total == 0 ? 0 : (_currentPage - 1) * PageSize + 1;
    var to = Math.Min(_currentPage * PageSize, total);
    _status.Text = $"Mostrando {from}–{to} de {total}";
    UpdatePageButtons(totalPages);
    _previousButton.Enabled = _currentPage > 1;
    _nextButton.Enabled = _currentPage < totalPages;
}
```

`Math.Ceiling` redondea hacia arriba. Con 123 clientes y páginas de 10, `123 / 10` da 12,3 y el redondeo da 13. `Math.Max(1, ...)` hace que exista al menos la página 1, incluso cuando la tabla está vacía.

`Math.Clamp(page, 1, totalPages)` limita el valor pedido al intervalo válido. Así nunca se consulta una página inexistente, incluso si se intenta navegar antes de la primera o después de la última.

Después de cambiar la página, `UpdatePageButtons` calcula la ventana de tres números. Cuando la página actual es 5 y hay 13 páginas, muestra 4, 5 y 6. En la primera página muestra 1, 2 y 3; en la última, 11, 12 y 13.

```csharp
var firstPage = Math.Clamp(_currentPage - 1, 1, Math.Max(1, totalPages - 2));
ConfigurePageButton(_previousPageButton, firstPage, totalPages);
ConfigurePageButton(_currentPageButton, firstPage + 1, totalPages);
ConfigurePageButton(_nextPageButton, firstPage + 2, totalPages);
```

Cada botón guarda el número que representa en `Tag`. Si existen menos de tres páginas, `ConfigurePageButton` oculta los botones que no correspondan.

La línea `_grid.DataSource = _repository.GetPage(_currentPage, PageSize);` es donde ocurre el _data binding_: al asignar la lista de `Cliente`, el `DataGridView` genera una columna por cada propiedad pública de `Cliente` y una fila por cada elemento de la lista, sin escribir código para dibujar las filas.

Para el rango mostrado en el estado se calcula:

```csharp
var from = total == 0 ? 0 : (_currentPage - 1) * PageSize + 1;
var to = Math.Min(_currentPage * PageSize, total);
```

Con esto, en la última página de 123 clientes el estado queda `Mostrando 121–123 de 123`. Si la tabla está vacía, queda `Mostrando 0–0 de 0`.

Finalmente, las flechas se deshabilitan en los extremos. En la primera página no se puede retroceder y en la última no se puede avanzar.

## Cómo probarlo

Para verificar que todo funciona como se espera, se siguen estos pasos:

1. Crear una base de datos SQL Server, por ejemplo `PaginationDemo`, y crear `src/PaginationDemo/.env` a partir de `.env.example`.
2. Configurar `PAGINATION_DEMO_SEED=true` para que la aplicación cargue automáticamente los 123 clientes de prueba.
3. Ejecutar la aplicación desde Visual Studio o con este comando desde la terminal:

   ```powershell
   dotnet run --project .\src\PaginationDemo
   ```

4. Confirmar que la grilla muestre 10 filas y que al iniciar el estado diga `Mostrando 41–50 de 123`.
5. Confirmar que al iniciar los números sean `4`, `5` y `6`. Usar `>` y comprobar que pasen a `5`, `6` y `7`, con el estado `Mostrando 51–60 de 123`.
6. Usar `<` y `>` para comprobar que cargan la página anterior y siguiente.
7. Navegar hasta la primera página y verificar que `<` quede deshabilitado. Navegar hasta la última y verificar que `>` quede deshabilitado.

## Cómo queda todo

Al final, la grilla, los cinco botones y el estado quedan sincronizados desde `MainForm`. El repositorio solo trae la página que hace falta, SQL Server recorta el resultado con `OFFSET` y `FETCH NEXT`, y el formulario valida que la página solicitada exista antes de consultarla. La solución no busca ser un componente reutilizable: es un ejemplo directo y completo para estudiar la paginación básica en WinForms.

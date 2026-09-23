# SkiaPagination Demo

Solución WinForms para .NET 10 con un control de paginación reutilizable dibujado por SkiaSharp y una app de ejemplo sobre SQL Server.

## Proyectos

- `SkiaPagination.WinForms`: librería de componentes. Incluye `SkiaPaginator`, `PaginationStyle` y el evento `PageChanged`.
- `SkiaPagination.Demo`: aplicación WinForms con una grilla paginada de clientes.

## Ejecutar

Requiere .NET 10 SDK, SQL Server o LocalDB y una base de datos existente. Por ejemplo, en SQL Server Management Studio:

```sql
CREATE DATABASE PaginationDemo;
GO
```

En PowerShell configurá la cadena de conexión y, opcionalmente, cargá los datos de ejemplo:

```powershell
$env:PAGINATION_DEMO_CONNECTION_STRING = "Server=(localdb)\MSSQLLocalDB;Database=PaginationDemo;Integrated Security=True;TrustServerCertificate=True"
$env:PAGINATION_DEMO_SEED = "true" # solo la primera vez que quieras datos demo
dotnet run --project .\src\SkiaPagination.Demo
```

`PAGINATION_DEMO_CONNECTION_STRING` es obligatoria y debe apuntar a la base SQL Server creada. Al iniciar, la aplicación crea `dbo.Customers` si no existe. `PAGINATION_DEMO_SEED` es opt-in: únicamente con el valor booleano `true` crea los 123 clientes iniciales y no duplica datos si la tabla ya contiene filas.

## Uso del componente

```csharp
paginator.TotalItems = count;
paginator.PageSize = 10;
paginator.PageChanged += (_, e) => CargarPagina(e.Page);
```

El componente calcula automáticamente páginas tentativas desde el `count`; cuando hay muchas páginas muestra el primer/último número y elipsis, y resalta la página activa.

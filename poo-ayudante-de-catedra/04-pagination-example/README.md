# Pagination Demo

Aplicación WinForms para .NET 10 con un control de paginación reutilizable (`Paginator`) y un ejemplo de grilla paginada sobre SQL Server.

## Proyecto

- `Pagination.Demo`: aplicación WinForms con una grilla paginada de clientes. El control `Paginator` vive en `src/Pagination.Demo/Views/UserControls` junto con `PaginationStyle` y `PageChangedEventArgs`, y se compila directamente dentro de esta app (no es una librería separada).

## Ejecutar

Requiere .NET 10 SDK, SQL Server o LocalDB y una base de datos existente. Por ejemplo, en SQL Server Management Studio:

```sql
CREATE DATABASE PaginationDemo;
GO
```

Creá `src/PaginationDemo/.env` a partir de `src/PaginationDemo/.env.example` y completá la conexión a tu base de datos:

```dotenv
PAGINATION_DEMO_CONNECTION_STRING="Server=(localdb)\MSSQLLocalDB;Database=PaginationDemo;Integrated Security=True;TrustServerCertificate=True"
PAGINATION_DEMO_SEED=true
```

Después ejecutá:

```powershell
dotnet run --project .\src\PaginationDemo
```

`PAGINATION_DEMO_CONNECTION_STRING` es obligatoria y debe apuntar a la base SQL Server creada. Al iniciar, la aplicación lee el archivo `.env`, crea la tabla `Clientes` si no existe y no sube ese archivo al repositorio. `PAGINATION_DEMO_SEED` es opt-in: únicamente con el valor booleano `true` crea los 123 clientes iniciales y no duplica datos si la tabla ya contiene filas.

## Uso del componente

```csharp
paginator.TotalItems = count;
paginator.PageSize = 10;
paginator.PageChanged += (_, e) => CargarPagina(e.Page);
```

El componente calcula automáticamente páginas tentativas desde el `count`; cuando hay muchas páginas muestra el primer/último número y elipsis, y resalta la página activa. Es un `UserControl` editable desde el diseñador de Visual Studio: su diseño (flechas `‹`/`›` y el panel donde se agregan los botones de página) está en `Paginator.Designer.cs`, mientras que `Paginator.cs` contiene solo el comportamiento.

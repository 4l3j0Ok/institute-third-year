# Pagination Demo

Aplicación WinForms para .NET 10 con una grilla paginada sobre SQL Server.

## Proyecto

- `Pagination.Demo`: aplicación WinForms con una grilla paginada de clientes.
- La interfaz y la navegación están en `src/PaginationDemo/Views/Forms/MainForm.cs`.

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

## Navegación

El pie del formulario muestra únicamente los botones `<`, `4`, `5`, `6` y `>`, además del estado actual, por ejemplo `Mostrando 41–50 de 123`.

Los botones numéricos cargan esas páginas directamente. Las flechas cargan la página anterior o siguiente y se deshabilitan al llegar al primer o último bloque disponible. No hay un `UserControl` ni lógica para crear botones, elipsis o estilos de paginación dinámicos.

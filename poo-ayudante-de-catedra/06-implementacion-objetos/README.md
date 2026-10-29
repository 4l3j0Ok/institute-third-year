# Cuenta Bancaria — Ejemplo POO en Windows Forms

Proyecto didáctico en **C# / .NET 10 Windows Forms**, listo para abrir desde Visual Studio 2026 (con la carga de trabajo **Desarrollo de escritorio con .NET**).

## Cómo ejecutar

1. Descomprimir el ZIP en Windows.
2. Abrir `CuentaBancariaWinForms.csproj` con Visual Studio 2026.
3. Ejecutar con **F5**. También se puede abrir la carpeta en una terminal Windows y ejecutar `dotnet run` si está instalado el SDK .NET 10.

## Qué explicar

- `Models/CuentaBancaria.cs`: clase, propiedades, constructor, métodos y validaciones.
- `Saldo { get; private set; }`: encapsulamiento. Se puede leer desde el formulario, pero únicamente los métodos de `CuentaBancaria` cambian el saldo.
- `Form1.cs`: instancia de la clase, eventos de botones, manejo de errores y actualización de interfaz.
- `Form1.Designer.cs`: controles del formulario, compatibles con el diseñador de Windows Forms.

## Pruebas recomendadas

- Depositar `500` y verificar el saldo.
- Retirar `1000`.
- Intentar retirar `999999` (saldo insuficiente).
- Depositar `-50`, `0` o texto no numérico.
- Intentar escribir `cuenta.Saldo = -100;` en `Form1.cs` y observar que no compila.

No necesita base de datos ni paquetes NuGet adicionales. El saldo existe solo en memoria durante la ejecución.

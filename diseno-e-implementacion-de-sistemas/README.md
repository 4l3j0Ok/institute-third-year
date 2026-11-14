# Cajero Automático

TP integrador de Diseño e Implementación de Sistemas (ISFT N° 93, 2026).

## Descripción

Simulación de un cajero automático con aspecto de ATM real (pantalla con botones laterales, teclado numérico, ranura de tarjeta, comprobante y salida de efectivo). Permite autenticación con tarjeta y PIN (3 intentos), consulta de saldo, extracción, transferencia y emisión de ticket. Aplicación de escritorio en C# con Windows Forms (.NET 10), en tres capas:

- `src/Cajero/Datos`: `SucursalDB` y `CentralDB` (simulados, datos en memoria).
- `src/Cajero/Logica`: `Operacion` (abstracta), `Consulta`, `Extraccion`, `Transferencia`, `Cliente`, `Tarjeta`, `Cuenta`, `Sesion`.
- `src/Cajero/Presentacion`: `FormCajero` (ventana principal; `FormCajero.cs` + `FormCajero.Designer.cs`).
- `src/Cajero.Tests`: pruebas unitarias (NUnit) de las capas de datos y lógica.

## Integrantes

- (completar, hasta 4 personas)

## Cómo ejecutar

Requiere el [SDK de .NET 10](https://dotnet.microsoft.com/download).

### Windows

Abrir `src/Cajero.slnx` en Visual Studio y ejecutar (F5). El diseñador visual edita `FormCajero` con doble clic sobre `FormCajero.cs`. También desde consola, en `src`:

```
dotnet run --project Cajero
```

### Linux (Wine)

Windows Forms no corre de forma nativa en Linux, así que el ejecutable de Windows se ejecuta con [Wine](https://www.winehq.org). La app se publica autocontenida (trae su propio runtime de .NET), por lo que no hay que instalar nada más dentro de Wine.

```
# Fedora
sudo dnf install wine
# Debian / Ubuntu
sudo apt install wine64
```

Compilar y ejecutar desde `src`:

```
./ejecutar-linux.sh
```

La primera vez Wine crea su prefijo (`~/.local/share/cajero-wine`) y tarda unos 30 segundos en abrir. El ejecutable generado queda en `src/Cajero/bin/wine/Cajero.exe` (~115 MB, incluye el runtime).

### Pruebas

Funcionan en cualquier sistema operativo:

```
dotnet test Cajero.slnx
```

Uso: elegir una tarjeta en el selector, pulsar **Insertar tarjeta** e ingresar el PIN con el teclado. Las opciones se eligen con los botones laterales de la pantalla.

Tarjetas de prueba: válida (PIN `1234`), bloqueada y vencida. CBU destino: 22 dígitos.

## Diagramas

Ver [DIAGRAMS.md](DIAGRAMS.md).

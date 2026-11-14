# Cajero Automático

TP integrador de Diseño e Implementación de Sistemas (ISFT N° 93, 2026).

## Descripción

Simulación de un cajero automático con aspecto de ATM real (pantalla con botones laterales, teclado numérico, ranura de tarjeta, comprobante y salida de efectivo). Permite autenticación con tarjeta y PIN (3 intentos), consulta de saldo, extracción, transferencia y emisión de ticket. Aplicación de escritorio en C# con Windows Forms (.NET 10), en tres capas:

- `src/Cajero/Data`: `BranchDB` (SucursalDB) y `CentralDB` (simulados, datos en memoria).
- `src/Cajero/Logic`: `Operation` (abstracta), `BalanceInquiry`, `CashWithdrawal`, `Transfer`, `Customer`, `Card`, `Account`, `Session`.
- `src/Cajero/Presentation`: `CajeroForm` (ventana principal, el `FormCajero` de la consigna; `CajeroForm.cs` + `CajeroForm.Designer.cs`).
- `src/Cajero.Tests`: pruebas unitarias (NUnit) de las capas de datos y lógica.

Los identificadores del código están en inglés; los textos de pantalla y el vocabulario de análisis (DFD, Casos de Uso, Diccionario de Datos) están en español. Equivalencias con los nombres de la consigna:

| Consigna | Código |
|---|---|
| FormCajero | `CajeroForm` |
| SucursalDB / CentralDB | `BranchDB` / `CentralDB` |
| Operacion, Consulta, Extraccion, Transferencia | `Operation`, `BalanceInquiry`, `CashWithdrawal`, `Transfer` |
| Cliente, Tarjeta, Cuenta, Sesion | `Customer`, `Card`, `Account`, `Session` |
| método `ejecutar()` | `Execute()` |

Glosario completo: `.agents/skills/dis-convenciones/references/glosario.md`.

## Integrantes

- (completar, hasta 4 personas)

## Cómo ejecutar

Requiere el [SDK de .NET 10](https://dotnet.microsoft.com/download).

### Windows

Abrir `src/Cajero.slnx` en Visual Studio y ejecutar (F5). El diseñador visual edita `CajeroForm` con doble clic sobre `CajeroForm.cs`. También desde consola, en `src`:

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

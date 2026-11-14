# Cajero Automático

TP integrador de Diseño e Implementación de Sistemas (ISFT N° 93, 2026).

## Descripción

Simulación de un cajero automático con aspecto de ATM real (pantalla con botones laterales, teclado numérico, ranura de tarjeta, comprobante y salida de efectivo). Permite autenticación con tarjeta y PIN (3 intentos), consulta de saldo, extracción, transferencia y emisión de ticket. Aplicación de escritorio en Python con PySide6, en tres capas:

- `src/cajero/datos`: `SucursalDB` y `CentralDB` (simulados, datos en memoria).
- `src/cajero/logica`: `Operacion` (abstracta), `Consulta`, `Extraccion`, `Transferencia`, `Cliente`, `Tarjeta`, `Cuenta`, `Sesion`.
- `src/cajero/presentacion`: `FormCajero` (ventana principal).

## Integrantes

- (completar, hasta 4 personas)

## Cómo ejecutar

Requiere Python 3.11+ y [uv](https://docs.astral.sh/uv/).

```
uv sync
uv run python -m cajero
```

Uso: elegir una tarjeta en el selector, pulsar **Insertar tarjeta** e ingresar el PIN con el teclado. Las opciones se eligen con los botones laterales de la pantalla.

Tarjetas de prueba: válida (PIN `1234`), bloqueada y vencida. CBU destino: 22 dígitos.

## Diagramas

Ver [DIAGRAMS.md](DIAGRAMS.md).

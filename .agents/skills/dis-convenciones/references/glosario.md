# Glosario español → inglés

Los identificadores del código y de los diagramas de diseño van en inglés. El vocabulario de análisis (flujos, procesos, almacenes, casos de uso, actores, diccionario) y los textos de pantalla siguen en español.

## Clases y tipos

| Consigna / dominio | Identificador | Capa |
|---|---|---|
| Cliente | `Customer` | Logic |
| Tarjeta | `Card` | Logic |
| Cuenta | `Account` | Logic |
| Sesion | `Session` | Logic |
| Operacion | `Operation` | Logic |
| Consulta | `BalanceInquiry` | Logic |
| Extraccion | `CashWithdrawal` | Logic |
| Transferencia | `Transfer` | Logic |
| (cifrado del PIN) | `Encryption` | Logic |
| (formato de dinero) | `MoneyFormatter` | Logic |
| EstadoSesion | `SessionState` (`Started`, `Authenticating`, `Validating`, `Operating`, `Closing`, `Closed`) | Logic |
| EstadoTarjeta | `CardStatus` (`Valid`, `Blocked`, `Expired`) | Data |
| SucursalDB | `BranchDB` | Data |
| CentralDB | `CentralDB` | Data |
| RespuestaSaldo / RespuestaDebito / RespuestaTransferencia | `BalanceResponse` / `DebitResponse` / `TransferResponse` | Data |
| FormCajero (`:PantallaCajero` en las secuencias) | `AtmForm` | Presentation |

## Miembros

| Español | Inglés |
|---|---|
| IngresarTarjeta · Autenticar · Ejecutar · Finalizar · Cerrar | `InsertCard` · `Authenticate` · `Execute` · `Finish` · `Close` |
| NuevaConsulta · NuevaExtraccion · NuevaTransferencia | `NewBalanceInquiry` · `NewCashWithdrawal` · `NewTransfer` |
| GenerarTicket | `GenerateReceipt` |
| ValidarTarjeta · BloquearTarjeta · ValidarPin | `ValidateCard` · `BlockCard` · `ValidatePin` |
| ConsultarSaldo · AutorizarDebito · EjecutarTransferencia | `GetBalance` · `AuthorizeDebit` · `ExecuteTransfer` |
| EstaVigente · RegistrarIntentoFallido · Bloquear · ActualizarSaldo | `IsValid` · `RegisterFailedAttempt` · `Block` · `UpdateBalance` |
| Nombre · Dni · Cbu | `Name` · `NationalId` (DNI) · `Cbu` |
| Numero · Vencimiento · Estado · IntentosPin · MaximoIntentos | `Number` · `ExpirationDate` · `Status` / `State` · `PinAttempts` · `MaximumPinAttempts` |
| SaldoDisponible · SaldoTotal · SaldoRestante | `AvailableBalance` · `TotalBalance` · `RemainingBalance` |
| Monto · Fecha · FechaInicio · Mensaje | `Amount` · `Timestamp` · `StartTime` · `Message` |
| NumeroComprobante (TR) / NumeroTransaccion (TX) | `ReferenceNumber` en `Operation`; `ReceiptNumber` / `TransactionNumber` en las respuestas de Datos |
| CbuDestino · LimiteDiario · Autorizado · Motivo | `DestinationCbu` · `DailyLimit` · `Authorized` · `Reason` |

## Controles de Windows Forms

`btnLateral` → `btnSide`, `lblEtiqueta` → `lblLabel`, `btnDigito` → `btnDigit`, `btnAceptar` → `btnAccept`, `btnCancelar` → `btnCancel`, `btnCorregir` → `btnClear`, `btnTarjeta` → `btnCard`, `cmbTarjeta` → `cmbCard`, `lblMensaje` → `lblMessage`, `lblTicket` → `lblReceipt`, `lblEfectivo` → `lblCash`, `pnlPantalla` → `pnlScreen`, `pnlBisel` → `pnlBezel`.

## Qué no se traduce
Textos de pantalla y de error (`"Saldo insuficiente"`, `"Tarjeta vencida"`), el ticket, los nombres de flujos/procesos/casos de uso de los diagramas de análisis y el diccionario de datos, y las guardas de los diagramas de estado y actividad.

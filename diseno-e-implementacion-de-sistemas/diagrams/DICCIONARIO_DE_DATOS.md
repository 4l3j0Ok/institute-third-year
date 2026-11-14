# Diccionario de Datos - Cajero Automático

| Nombre | Descripción | Composición | Valores permitidos |
|---|---|---|---|
| Datos de tarjeta | Datos leídos de la banda/chip de la tarjeta | Numero_Tarjeta + Vencimiento + Entidad_Emisora | Numero_Tarjeta: 16 dígitos; Vencimiento: MM/AAAA; Entidad_Emisora: texto |
| Estado de tarjeta | Resultado de la validación de la Sucursal | Estado | `válida`, `bloqueada`, `vencida` |
| Solicitud de validación de tarjeta | Pedido del cajero a la Sucursal | Datos de tarjeta | - |
| PIN | Clave ingresada por el cliente | Digitos_PIN | 4 dígitos numéricos |
| Solicitud de validación de PIN | Pedido a Red Central | Numero_Tarjeta + PIN_Encriptado | PIN_Encriptado: cadena cifrada |
| Confirmación de PIN | Respuesta de Red Central | Resultado_PIN | `correcto`, `incorrecto` |
| Solicitud de PIN | Mensaje al cliente para ingresar la clave | Mensaje | texto |
| Menú de operaciones | Opciones disponibles | Opcion* | `Consultar saldo`, `Extraer efectivo`, `Transferir`, `Finalizar` |
| Opción elegida | Selección del cliente | Opcion | una del menú |
| Monto | Importe de la operación | Importe | decimal > 0, múltiplo de 100 para extracciones |
| CBU destino | Cuenta destino de transferencia | CBU | 22 dígitos |
| Confirmación | Aceptación del cliente | Respuesta | `sí`, `no` |
| Solicitud de consulta de saldo | Pedido a Red Central | Numero_Tarjeta | - |
| Saldo disponible | Saldo consultado | Saldo_Disponible + Saldo_Total + Fecha_Hora | decimales >= 0 |
| Saldo consultado | Saldo mostrado al cliente | Saldo_Disponible + Saldo_Total + Fecha_Hora | decimales >= 0 |
| Instrucción de débito | Pedido de débito | Numero_Tarjeta + Monto | - |
| Autorización de débito | Respuesta de Red Central | Resultado + Nro_Transaccion + Saldo_Restante | Resultado: `autorizado`, `rechazado` |
| Efectivo dispensado | Billetes entregados | Monto | = monto autorizado |
| Instrucción de transferencia | Pedido de transferencia | Numero_Tarjeta + CBU_Destino + Monto | - |
| Confirmación de transferencia | Respuesta de Red Central | Resultado + Nro_Comprobante | Resultado: `ok`, `error` |
| Número de comprobante | Identificador de la transacción | Nro_Comprobante | alfanumérico, 10 caracteres |
| Comprobante transferencia | Resumen mostrado al cliente | Cuenta_Origen + CBU_Destino + Monto + Nro_Comprobante | - |
| Ticket impreso | Detalle de la sesión | Operacion* (Tipo + Monto + Hora + Nro_Comprobante) | - |
| Tarjeta devuelta | Devolución física de la tarjeta | Tarjeta | - |
| **D1 Sesión** | Almacén de la sesión activa | Estado + Tarjeta validada + Fecha_Inicio | Estado: Iniciada, Autenticando, Validando, Operando, Cerrando, Cerrada |
| **D2 Tarjetas leídas** | Almacén temporal de la tarjeta en curso | Datos de tarjeta | se borra al cerrar sesión |
| **D3 Log de transacciones** | Operaciones de la sesión | Tipo + Monto + Hora + Nro_Comprobante | Tipo: consulta, extracción, transferencia |

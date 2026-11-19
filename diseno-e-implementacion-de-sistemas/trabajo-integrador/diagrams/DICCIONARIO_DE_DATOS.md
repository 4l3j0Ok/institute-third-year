# Diccionario de Datos - Cajero Automático


## Flujos de datos

| Nombre | Descripción | Composición | Valores permitidos |
|---|---|---|---|
| Datos de tarjeta | Datos que el cajero lee de la banda o del chip al insertar la tarjeta | Datos de tarjeta = Número de tarjeta + Vencimiento + Entidad emisora | Ver cada elemento |
| Datos de tarjeta leídos | Copia en memoria de los datos de tarjeta durante la sesión | Datos de tarjeta leídos = Datos de tarjeta | Los de Datos de tarjeta |
| Solicitud de validación de tarjeta | Pedido del cajero a la Sucursal para validar la tarjeta | Solicitud de validación de tarjeta = Número de tarjeta + Vencimiento | Ver cada elemento |
| Estado de tarjeta | Resultado de la validación hecha por la Sucursal | Estado de tarjeta = [ "Válida" \| "Bloqueada" \| "Vencida" ] | Una de las tres opciones |
| Tarjeta validada | Tarjeta aceptada por la Sucursal, guardada en la sesión | Tarjeta validada = Número de tarjeta + Estado de tarjeta | Estado de tarjeta = Válida |
| Solicitud de PIN | Mensaje en pantalla que pide la clave al cliente | Solicitud de PIN = Mensaje de pantalla | Mensaje de pantalla = "INGRESE SU PIN" |
| PIN | Clave secreta que el cliente tipea en el teclado numérico | PIN = 4{Dígito}4 | 4 dígitos numéricos |
| Solicitud de validación de PIN | Pedido a Red Central para verificar la clave contra la cuenta | Solicitud de validación de PIN = Número de tarjeta + PIN | Ver cada elemento |
| Confirmación de PIN | Respuesta de Red Central sobre la clave | Confirmación de PIN = [ "Correcto" \| "Incorrecto" ] | Una de las dos opciones |
| Sesión autenticada | Marca de que el cliente acreditó su identidad | Sesión autenticada = Número de tarjeta + Fecha y hora de inicio + Estado de sesión | Estado de sesión = Operando |
| Menú de operaciones | Opciones que el cajero muestra al cliente autenticado | Menú de operaciones = 1{Opción de menú}4 | Hasta 4 opciones |
| Opción elegida | Opción que el cliente selecciona en el menú | Opción elegida = Opción de menú | Una opción del menú |
| Datos de sesión | Datos de la sesión activa que usan las operaciones y el ticket | Datos de sesión = Número de tarjeta + CBU + Fecha y hora de inicio | Ver cada elemento |
| Solicitud de consulta de saldo | Pedido a Red Central de los saldos de la cuenta | Solicitud de consulta de saldo = Número de tarjeta | Ver Número de tarjeta |
| Saldo disponible | Saldos que informa Red Central | Saldo disponible = Importe disponible + Importe total | Importes >= 0 |
| Saldo consultado | Saldos que el cajero muestra al cliente | Saldo consultado = Importe disponible + Importe total + Fecha y hora | Importes >= 0 |
| Monto | Importe a extraer o transferir que ingresa el cliente | Monto = Importe | Importe > 0 |
| Instrucción de débito | Pedido a Red Central de verificar saldo y autorizar el débito | Instrucción de débito = Número de tarjeta + Monto | Ver cada elemento |
| Autorización de débito | Respuesta de Red Central a la instrucción de débito | Autorización de débito = Resultado + (Número de transacción) + Importe restante | Resultado = [ Autorizado \| Rechazado ]; Número de transacción solo si es Autorizado |
| Efectivo dispensado | Billetes entregados por el cajero | Efectivo dispensado = Monto | Igual al monto autorizado |
| Datos de extracción | Datos de la extracción para el registro | Datos de extracción = Tipo de operación + Monto + Número de transacción + Fecha y hora | Tipo de operación = Extracción |
| CBU destino | Cuenta destino que ingresa el cliente | CBU destino = CBU | 22 dígitos |
| Confirmación | Respuesta del cliente a una pregunta del cajero (confirmar transferencia, desear comprobante) | Confirmación = [ "Sí" \| "No" ] | Una de las dos opciones |
| Instrucción de transferencia | Pedido a Red Central de ejecutar la transferencia | Instrucción de transferencia = Número de tarjeta + CBU destino + Monto | Ver cada elemento |
| Confirmación de transferencia | Respuesta de Red Central a la instrucción | Confirmación de transferencia = Resultado de transferencia | Resultado de transferencia = [ Ejecutada \| Rechazada ] |
| Número de comprobante | Identificador de la transferencia que asigna Red Central | Número de comprobante = "TR" + 8{Dígito}8 | 10 caracteres |
| Comprobante transferencia | Resumen que el cajero muestra al cliente | Comprobante transferencia = Cuenta origen + CBU destino + Monto + Número de comprobante + Fecha y hora | Ver cada elemento |
| Datos de consulta | Datos de la consulta para el registro | Datos de consulta = Tipo de operación + Fecha y hora | Tipo de operación = Consulta de saldo |
| Datos de transferencia | Datos de la transferencia para el registro | Datos de transferencia = Tipo de operación + Monto + Número de comprobante + Fecha y hora | Tipo de operación = Transferencia |
| Transacción registrada | Operación completada que se guarda en el log de la sesión | Transacción registrada = Tipo de operación + Monto + Fecha y hora + ([ Número de transacción \| Número de comprobante ]) | Ver cada elemento |
| Transacciones de la sesión | Todas las operaciones completadas durante la sesión | Transacciones de la sesión = {Transacción registrada} | Cero o más transacciones |
| Ticket impreso | Comprobante en papel con el detalle de la sesión | Ticket impreso = Encabezado + Transacciones de la sesión | Encabezado = "COMPROBANTE" |
| Tarjeta devuelta | Devolución física de la tarjeta al cliente | Tarjeta devuelta = Número de tarjeta | Ver Número de tarjeta |

## Almacenes

| Nombre | Descripción | Composición | Valores permitidos |
|---|---|---|---|
| **D1 Sesiones** | Datos de la sesión activa; se eliminan al cerrar la sesión | D1 Sesiones = Número de tarjeta + Estado de tarjeta + CBU + Estado de sesión + Fecha y hora de inicio | Una sola sesión activa por cajero |
| **D2 Tarjetas leídas** | Tarjeta en curso, leída por el cajero | D2 Tarjetas leídas = Datos de tarjeta | Se borra al cerrar la sesión |
| **D3 Transacciones** | Log de operaciones de la sesión | D3 Transacciones = {Transacción registrada} | Se borra al cerrar la sesión |

## Elementos de datos

| Nombre | Descripción | Composición | Valores permitidos |
|---|---|---|---|
| Dígito | Carácter numérico | Dígito = [ 0 \| 1 \| 2 \| 3 \| 4 \| 5 \| 6 \| 7 \| 8 \| 9 ] | 0 a 9 |
| Número de tarjeta | Identificador de 16 dígitos de la tarjeta | Número de tarjeta = 16{Dígito}16 | 16 dígitos |
| Vencimiento | Mes y año hasta los que vale la tarjeta | Vencimiento = Mes + Año | Una tarjeta con vencimiento anterior a hoy está vencida |
| Mes | Mes del año | Mes = 1{Dígito}2 | 1 a 12 |
| Año | Año calendario | Año = 4{Dígito}4 | 4 dígitos |
| Entidad emisora | Banco que emitió la tarjeta | Entidad emisora = {Carácter legal} | Texto |
| Carácter legal | Letra, número o espacio permitido en un texto | Carácter legal = [ Letra \| Dígito \| Espacio ] | Letras sin tildes, dígitos y espacio |
| Letra | Letra del alfabeto sin tildes | Letra = [ A-Z \| a-z ] | A a Z, mayúscula o minúscula |
| Espacio | Separador entre palabras | Espacio = * carácter de espacio * | Un espacio |
| Mensaje de pantalla | Texto que el cajero muestra al cliente | Mensaje de pantalla = {Carácter legal} | Texto |
| Opción de menú | Operación que ofrece el cajero | Opción de menú = [ "Consultar saldo" \| "Extraer efectivo" \| "Transferir" \| "Finalizar" ] | Una de las cuatro |
| Tipo de operación | Clase de operación realizada | Tipo de operación = [ "Consulta de saldo" \| "Extracción" \| "Transferencia" ] | Una de las tres |
| Importe | Cantidad de dinero en pesos | Importe = * pesos, 2 decimales * | Para una extracción: mayor que 0, no supera el saldo ni el límite diario de $100.000 |
| Importe disponible | Parte del saldo que se puede usar | Importe disponible = Importe | >= 0 |
| Importe total | Saldo total de la cuenta | Importe total = Importe | >= 0 |
| Importe restante | Saldo disponible luego del débito | Importe restante = Importe | >= 0 |
| Encabezado | Título del ticket | Encabezado = {Carácter legal} | "COMPROBANTE" |
| Resultado de transferencia | Veredicto de Red Central sobre una transferencia | Resultado de transferencia = [ "Ejecutada" \| "Rechazada" ] | Una de las dos |
| Resultado | Veredicto de una autorización | Resultado = [ "Autorizado" \| "Rechazado" ] | Una de las dos |
| CBU | Clave Bancaria Uniforme de una cuenta | CBU = 22{Dígito}22 | 22 dígitos |
| Cuenta origen | Cuenta del cliente desde la que se transfiere | Cuenta origen = CBU | 22 dígitos |
| Número de transacción | Identificador de una extracción asignado por Red Central | Número de transacción = "TX" + 8{Dígito}8 | 10 caracteres |
| Estado de sesión | Etapa del ciclo de vida de la sesión | Estado de sesión = [ "Iniciada" \| "Autenticando" \| "Validando" \| "Operando" \| "Cerrando" \| "Cerrada" ] | Los estados del [diagrama de estado](07_estado_sesion.puml) |
| Fecha y hora | Momento de un hecho | Fecha y hora = Día + Mes + Año + Hora + Minuto | dd/MM/aaaa HH:mm |
| Fecha y hora de inicio | Momento en que empezó la sesión | Fecha y hora de inicio = Fecha y hora | dd/MM/aaaa HH:mm |
| Día | Día del mes | Día = 1{Dígito}2 | 1 a 31 |
| Hora | Hora del día | Hora = 1{Dígito}2 | 0 a 23 |
| Minuto | Minuto de la hora | Minuto = 1{Dígito}2 | 0 a 59 |

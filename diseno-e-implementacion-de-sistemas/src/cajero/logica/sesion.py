from datetime import date
from enum import Enum

from cajero.datos import CentralDB, EstadoTarjeta, SucursalDB

from .cliente import Cliente
from .cuenta import Cuenta
from .operacion import Consulta, Extraccion, Operacion, Transferencia
from .tarjeta import Tarjeta


class EstadoSesion(Enum):
    INICIADA = "Iniciada"
    AUTENTICANDO = "Autenticando"
    VALIDANDO = "Validando"
    OPERANDO = "Operando"
    CERRANDO = "Cerrando"
    CERRADA = "Cerrada"


class Sesion:
    def __init__(self) -> None:
        self._sucursal = SucursalDB()
        self._central = CentralDB()
        self._operaciones: list[Operacion] = []
        self._estado = EstadoSesion.INICIADA
        self._tarjeta: Tarjeta | None = None
        self._cuenta = Cuenta("0170099920000012345678", 150_000.0, 150_000.0)
        self._cliente = Cliente("Cliente Demo", "30111222")
        self._mensaje = ""

    @property
    def estado(self) -> EstadoSesion:
        return self._estado

    @property
    def tarjeta(self) -> Tarjeta | None:
        return self._tarjeta

    @property
    def cuenta(self) -> Cuenta:
        return self._cuenta

    @property
    def cliente(self) -> Cliente:
        return self._cliente

    @property
    def operaciones(self) -> list[Operacion]:
        return list(self._operaciones)

    @property
    def mensaje(self) -> str:
        return self._mensaje

    def ingresar_tarjeta(self, numero: str, vencimiento: date) -> bool:
        """Iniciada -> Autenticando -> Validando (tarjeta válida) o Cerrando (inválida)."""
        self._estado = EstadoSesion.AUTENTICANDO
        self._tarjeta = Tarjeta(numero, vencimiento)
        estado = self._sucursal.validar_tarjeta(numero, vencimiento)
        self._tarjeta.estado = estado
        if estado != EstadoTarjeta.VALIDA:
            self._mensaje = "Tarjeta vencida" if estado == EstadoTarjeta.VENCIDA else "Tarjeta bloqueada"
            self._estado = EstadoSesion.CERRANDO
            return False
        self._estado = EstadoSesion.VALIDANDO
        return True

    def autenticar(self, pin: str) -> bool:
        """Validando -> Operando (PIN ok) / Validando (reintento) / Cerrando (3 intentos)."""
        if self._tarjeta is None:
            return False
        if self._central.validar_pin(self._tarjeta.numero, pin):
            self._estado = EstadoSesion.OPERANDO
            return True
        intentos = self._tarjeta.registrar_intento_fallido()
        if intentos >= Tarjeta.MAX_INTENTOS:
            self._sucursal.bloquear_tarjeta(self._tarjeta.numero)
            self._mensaje = "PIN incorrecto 3 veces\nTarjeta bloqueada"
            self._estado = EstadoSesion.CERRANDO
        else:
            self._mensaje = f"PIN incorrecto\nIntentos restantes: {Tarjeta.MAX_INTENTOS - intentos}"
        return False

    def ejecutar(self, operacion: Operacion) -> bool:
        ok = operacion.ejecutar()
        self._mensaje = operacion.mensaje
        if ok:
            self._operaciones.append(operacion)
        return ok

    def nueva_consulta(self) -> Operacion:
        return Consulta(self._central, self._tarjeta, self._cuenta)

    def nueva_extraccion(self, monto: float) -> Operacion:
        return Extraccion(self._central, self._tarjeta, self._cuenta, monto)

    def nueva_transferencia(self, cbu: str, monto: float) -> Operacion:
        return Transferencia(self._central, self._tarjeta, self._cuenta, cbu, monto)

    def finalizar(self) -> None:
        self._estado = EstadoSesion.CERRANDO

    def generar_ticket(self) -> str:
        lineas = ["*** BANCO ISFT 93 ***", "COMPROBANTE", ""]
        lineas += [str(op) for op in self._operaciones] or ["Sin operaciones"]
        return "\n".join(lineas)

    def cerrar(self) -> None:
        self._tarjeta = None
        self._operaciones.clear()
        self._estado = EstadoSesion.CERRADA

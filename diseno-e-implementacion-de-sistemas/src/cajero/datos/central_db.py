import random
from dataclasses import dataclass


@dataclass(frozen=True)
class RespuestaDebito:
    autorizado: bool
    nro_transaccion: str
    saldo_restante: float
    motivo: str = ""


@dataclass(frozen=True)
class RespuestaSaldo:
    disponible: float
    total: float


@dataclass(frozen=True)
class RespuestaTransferencia:
    ok: bool
    nro_comprobante: str
    motivo: str = ""


class CentralDB:
    """Simula la comunicación con la Red Central (datos en memoria)."""

    LIMITE_DIARIO = 100_000.0

    def __init__(self) -> None:
        self._pines = {
            "1234567890123456": "1234",
            "5555666677778888": "0000",
            "1111222233334444": "0000",
        }
        self._saldo = 150_000.0
        self._extraido_hoy = 0.0

    def validar_pin(self, numero_tarjeta: str, pin: str) -> bool:
        return self._pines.get(numero_tarjeta) == pin

    def consultar_saldo(self, numero_tarjeta: str) -> RespuestaSaldo:
        return RespuestaSaldo(self._saldo, self._saldo)

    def autorizar_debito(self, numero_tarjeta: str, monto: float) -> RespuestaDebito:
        if monto <= 0:
            return RespuestaDebito(False, "", self._saldo, "Monto inválido")
        if monto > self._saldo:
            return RespuestaDebito(False, "", self._saldo, "Saldo insuficiente")
        if self._extraido_hoy + monto > self.LIMITE_DIARIO:
            return RespuestaDebito(False, "", self._saldo, "Supera el límite diario")
        self._saldo -= monto
        self._extraido_hoy += monto
        return RespuestaDebito(True, self._nuevo_numero("TX"), self._saldo)

    def ejecutar_transferencia(self, numero_tarjeta: str, cbu_destino: str, monto: float) -> RespuestaTransferencia:
        if len(cbu_destino) != 22 or not cbu_destino.isdigit():
            return RespuestaTransferencia(False, "", "CBU inválido")
        if monto <= 0:
            return RespuestaTransferencia(False, "", "Monto inválido")
        if monto > self._saldo:
            return RespuestaTransferencia(False, "", "Saldo insuficiente")
        self._saldo -= monto
        return RespuestaTransferencia(True, self._nuevo_numero("TR"))

    @staticmethod
    def _nuevo_numero(prefijo: str) -> str:
        return f"{prefijo}{random.randint(10_000_000, 99_999_999)}"

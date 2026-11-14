from datetime import date

from cajero.datos import EstadoTarjeta


class Tarjeta:
    MAX_INTENTOS = 3

    def __init__(self, numero: str, vencimiento: date) -> None:
        self._numero = numero
        self._vencimiento = vencimiento
        self._estado = EstadoTarjeta.VALIDA
        self._intentos_pin = 0

    @property
    def numero(self) -> str:
        return self._numero

    @property
    def vencimiento(self) -> date:
        return self._vencimiento

    @property
    def estado(self) -> EstadoTarjeta:
        return self._estado

    @estado.setter
    def estado(self, valor: EstadoTarjeta) -> None:
        self._estado = valor

    @property
    def intentos_pin(self) -> int:
        return self._intentos_pin

    def esta_vigente(self) -> bool:
        return self._estado == EstadoTarjeta.VALIDA and self._vencimiento >= date.today()

    def registrar_intento_fallido(self) -> int:
        self._intentos_pin += 1
        if self._intentos_pin >= self.MAX_INTENTOS:
            self.bloquear()
        return self._intentos_pin

    def bloquear(self) -> None:
        self._estado = EstadoTarjeta.BLOQUEADA

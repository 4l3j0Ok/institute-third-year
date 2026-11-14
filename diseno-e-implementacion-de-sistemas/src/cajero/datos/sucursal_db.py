from datetime import date
from enum import Enum


class EstadoTarjeta(Enum):
    VALIDA = "válida"
    BLOQUEADA = "bloqueada"
    VENCIDA = "vencida"


class SucursalDB:
    """Simula la comunicación con la Sucursal."""

    def __init__(self) -> None:
        self._tarjetas = {
            "1234567890123456": EstadoTarjeta.VALIDA,
            "1111222233334444": EstadoTarjeta.BLOQUEADA,
            "5555666677778888": EstadoTarjeta.VENCIDA,
        }

    def validar_tarjeta(self, numero: str, vencimiento: date) -> EstadoTarjeta:
        estado = self._tarjetas.get(numero)
        if estado is None:
            return EstadoTarjeta.BLOQUEADA
        if vencimiento < date.today():
            return EstadoTarjeta.VENCIDA
        return estado

    def bloquear_tarjeta(self, numero: str) -> None:
        self._tarjetas[numero] = EstadoTarjeta.BLOQUEADA

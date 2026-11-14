from abc import ABC, abstractmethod
from datetime import datetime

from cajero.datos import CentralDB

from .cuenta import Cuenta
from .tarjeta import Tarjeta


def formato_dinero(valor: float) -> str:
    return "$" + f"{valor:,.2f}".replace(",", "X").replace(".", ",").replace("X", ".")


class Operacion(ABC):
    def __init__(self, central: CentralDB, tarjeta: Tarjeta, cuenta: Cuenta, monto: float = 0.0) -> None:
        self._central = central
        self._tarjeta = tarjeta
        self._cuenta = cuenta
        self._monto = monto
        self._fecha = datetime.now()
        self._nro_comprobante = ""
        self._mensaje = ""

    @property
    def monto(self) -> float:
        return self._monto

    @monto.setter
    def monto(self, valor: float) -> None:
        self._monto = valor

    @property
    def fecha(self) -> datetime:
        return self._fecha

    @property
    def nro_comprobante(self) -> str:
        return self._nro_comprobante

    @property
    def mensaje(self) -> str:
        return self._mensaje

    @property
    @abstractmethod
    def tipo(self) -> str: ...

    @abstractmethod
    def ejecutar(self) -> bool: ...

    def __str__(self) -> str:
        return f"{self._fecha:%H:%M} {self.tipo} {formato_dinero(self._monto)} {self._nro_comprobante}".strip()


class Consulta(Operacion):
    @property
    def tipo(self) -> str:
        return "Consulta de saldo"

    def ejecutar(self) -> bool:
        r = self._central.consultar_saldo(self._tarjeta.numero)
        self._cuenta.actualizar_saldo(r.disponible, r.total)
        self._mensaje = (
            f"Saldo disponible: {formato_dinero(r.disponible)}\n"
            f"Saldo total: {formato_dinero(r.total)}\n"
            f"{datetime.now():%d/%m/%Y %H:%M}"
        )
        return True


class Extraccion(Operacion):
    @property
    def tipo(self) -> str:
        return "Extracción"

    def ejecutar(self) -> bool:
        r = self._central.autorizar_debito(self._tarjeta.numero, self._monto)
        if not r.autorizado:
            self._mensaje = r.motivo
            return False
        self._nro_comprobante = r.nro_transaccion
        self._cuenta.actualizar_saldo(r.saldo_restante, r.saldo_restante)
        self._mensaje = (
            f"Operación exitosa\nEfectivo dispensado: {formato_dinero(self._monto)}\n"
            f"Saldo restante: {formato_dinero(r.saldo_restante)}"
        )
        return True


class Transferencia(Operacion):
    def __init__(self, central: CentralDB, tarjeta: Tarjeta, cuenta: Cuenta, cbu_destino: str, monto: float) -> None:
        super().__init__(central, tarjeta, cuenta, monto)
        self._cbu_destino = cbu_destino

    @property
    def cbu_destino(self) -> str:
        return self._cbu_destino

    @cbu_destino.setter
    def cbu_destino(self, valor: str) -> None:
        self._cbu_destino = valor

    @property
    def tipo(self) -> str:
        return "Transferencia"

    def ejecutar(self) -> bool:
        r = self._central.ejecutar_transferencia(self._tarjeta.numero, self._cbu_destino, self._monto)
        if not r.ok:
            self._mensaje = r.motivo
            return False
        self._nro_comprobante = r.nro_comprobante
        self._cuenta.actualizar_saldo(
            self._cuenta.saldo_disponible - self._monto, self._cuenta.saldo_total - self._monto
        )
        self._mensaje = (
            f"Transferencia realizada\nDestino: {self._cbu_destino}\n"
            f"Monto: {formato_dinero(self._monto)}\nComprobante: {self._nro_comprobante}"
        )
        return True

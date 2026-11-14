class Cuenta:
    def __init__(self, cbu: str, saldo_disponible: float, saldo_total: float) -> None:
        self._cbu = cbu
        self._saldo_disponible = saldo_disponible
        self._saldo_total = saldo_total

    @property
    def cbu(self) -> str:
        return self._cbu

    @property
    def saldo_disponible(self) -> float:
        return self._saldo_disponible

    @saldo_disponible.setter
    def saldo_disponible(self, valor: float) -> None:
        self._saldo_disponible = valor

    @property
    def saldo_total(self) -> float:
        return self._saldo_total

    @saldo_total.setter
    def saldo_total(self, valor: float) -> None:
        self._saldo_total = valor

    def actualizar_saldo(self, disponible: float, total: float) -> None:
        self._saldo_disponible = disponible
        self._saldo_total = total

class Cliente:
    def __init__(self, nombre: str, dni: str) -> None:
        self._nombre = nombre
        self._dni = dni

    @property
    def nombre(self) -> str:
        return self._nombre

    @nombre.setter
    def nombre(self, valor: str) -> None:
        self._nombre = valor

    @property
    def dni(self) -> str:
        return self._dni

    @dni.setter
    def dni(self, valor: str) -> None:
        self._dni = valor

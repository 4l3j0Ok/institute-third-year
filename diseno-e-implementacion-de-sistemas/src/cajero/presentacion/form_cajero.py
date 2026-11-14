from collections.abc import Callable
from datetime import date

from PySide6.QtCore import Qt
from PySide6.QtWidgets import (
    QComboBox, QFrame, QGridLayout, QHBoxLayout, QLabel, QMainWindow, QPushButton,
    QVBoxLayout, QWidget,
)

from cajero.logica import EstadoSesion, Sesion, formato_dinero

from .estilos import ESTILO

TARJETAS = [
    ("Tarjeta válida (PIN 1234)", "1234567890123456", date(2030, 12, 31)),
    ("Tarjeta bloqueada", "1111222233334444", date(2030, 12, 31)),
    ("Tarjeta vencida", "5555666677778888", date(2020, 1, 31)),
]
ALTO_FILA = 48
ALTO_MENSAJE = 190
Opcion = tuple[str, Callable[[], None]]


class FormCajero(QMainWindow):
    def __init__(self) -> None:
        super().__init__()
        self.setWindowTitle("Cajero Automático - Banco ISFT 93")
        self.setMinimumSize(540, 860)
        self._sesion = Sesion()
        self._acciones: list[Callable[[], None] | None] = [None] * 8
        self._entrada: dict | None = None
        self._cbu = ""
        self._monto = 0.0
        self._construir()
        self.setStyleSheet(ESTILO)
        self._ir_inicio()

    def _construir(self) -> None:
        raiz = QWidget()
        self.setCentralWidget(raiz)
        externo = QVBoxLayout(raiz)
        externo.setContentsMargins(16, 16, 16, 16)
        cuerpo = QFrame(objectName="cuerpo")
        externo.addWidget(cuerpo)
        lay = QVBoxLayout(cuerpo)
        lay.setContentsMargins(22, 16, 22, 20)
        lay.setSpacing(14)

        marca = QLabel("BANCO ISFT 93", objectName="marca")
        marca.setAlignment(Qt.AlignmentFlag.AlignCenter)
        lay.addWidget(marca)
        lay.addWidget(self._construir_pantalla())

        inferior = QHBoxLayout()
        inferior.setSpacing(18)
        inferior.addWidget(self._construir_teclado(), 3)
        inferior.addLayout(self._construir_ranuras(), 2)
        lay.addLayout(inferior)

    def _construir_pantalla(self) -> QFrame:
        bisel = QFrame(objectName="bisel")
        grid = QHBoxLayout(bisel)
        grid.setContentsMargins(10, 10, 10, 10)
        grid.setSpacing(8)

        self._botones: list[QPushButton] = []
        izq, der = QVBoxLayout(), QVBoxLayout()
        for col in (izq, der):
            col.setSpacing(0)
            col.setContentsMargins(0, 0, 0, 0)
            col.addSpacing(ALTO_MENSAJE)
        for i in range(8):
            b = QPushButton(objectName="botonLateral")
            b.setFixedSize(46, 34)
            b.clicked.connect(lambda _=False, n=i: self._soft(n))
            self._botones.append(b)
            caja = QWidget()
            caja.setFixedHeight(ALTO_FILA)
            h = QHBoxLayout(caja)
            h.setContentsMargins(0, 0, 0, 0)
            h.addWidget(b)
            (izq if i < 4 else der).addWidget(caja)
        grid.addLayout(izq)

        pantalla = QFrame(objectName="pantalla")
        pl = QVBoxLayout(pantalla)
        pl.setContentsMargins(0, 0, 0, 0)
        pl.setSpacing(0)
        self._mensaje = QLabel(objectName="mensaje")
        self._mensaje.setWordWrap(True)
        self._mensaje.setAlignment(Qt.AlignmentFlag.AlignCenter)
        self._mensaje.setFixedHeight(ALTO_MENSAJE)
        pl.addWidget(self._mensaje)
        self._etiquetas: list[QLabel] = []
        for fila in range(4):
            h = QHBoxLayout()
            h.setContentsMargins(0, 0, 0, 0)
            li = QLabel(objectName="opcionIzq")
            ld = QLabel(objectName="opcionDer")
            li.setAlignment(Qt.AlignmentFlag.AlignLeft | Qt.AlignmentFlag.AlignVCenter)
            ld.setAlignment(Qt.AlignmentFlag.AlignRight | Qt.AlignmentFlag.AlignVCenter)
            for lb in (li, ld):
                lb.setFixedHeight(ALTO_FILA)
            h.addWidget(li, 1)
            h.addWidget(ld, 1)
            pl.addLayout(h)
            self._etiquetas.extend([li, ld])
        grid.addWidget(pantalla, 1)
        grid.addLayout(der)
        return bisel

    def _construir_teclado(self) -> QWidget:
        w = QWidget(objectName="teclado")
        g = QGridLayout(w)
        g.setSpacing(8)
        for c in range(3):
            g.setColumnStretch(c, 1)
        for i, d in enumerate("123456789"):
            self._tecla(g, d, i // 3, i % 3)
        self._tecla(g, "BORRAR", 3, 0, "teclaCorregir", self._corregir)
        self._tecla(g, "0", 3, 1)
        self._tecla(g, "ACEPTAR", 3, 2, "teclaAceptar", self._aceptar)
        self._tecla(g, "CANCELAR", 4, 0, "teclaCancelar", self._cancelar, ancho=3)
        return w

    def _tecla(self, g: QGridLayout, texto: str, f: int, c: int, nombre: str = "",
               accion: Callable[[], None] | None = None, ancho: int = 1) -> None:
        b = QPushButton(texto)
        if nombre:
            b.setObjectName(nombre)
        b.clicked.connect(accion or (lambda _=False, d=texto: self._digito(d)))
        g.addWidget(b, f, c, 1, ancho)

    def _construir_ranuras(self) -> QVBoxLayout:
        v = QVBoxLayout()
        v.setSpacing(6)
        v.addWidget(QLabel("TARJETA", objectName="rotulo"))
        self._combo = QComboBox()
        for texto, _, _ in TARJETAS:
            self._combo.addItem(texto)
        v.addWidget(self._combo)
        self._btn_tarjeta = QPushButton("Insertar tarjeta", objectName="botonTarjeta")
        self._btn_tarjeta.clicked.connect(self._tarjeta_click)
        v.addWidget(self._btn_tarjeta)
        v.addWidget(QLabel("COMPROBANTE", objectName="rotulo"))
        self._ticket = QLabel("", objectName="ticket")
        self._ticket.setMinimumHeight(120)
        self._ticket.setWordWrap(True)
        self._ticket.setAlignment(Qt.AlignmentFlag.AlignTop | Qt.AlignmentFlag.AlignLeft)
        v.addWidget(self._ticket)
        v.addWidget(QLabel("SALIDA DE EFECTIVO", objectName="rotulo"))
        self._efectivo = QLabel("", objectName="efectivo")
        self._efectivo.setAlignment(Qt.AlignmentFlag.AlignCenter)
        self._efectivo.setMinimumHeight(34)
        v.addWidget(self._efectivo)
        return v

    # ---------- pantalla ----------
    def _mostrar(self, mensaje: str, opciones: list[Opcion | None] | None = None, entrada: dict | None = None) -> None:
        self._entrada = entrada
        self._mensaje.setText(mensaje)
        opciones = (opciones or []) + [None] * 8
        for i in range(8):
            op = opciones[i]
            self._acciones[i] = op[1] if op else None
            self._botones[i].setEnabled(op is not None)
            texto = op[0] if op else ""
            # etiquetas: fila f -> izquierda índice 2f, derecha 2f+1
            etiqueta = self._etiquetas[(i % 4) * 2 + (0 if i < 4 else 1)]
            etiqueta.setText(("◄ " + texto) if (op and i < 4) else (texto + " ►") if op else "")

    def _soft(self, n: int) -> None:
        if self._acciones[n]:
            self._acciones[n]()

    def _refrescar_entrada(self) -> None:
        e = self._entrada
        buf = e["buffer"]
        visible = "●" * len(buf) if e["oculto"] else buf
        if e.get("dinero") and buf:
            visible = formato_dinero(int(buf))
        self._mensaje.setText(f"{e['titulo']}\n\n{visible or '_'}")

    def _pedir(self, titulo: str, largo: int, aceptar: Callable[[str], None], cancelar: Callable[[], None],
               oculto: bool = False, dinero: bool = False) -> None:
        self._mostrar("", entrada={"titulo": titulo, "largo": largo, "buffer": "", "oculto": oculto,
                                   "dinero": dinero, "aceptar": aceptar, "cancelar": cancelar})
        self._refrescar_entrada()

    # ---------- teclado ----------
    def _digito(self, d: str) -> None:
        e = self._entrada
        if e and len(e["buffer"]) < e["largo"]:
            e["buffer"] += d
            self._refrescar_entrada()

    def _corregir(self) -> None:
        if self._entrada:
            self._entrada["buffer"] = ""
            self._refrescar_entrada()

    def _aceptar(self) -> None:
        if self._entrada:
            self._entrada["aceptar"](self._entrada["buffer"])

    def _cancelar(self) -> None:
        if self._entrada:
            self._entrada["cancelar"]()
        elif self._sesion.estado == EstadoSesion.OPERANDO:
            self._preguntar_ticket()

    # ---------- flujo ----------
    def _ir_inicio(self) -> None:
        self._sesion = Sesion()
        self._ticket.setText("")
        self._efectivo.setText("")
        self._combo.setEnabled(True)
        self._btn_tarjeta.setText("Insertar tarjeta")
        self._btn_tarjeta.setEnabled(True)
        self._mostrar("BIENVENIDO\n\nInserte su tarjeta")

    def _tarjeta_click(self) -> None:
        if self._sesion.estado != EstadoSesion.INICIADA:
            self._ir_inicio()
            return
        _, numero, venc = TARJETAS[self._combo.currentIndex()]
        self._combo.setEnabled(False)
        self._btn_tarjeta.setEnabled(False)
        self._btn_tarjeta.setText("Retirar tarjeta")
        if self._sesion.ingresar_tarjeta(numero, venc):
            self._pedir_pin()
        else:
            self._terminar(self._sesion.mensaje)

    def _pedir_pin(self) -> None:
        self._pedir("INGRESE SU PIN", 4, self._validar_pin, lambda: self._terminar("Operación cancelada"), oculto=True)

    def _validar_pin(self, pin: str) -> None:
        if len(pin) != 4:
            return
        if self._sesion.autenticar(pin):
            self._menu()
        elif self._sesion.estado == EstadoSesion.CERRANDO:
            self._terminar(self._sesion.mensaje)
        else:
            self._mostrar(self._sesion.mensaje, [("Reintentar", self._pedir_pin)])

    def _menu(self) -> None:
        self._mostrar("SELECCIONE UNA OPERACIÓN", [
            ("Consultar saldo", self._consultar), ("Extraer efectivo", self._menu_extraer),
            None, None,
            ("Transferir", self._pedir_cbu), ("Finalizar", self._preguntar_ticket),
        ])

    def _resultado(self, ok: bool, volver: Callable[[], None]) -> None:
        self._mostrar(self._sesion.mensaje if ok else f"ERROR\n\n{self._sesion.mensaje}",
                      [("Otra operación", self._menu)] + [None] * 3 + [("Finalizar", self._preguntar_ticket)]
                      if ok else [("Reintentar", volver), ("Menú", self._menu)])

    def _consultar(self) -> None:
        ok = self._sesion.ejecutar(self._sesion.nueva_consulta())
        self._resultado(ok, self._menu)

    def _menu_extraer(self) -> None:
        montos = [1000, 5000, 10000, 20000]
        ops: list[Opcion | None] = [(formato_dinero(m).split(",")[0], lambda m=m: self._extraer(m)) for m in montos]
        ops += [("Otro monto", self._pedir_monto_extraer), None, None, ("Volver", self._menu)]
        self._mostrar("EXTRAER EFECTIVO\n\nElija el monto", ops)

    def _pedir_monto_extraer(self) -> None:
        self._pedir("MONTO A RETIRAR", 7, lambda t: self._extraer(int(t)) if t else None, self._menu_extraer, dinero=True)

    def _extraer(self, monto: float) -> None:
        ok = self._sesion.ejecutar(self._sesion.nueva_extraccion(float(monto)))
        if ok:
            self._efectivo.setText(f"▼ {formato_dinero(monto)} ▼")
        self._resultado(ok, self._menu_extraer)

    def _pedir_cbu(self) -> None:
        self._pedir("CBU DESTINO (22 dígitos)", 22, self._cbu_ok, self._menu)

    def _cbu_ok(self, cbu: str) -> None:
        if len(cbu) == 22:
            self._cbu = cbu
            self._pedir("MONTO A TRANSFERIR", 7, self._monto_ok, self._menu, dinero=True)

    def _monto_ok(self, texto: str) -> None:
        if not texto or int(texto) <= 0:
            return
        self._monto = float(texto)
        self._mostrar(
            f"CONFIRME LA OPERACIÓN\n\nDestino: {self._cbu[:11]}\n{self._cbu[11:]}\nMonto: {formato_dinero(self._monto)}",
            [("Confirmar", self._transferir), None, None, None, ("Cancelar", self._menu)],
        )
        self._entrada = {"titulo": "", "largo": 0, "buffer": "", "oculto": False, "dinero": False,
                         "aceptar": lambda _: self._transferir(), "cancelar": self._menu}

    def _transferir(self) -> None:
        ok = self._sesion.ejecutar(self._sesion.nueva_transferencia(self._cbu, self._monto))
        self._resultado(ok, self._pedir_cbu)

    def _preguntar_ticket(self) -> None:
        self._sesion.finalizar()
        self._mostrar("¿DESEA COMPROBANTE IMPRESO?", [("Sí", lambda: self._terminar("¡Gracias!", True)),
                      None, None, None, ("No", lambda: self._terminar("¡Gracias!"))])

    def _terminar(self, mensaje: str, con_ticket: bool = False) -> None:
        if con_ticket:
            self._ticket.setText(self._sesion.generar_ticket())
        self._sesion.cerrar()
        self._btn_tarjeta.setEnabled(True)
        self._btn_tarjeta.setText("Retirar tarjeta")
        self._mostrar(f"{mensaje}\n\nRETIRE SU TARJETA")

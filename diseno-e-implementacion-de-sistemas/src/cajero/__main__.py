import sys

from PySide6.QtWidgets import QApplication

from cajero.presentacion import FormCajero


def main() -> int:
    app = QApplication(sys.argv)
    ventana = FormCajero()
    ventana.show()
    return app.exec()


if __name__ == "__main__":
    sys.exit(main())

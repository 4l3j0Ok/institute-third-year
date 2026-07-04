import sys

from PySide6.QtCore import QUrl
from PySide6.QtGui import QFont
from PySide6.QtQml import QQmlApplicationEngine
from PySide6.QtWidgets import QApplication  # QApplication (no QGuiApplication) carga los plugins de estilo de KDE/plasma-integration

from controllers.CalculadoraController import CalculadoraController
from controllers.DispersionController import DispersionController


def main():
    app = QApplication(sys.argv)
    app.setFont(QFont("CaskaydiaCove NFM"))
    engine = QQmlApplicationEngine()

    # Registrar controllers para QML (guardar referencia para evitar GC)
    calculadora_controller = CalculadoraController()
    dispersion_controller = DispersionController()

    engine.rootContext().setContextProperty("calculadoraController", calculadora_controller)
    engine.rootContext().setContextProperty("dispersionController", dispersion_controller)

    # Mantener referencias para evitar garbage collection
    app._calculadora_controller = calculadora_controller
    app._dispersion_controller = dispersion_controller

    engine.load(QUrl.fromLocalFile("qml/Main.qml"))
    if not engine.rootObjects():
        sys.exit(-1)
    sys.exit(app.exec())


if __name__ == "__main__":
    main()

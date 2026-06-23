import sys

from PySide6.QtCore import QUrl
from PySide6.QtGui import QGuiApplication
from PySide6.QtQml import QQmlApplicationEngine

from controller.AppController import AppController


def main():
    app = QGuiApplication(sys.argv)
    engine = QQmlApplicationEngine()

    # Create controller and expose to QML via context
    controller = AppController()
    engine.rootContext().setContextProperty("appController", controller)

    engine.load(QUrl.fromLocalFile("qml/Main.qml"))
    if not engine.rootObjects():
        sys.exit(-1)
    sys.exit(app.exec())


if __name__ == "__main__":
    main()

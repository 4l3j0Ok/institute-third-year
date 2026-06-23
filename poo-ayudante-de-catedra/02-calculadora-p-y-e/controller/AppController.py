from PySide6.QtCore import Property, QObject, Signal, Slot


class AppController(QObject):
    """MVC Controller: manages navigation state between pages."""

    currentIndexChanged = Signal()

    def __init__(self, parent=None):
        super().__init__(parent)
        self._current_index = 0

    # --- Property exposed to QML for two-way binding ---

    def _get_current_index(self) -> int:
        return self._current_index

    def _set_current_index(self, value: int):
        if self._current_index != value:
            self._current_index = value
            self.currentIndexChanged.emit()

    currentIndex = Property(
        int,
        _get_current_index,
        _set_current_index,
        notify=currentIndexChanged,
    )

    @Slot(int)
    def setCurrentIndex(self, index: int):
        """Slot called from QML when a nav button is clicked."""
        self._current_index = index
        self.currentIndexChanged.emit()

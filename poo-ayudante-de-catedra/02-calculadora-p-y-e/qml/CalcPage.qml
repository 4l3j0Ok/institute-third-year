import QtQuick
import QtQuick.Controls
import QtQuick.Layouts

Item {
    ColumnLayout {
        anchors.centerIn: parent
        spacing: 16

        Text {
            text: "🔢"
            font.pixelSize: 48
            Layout.alignment: Qt.AlignHCenter
        }

        Text {
            text: "Calculadora"
            font.pixelSize: 24
            font.bold: true
            color: "#1a1a2e"
            Layout.alignment: Qt.AlignHCenter
        }

        Text {
            text: "Página de cálculos."
            font.pixelSize: 14
            color: "#666"
            Layout.alignment: Qt.AlignHCenter
        }
    }
}

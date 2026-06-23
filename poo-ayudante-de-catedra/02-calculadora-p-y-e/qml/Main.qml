import QtQuick
import QtQuick.Controls
import QtQuick.Layouts

ApplicationWindow {
    id: root
    visible: true
    width: 400
    height: 600
    title: "App MVC"

    ColumnLayout {
        anchors.fill: parent
        spacing: 0

        // ── Top Navigation Bar ──
        Rectangle {
            Layout.fillWidth: true
            Layout.preferredHeight: 50
            color: "#1a1a2e"

            RowLayout {
                anchors.fill: parent
                anchors.margins: 8
                spacing: 4

                Repeater {
                    model: ["Inicio", "Calculadora", "Ajustes"]

                    Button {
                        Layout.fillWidth: true
                        Layout.fillHeight: true
                        text: modelData
                        checkable: true
                        checked: appController.currentIndex === index

                        onClicked: appController.setCurrentIndex(index)

                        background: Rectangle {
                            radius: 8
                            color: parent.checked
                                ? "#e94560"
                                : (parent.hovered ? "#16213e" : "transparent")
                        }

                        contentItem: Text {
                            text: parent.text
                            color: "#ffffff"
                            font.bold: parent.checked
                            font.pixelSize: 14
                            horizontalAlignment: Text.AlignHCenter
                            verticalAlignment: Text.AlignVCenter
                        }
                    }
                }
            }
        }

        // ── Page Content ──
        SwipeView {
            id: swipeView
            Layout.fillWidth: true
            Layout.fillHeight: true
            currentIndex: appController.currentIndex
            interactive: false

            onCurrentIndexChanged: appController.setCurrentIndex(currentIndex)

            HomePage {}
            CalcPage {}
            SettingsPage {}
        }
    }
}

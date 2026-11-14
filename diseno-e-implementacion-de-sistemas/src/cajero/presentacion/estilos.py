ESTILO = """
QMainWindow { background: #1c1f26; }
#cuerpo {
    background: qlineargradient(x1:0,y1:0,x2:0,y2:1, stop:0 #d7dbe2, stop:0.5 #b8bec9, stop:1 #98a0ad);
    border: 3px solid #5d6573; border-radius: 22px;
}
#marca { color: #14305c; font: bold 20px "DejaVu Sans"; letter-spacing: 3px; }
#bisel { background: #20242c; border: 4px solid #4a515e; border-radius: 12px; }
#pantalla { background: #06222b; border: 2px solid #0d4c5b; border-radius: 6px; }
#mensaje { color: #7dffc0; font: bold 17px "DejaVu Sans Mono"; padding: 14px; }
#opcionIzq, #opcionDer { color: #bff6ff; font: bold 14px "DejaVu Sans Mono"; padding: 0 8px; }
#botonLateral {
    background: qlineargradient(x1:0,y1:0,x2:0,y2:1, stop:0 #e9ecf1, stop:1 #8d95a3);
    border: 2px solid #4a515e; border-radius: 6px;
}
#botonLateral:pressed { background: #727b8a; }
#botonLateral:disabled { background: #6a707b; border-color: #3a3f48; }
#teclado QPushButton {
    background: qlineargradient(x1:0,y1:0,x2:0,y2:1, stop:0 #f1f3f6, stop:1 #aab1bd);
    border: 2px solid #444b57; border-radius: 8px; color: #1c1f26;
    font: bold 20px "DejaVu Sans"; min-height: 44px; min-width: 0px; padding: 0px;
}
#teclado QPushButton:pressed { background: #8d95a3; }
#teclado #teclaAceptar { background: #2fa84f !important; color: white !important; font: bold 11px "DejaVu Sans" !important; }
#teclado #teclaCorregir { background: #e6b422 !important; font: bold 11px "DejaVu Sans" !important; }
#teclado #teclaCancelar { background: #d1432f !important; color: white !important; font: bold 11px "DejaVu Sans" !important; }
#ranura, #bandeja { background: #101215; border: 3px solid #3a3f48; border-radius: 6px; color: #8d95a3;
    font: bold 11px "DejaVu Sans"; padding: 4px; }
#rotulo { color: #14305c; font: bold 11px "DejaVu Sans"; }
#botonTarjeta { background: #2a63b8; color: white; border: 2px solid #17376b; border-radius: 6px;
    font: bold 12px "DejaVu Sans"; padding: 6px; }
#botonTarjeta:disabled { background: #6a707b; border-color: #3a3f48; }
QComboBox { background: #f1f3f6; border: 2px solid #444b57; border-radius: 6px; padding: 4px; color: #1c1f26; }
QComboBox QAbstractItemView { background: #f1f3f6; color: #1c1f26; selection-background-color: #2a63b8; }
#ticket { background: #fdfdf6; color: #222; border: 1px solid #aaa; font: 11px "DejaVu Sans Mono"; padding: 6px; }
#efectivo { background: #101215; color: #6dff8a; border: 3px solid #3a3f48; border-radius: 6px;
    font: bold 15px "DejaVu Sans Mono"; padding: 4px; }
"""

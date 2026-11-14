#!/usr/bin/env bash
# Compila el cajero para Windows (autocontenido) y lo ejecuta en Linux con Wine.
set -euo pipefail

cd "$(dirname "$0")"

SALIDA="Atm/bin/wine"
export WINEPREFIX="${WINEPREFIX:-$HOME/.local/share/atm-wine}"
export WINEDEBUG="${WINEDEBUG:--all}"
export WINEDLLOVERRIDES="mscoree=d"   # no ofrecer instalar wine-mono: la app trae su propio runtime

command -v wine >/dev/null || { echo "Falta Wine. Fedora: sudo dnf install wine | Debian/Ubuntu: sudo apt install wine64"; exit 1; }

dotnet publish Atm -c Release -r win-x64 --self-contained \
    -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true \
    -o "$SALIDA" --nologo -v quiet

echo "Iniciando con Wine (la primera vez crea el prefijo y tarda ~30 s)..."
exec wine "$SALIDA/Atm.exe"

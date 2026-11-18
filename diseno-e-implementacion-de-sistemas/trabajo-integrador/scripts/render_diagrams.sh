#!/usr/bin/env bash
# Regenera los PNG de diagrams/*.puml. Uso: scripts/render_diagrams.sh [archivo.puml ...]
set -euo pipefail

cd "$(dirname "$0")/.."

if command -v plantuml >/dev/null; then
    PLANTUML=(plantuml)
else
    JAR=$(ls ~/.vscode/extensions/jebbs.plantuml-*/plantuml.jar 2>/dev/null | tail -n 1 || true)
    [ -n "$JAR" ] || { echo "Falta PlantUML (paquete plantuml o la extensión jebbs.plantuml de VS Code)"; exit 1; }
    PLANTUML=(java -jar "$JAR")
fi
command -v dot >/dev/null || { echo "Falta Graphviz (dot): Contexto y DFD lo necesitan"; exit 1; }

ARCHIVOS=("$@")
[ ${#ARCHIVOS[@]} -gt 0 ] || ARCHIVOS=(diagrams/*.puml)

for archivo in "${ARCHIVOS[@]}"; do
    "${PLANTUML[@]}" -tpng -charset UTF-8 "$archivo"
done
echo "Generados: ${#ARCHIVOS[@]} archivo(s) fuente"

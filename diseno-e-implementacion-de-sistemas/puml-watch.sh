#!/usr/bin/env bash

set -euo pipefail

ROOT_DIR="."
OUTPUT_EXTENSION="png"

usage() {
    cat <<EOF
Usage:
  $(basename "$0") [directory] [--output-extension png|svg]

Examples:
  $(basename "$0")
  $(basename "$0") ./docs
  $(basename "$0") ./docs --output-extension svg

Options:
  --output-extension   Output format: png or svg. Default: png
  -h, --help           Show this help
EOF
}

log() {
    printf '[%s] %s\n' "$(date '+%Y-%m-%d %H:%M:%S')" "$*"
}
render_puml() {
    local file="$1"
    local dir
    local filename
    local basename
    local output

    dir="$(dirname "$file")"
    filename="$(basename "$file")"
    basename="${filename%.puml}"
    output="${dir}/${basename}.${OUTPUT_EXTENSION}"

    if [[ -f "$output" && ! "$file" -nt "$output" ]]; then
        return 0
    fi

    log "Rendering: $file -> $output"

    case "$OUTPUT_EXTENSION" in
        png)
            if ! plantuml -tpng "$file"; then
                log "WARNING: PlantUML failed for: $file"
                return 0
            fi
            ;;
        svg)
            if ! plantuml -tsvg "$file"; then
                log "WARNING: PlantUML failed for: $file"
                return 0
            fi
            ;;
    esac
}

render_all() {
    log "Initial scan: $ROOT_DIR"

    while IFS= read -r -d '' file; do
        render_puml "$file"
    done < <(
        find "$ROOT_DIR" \
            -type f \
            -name '*.puml' \
            -print0
    )
}

cleanup() {
    log "Stopping PlantUML watcher"
    exit 0
}

trap cleanup SIGINT SIGTERM

while [[ $# -gt 0 ]]; do
    case "$1" in
        --output-extension)
            if [[ $# -lt 2 ]]; then
                echo "Error: --output-extension requires a value" >&2
                exit 1
            fi

            OUTPUT_EXTENSION="$2"
            shift 2
            ;;

        --output-extension=*)
            OUTPUT_EXTENSION="${1#*=}"
            shift
            ;;

        -h|--help)
            usage
            exit 0
            ;;

        -*)
            echo "Unknown option: $1" >&2
            usage >&2
            exit 1
            ;;

        *)
            ROOT_DIR="$1"
            shift
            ;;
    esac
done

case "$OUTPUT_EXTENSION" in
    png|svg)
        ;;
    *)
        echo "Invalid output extension: $OUTPUT_EXTENSION" >&2
        echo "Allowed values: png, svg" >&2
        exit 1
        ;;
esac

if ! command -v plantuml >/dev/null 2>&1; then
    echo "Error: plantuml CLI is not installed or not in PATH" >&2
    exit 1
fi

if ! command -v inotifywait >/dev/null 2>&1; then
    echo "Error: inotifywait is required." >&2
    echo "Ubuntu/Debian: sudo apt install inotify-tools" >&2
    echo "Fedora:        sudo dnf install inotify-tools" >&2
    echo "Arch:          sudo pacman -S inotify-tools" >&2
    exit 1
fi

if [[ ! -d "$ROOT_DIR" ]]; then
    echo "Error: directory does not exist: $ROOT_DIR" >&2
    exit 1
fi

ROOT_DIR="$(realpath "$ROOT_DIR")"

render_all

log "Watching recursively: $ROOT_DIR"
log "Output format: .$OUTPUT_EXTENSION"

inotifywait \
    --monitor \
    --recursive \
    --quiet \
    --format '%w%f' \
    --event close_write \
    --event moved_to \
    --event create \
    "$ROOT_DIR" |
while IFS= read -r file; do
    case "$file" in
        *.puml)
            # A create event may happen before the writer has finished.
            # close_write will normally trigger immediately afterwards.
            [[ -f "$file" ]] && render_puml "$file"
            ;;
    esac
done

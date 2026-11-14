PRETASKS.md: Tareas a realizar antes de iniciar el proyecto.
TASKS.md: Tareas a realizar durante el proyecto. Debe generarse una vez completado PRETASKS.md.

Diagramas y código: usar siempre las skills `.agents/skills/dis-*` (empezar por `dis-convenciones`, que fija fuentes, nomenclatura y convenciones de C#). Tras tocar un diagrama: `scripts/render_diagrams.sh`, `python3 scripts/verificar_diagramas.py` y mirar el PNG. Tras tocar código: `dotnet build` y `dotnet test` en `src/` (el build exige las convenciones de nombres).

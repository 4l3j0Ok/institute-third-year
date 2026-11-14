# Repo layout

Colección de trabajos de la carrera (ISFT N° 93), una carpeta por materia. No hay build/CI/lint raíz; cada subproyecto es independiente. Idioma de docs y comentarios: español.

- `diseno-e-implementacion-de-sistemas/`: TP Cajero Automático (PlantUML + C#). Tiene su propio `AGENTS.md` con comandos y reglas; leerlo antes de trabajar ahí y usar las skills `.agents/skills/dis-*`.
- `edi/`: apps Blazor (.NET, `net10.0`) numeradas por unidad. `edi/edi.sln` solo incluye `01-task-manager` y `05-component-communications`; los demás tienen su `.sln` propio. Correr con `dotnet run --project edi/<proyecto>`. `03-basic-page-html-css-js` es HTML/JS estático. `edi/dotnet-notes.md`: la plantilla es `dotnet new blazor -int Server` (no `blazorserver`).
- `stacks/mssql/`: SQL Server + CloudBeaver con docker compose. Crear `.env` con `bash create-env.sh` (pide la contraseña; no commitear `.env`) antes de `docker compose up`.
- `practicas-profesionalizantes/InstitutoNet`: submódulo git (ISFT93/InstitutoNET); está vacío si no se hizo `git submodule update --init`. No editar desde este repo.
- `gestion-de-base-de-datos/`, `poo-ayudante-de-catedra/`, `teleinformatica/`: apuntes, consultas SQL y ejercicios; sin build común.

# Convenciones

- `bin/`, `obj/`, `.vs/`, `.env` están ignorados; no commitearlos.
- Commits: estilo Conventional Commits (`feat:`, `fix:`, `docs:`).

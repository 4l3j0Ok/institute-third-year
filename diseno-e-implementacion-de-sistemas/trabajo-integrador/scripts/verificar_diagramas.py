#!/usr/bin/env python3
"""Verifica los diagramas de diagrams/ contra las reglas de la cátedra (ver skill dis-convenciones).

Uso: python3 scripts/verificar_diagramas.py
Sale con código 1 si hay errores. Las advertencias no cortan.
"""
import re
import sys
from pathlib import Path

RAIZ = Path(__file__).resolve().parent.parent
DIAGRAMAS = RAIZ / "diagrams"
TERMINADORES = {"CLI": "Cliente", "SUC": "Sucursal", "RED": "Red Central"}


def terminador(identificador: str) -> str | None:
    """Los terminadores pueden repetirse en el DFD (CLI, CLI2, ...); se identifican por el prefijo."""
    prefijo = re.match(r"^([A-Z]{3})\d*$", identificador)
    return TERMINADORES.get(prefijo.group(1)) if prefijo else None

errores: list[str] = []
avisos: list[str] = []


def error(diagrama: str, mensaje: str) -> None:
    errores.append(f"[{diagrama}] {mensaje}")


def aviso(diagrama: str, mensaje: str) -> None:
    avisos.append(f"[{diagrama}] {mensaje}")


def leer(nombre: str) -> str:
    return (DIAGRAMAS / nombre).read_text(encoding="utf-8")


def normalizar(texto: str) -> str:
    return re.sub(r"\s+", " ", texto.replace("\\n", " ")).strip()


# ---------- Contexto y DFD (DOT dentro de @startdot) ----------

def parsear_dot(fuente: str):
    nodos: dict[str, dict] = {}
    flujos: list[tuple[str, str, str]] = []
    for linea in fuente.splitlines():
        arista = re.match(r'\s*(\w+)\s*->\s*(\w+)\s*\[(.*)\]\s*;?\s*$', linea)
        if arista:
            etiqueta = re.search(r'label="([^"]*)"', arista.group(3))
            flujos.append((arista.group(1), arista.group(2), normalizar(etiqueta.group(1)) if etiqueta else ""))
            continue
        nodo = re.match(r'\s*(\w+)\s*\[(.*)\]\s*;?\s*$', linea)
        if nodo and nodo.group(1) not in {"node", "edge", "graph"}:
            atributos = nodo.group(2)
            forma = re.search(r'shape=(\w+)', atributos)
            html = re.findall(r'<TD[^>]*>([^<]*)</TD>', atributos)
            texto = re.search(r'label="([^"]*)"', atributos)
            nodos[nodo.group(1)] = {
                "forma": forma.group(1) if forma else "",
                "texto": normalizar(" ".join(html)) if html else normalizar(texto.group(1)) if texto else "",
            }
    return nodos, flujos


def verificar_contexto_y_dfd() -> tuple[set[str], set[str]]:
    nodos_c, flujos_c = parsear_dot(leer("01_contexto.puml"))
    nodos_d, flujos_d = parsear_dot(leer("02_dfd_nivel0.puml"))

    procesos_c = [n for n, v in nodos_c.items() if v["forma"] == "circle"]
    if len(procesos_c) != 1:
        error("Contexto", f"debe tener una sola burbuja y tiene {len(procesos_c)}")
    nombres_c = {terminador(n) for n in nodos_c if terminador(n)}
    if nombres_c != set(TERMINADORES.values()):
        error("Contexto", f"terminadores esperados {sorted(TERMINADORES.values())}, hay {sorted(nombres_c)}")
    if any(n for n, v in nodos_c.items() if v["forma"] == "none"):
        error("Contexto", "no debe tener almacenes")
    for origen, destino, etiqueta in flujos_c:
        if not etiqueta:
            error("Contexto", f"flujo {origen} -> {destino} sin nombre")
        if terminador(origen) and terminador(destino):
            error("Contexto", f"flujo entre terminadores {origen} -> {destino} (no se muestran)")

    procesos_d = [n for n, v in nodos_d.items() if v["forma"] == "circle"]
    almacenes = [n for n, v in nodos_d.items() if v["forma"] == "none" and re.match(r"^D\d+$", n)]
    if len(procesos_d) != 8:
        error("DFD", f"se esperaban 8 procesos y hay {len(procesos_d)}")
    if sorted(almacenes) != ["D1", "D2", "D3"]:
        error("DFD", f"se esperaban los almacenes D1, D2, D3 y hay {sorted(almacenes)}")

    for origen, destino, etiqueta in flujos_d:
        if not etiqueta:
            error("DFD", f"flujo {origen} -> {destino} sin nombre")
        if origen in almacenes and destino in almacenes:
            error("DFD", f"flujo almacén -> almacén {origen} -> {destino}")
        if (terminador(origen) and destino in almacenes) or (origen in almacenes and terminador(destino)):
            error("DFD", f"flujo terminador <-> almacén {origen} -> {destino}")
        if terminador(origen) and terminador(destino):
            error("DFD", f"flujo entre terminadores {origen} -> {destino}")
        if origen not in nodos_d or destino not in nodos_d:
            error("DFD", f"flujo «{etiqueta}» usa un nodo no declarado: {origen} -> {destino}")

    for proceso in procesos_d:
        entradas = [f for f in flujos_d if f[1] == proceso]
        salidas = [f for f in flujos_d if f[0] == proceso]
        if not entradas or not salidas:
            error("DFD", f"proceso {proceso} sin entradas o sin salidas")
        nombre = nodos_d[proceso]["texto"].split(" ", 1)[-1]
        if not re.match(r"^[A-ZÁÉÍÓÚ][a-záéíóúñ]+(ar|er|ir)\b", nombre):
            aviso("DFD", f"proceso {proceso} «{nombre}» no empieza con verbo en infinitivo")
    for almacen in almacenes:
        if not any(f[1] == almacen for f in flujos_d) or not any(f[0] == almacen for f in flujos_d):
            error("DFD", f"almacén {almacen} sin escrituras o sin lecturas (los datos que salen deben haber entrado)")

    def externos(flujos, procesos_internos=None):
        resultado = set()
        for origen, destino, etiqueta in flujos:
            if terminador(origen):
                resultado.add((terminador(origen), "entra", etiqueta))
            elif terminador(destino):
                resultado.add((terminador(destino), "sale", etiqueta))
        return resultado

    externos_c = externos(flujos_c)
    externos_d = externos(flujos_d)
    for faltante in sorted(externos_c - externos_d):
        error("Balanceo", f"flujo del Contexto ausente en el Nivel 0: {faltante}")
    for sobrante in sorted(externos_d - externos_c):
        error("Balanceo", f"flujo externo del Nivel 0 que no está en el Contexto: {sobrante}")

    etiquetas_flujos = {e for _, _, e in flujos_c} | {e for _, _, e in flujos_d}
    etiquetas_almacenes = {nodos_d[a]["texto"].split(" ", 1)[-1] for a in almacenes}
    return etiquetas_flujos, etiquetas_almacenes


# ---------- Diccionario de datos ----------

def verificar_diccionario(flujos: set[str], almacenes: set[str]) -> None:
    ruta = DIAGRAMAS / "DICCIONARIO_DE_DATOS.md"
    definidos: dict[str, str] = {}
    for linea in ruta.read_text(encoding="utf-8").splitlines():
        if not linea.startswith("|") or set(linea.replace("|", "").strip()) <= {"-", " ", ":"}:
            continue
        celdas = [c.strip().replace("\x00", "|")
                  for c in linea.replace("\\|", "\x00").strip().strip("|").split("|")]
        nombre = celdas[0].replace("**", "").lower()
        if nombre in {"nombre"}:
            continue
        if nombre in definidos:
            error("DD", f"«{celdas[0]}» definido más de una vez")
        definidos[nombre] = celdas[2] if len(celdas) > 2 else ""
        if len(celdas) != 4 or not all(celdas):
            error("DD", f"«{celdas[0]}» debe tener nombre, descripción, composición y valores permitidos")
    for flujo in sorted(flujos):
        if flujo.lower() not in definidos:
            error("DD", f"flujo sin definir en el diccionario: «{flujo}»")
    for almacen in sorted(almacenes):
        if not any(almacen.lower() in nombre for nombre in definidos):
            error("DD", f"almacén sin definir en el diccionario: «{almacen}»")
    usados = set()
    for composicion in definidos.values():
        for componente in re.split(r"[+=\[\]|(){}]", re.sub(r"\*[^*]*\*", "", composicion)):
            if componente.strip().startswith('"'):
                continue  # valor literal: "TR", "Válida"
            componente = re.sub(r"^\d+|\d+$", "", componente.strip().strip("«»\"'")).strip().lower()
            es_rango = re.fullmatch(r"[a-záéíóú0-9]-[a-záéíóú0-9]", componente)
            if componente and not es_rango and not re.fullmatch(r"[0-9a-z\-/.:, ]{1,2}", componente):
                usados.add(componente)
    for componente in sorted(usados):
        if componente not in definidos and not componente.startswith('"'):
            aviso("DD", f"componente usado en una composición y sin definir: «{componente}»")


# ---------- Clases ----------

def verificar_clases() -> None:
    fuente = leer("04_clases.puml")
    clases: dict[str, list[str]] = {}
    actual = None
    for linea in fuente.splitlines():
        cabecera = re.match(r'\s*(?:abstract\s+)?class\s+(\w+)\s*\{', linea)
        if cabecera:
            actual = cabecera.group(1)
            clases[actual] = []
        elif actual and linea.strip() == "}":
            actual = None
        elif actual:
            clases[actual].append(linea.strip())
    if len(clases) != 8:
        error("Clases", f"se esperaban 8 clases y hay {len(clases)}")
    for clase, miembros in clases.items():
        for miembro in filter(None, miembros):
            if miembro.startswith("--") or miembro.startswith(".."):
                continue
            if not re.match(r"^[-+#~]", miembro):
                error("Clases", f"{clase}: «{miembro}» sin visibilidad (+ - # ~)")
            elif "(" in miembro:
                es_constructor = re.match(rf"^[-+#~]\s*{clase}\(", miembro)
                if not es_constructor and not re.search(r"\)\s*:\s*\S+", miembro):
                    error("Clases", f"{clase}: método «{miembro}» sin tipo de retorno")
            elif not re.search(r":\s*\S+", miembro):
                error("Clases", f"{clase}: atributo «{miembro}» sin tipo")
            if "(" not in miembro and re.match(r"^[-#]\s*\w+", miembro) and not re.match(r"^[-#]\s*_[a-z]", miembro):
                error("Clases", f"{clase}: atributo «{miembro}» debe llamarse _camelCase")
    for linea in fuente.splitlines():
        if re.match(r"\s*\w+\s+.*(--|\.\.)", linea) and "<|--" not in linea and "note" not in linea \
                and not linea.strip().startswith("'"):
            if len(re.findall(r'"[^"]+"', linea.split(":")[0])) < 2:
                error("Clases", f"relación sin multiplicidad en ambos extremos: «{linea.strip()}»")


# ---------- Secuencia ----------

def verificar_secuencias() -> None:
    for nombre in ("05_secuencia_autenticar.puml", "06_secuencia_extraccion.puml"):
        fuente = leer(nombre)
        etiqueta = f"Secuencia {nombre[:2]}"
        participantes = re.findall(r"^\s*(?:actor|participant)\s+", fuente, flags=re.M)
        minimo = 4 if nombre.startswith("05") else 3
        if len(participantes) < minimo:
            error(etiqueta, f"se esperaban al menos {minimo} participantes y hay {len(participantes)}")
        for linea in fuente.splitlines():
            if re.search(r"\w\s*->\s*\w", linea) and "-->" not in linea and "(" not in linea:
                error(etiqueta, f"mensaje sincrónico sin paréntesis/parámetros: «{linea.strip()}»")
            if "->>" in linea or "\\\\" in linea:
                error(etiqueta, f"mensaje asincrónico: «{linea.strip()}»")
        activaciones = len(re.findall(r"^\s*activate\b", fuente, flags=re.M))
        desactivaciones = len(re.findall(r"^\s*deactivate\b", fuente, flags=re.M))
        if activaciones != desactivaciones:
            error(etiqueta, f"focos de control desparejos: {activaciones} activate / {desactivaciones} deactivate")
        if "<u>" not in fuente:
            aviso(etiqueta, "los nombres de objetos deberían ir subrayados (<u>:Clase</u>)")


# ---------- Estado ----------

def verificar_estado() -> None:
    fuente = leer("07_estado_sesion.puml")
    esperados = {"Started", "Authenticating", "Validating", "Operating", "Closing", "Closed"}
    estados = set(re.findall(r"^\s*(\w+)\s*-->|-->\s*(\w+)", fuente, flags=re.M))
    nombres = {n for par in estados for n in par if n and n not in {"[*]"}}
    if nombres != esperados:
        error("Estado", f"estados esperados {sorted(esperados)}, hay {sorted(nombres)}")
    for linea in fuente.splitlines():
        if "-->" in linea and "[*]" not in linea and ":" not in linea:
            error("Estado", f"transición sin evento: «{linea.strip()}»")


# ---------- Actividad ----------

def verificar_actividad() -> None:
    fuente = leer("08_actividad_sistema.puml")
    for numero, linea in enumerate(fuente.splitlines(), 1):
        for guarda in re.findall(r"(?:then|else|is|not)\s*\(([^)]*)\)", linea):
            if guarda and not re.fullmatch(r"\[[^\]]+\]", guarda.strip()):
                error("Actividad", f"línea {numero}: guarda «{guarda}» debe ir entre corchetes")
    if not re.search(r"^\s*start\b", fuente, flags=re.M) or not re.search(r"^\s*stop\b", fuente, flags=re.M):
        error("Actividad", "falta el nodo inicial o el final")
    if len(re.findall(r"^\s*\|[^|]+\|", fuente, flags=re.M)) == 0:
        aviso("Actividad", "sin calles (swimlanes); el ejemplo del docente las usa")


# ---------- Casos de uso ----------

def verificar_casos_de_uso() -> None:
    fuente = leer("03_casos_de_uso.puml")
    if len(re.findall(r"^\s*usecase\s", fuente, flags=re.M)) != 5:
        error("Casos de uso", "se esperaban 5 casos de uso")
    if len(re.findall(r"^\s*actor\s", fuente, flags=re.M)) != 3:
        error("Casos de uso", "se esperaban 3 actores")
    if "<<include>>" not in fuente or "<<extend>>" not in fuente:
        error("Casos de uso", "faltan relaciones <<include>> y/o <<extend>>")
    for secundario in ("SUC", "RED"):
        if re.search(rf"^\s*{secundario}\s*-->", fuente, flags=re.M):
            error("Casos de uso", f"el actor secundario {secundario} no debe iniciar la asociación (va del caso de uso hacia el actor)")


# ---------- Diagramas contra código ----------

def verificar_contra_codigo() -> None:
    """Cada clase, atributo, método y estado de los diagramas existe en src/Atm/ y al revés (clases y estados)."""
    logica = RAIZ / "src" / "Atm" / "Logic"
    fuente = leer("04_clases.puml")
    clases = re.findall(r"^\s*(?:abstract\s+)?class\s+(\w+)", fuente, flags=re.M)
    for clase in clases:
        ruta = logica / f"{clase}.cs"
        if not ruta.exists():
            error("Código", f"la clase {clase} del diagrama no tiene {ruta.relative_to(RAIZ)}")
            continue
        codigo = ruta.read_text(encoding="utf-8")
        cuerpo = re.search(rf"class\s+{clase}\b.*?\n\}}", fuente, flags=re.S)
        if not cuerpo:
            continue
        for miembro in cuerpo.group(0).splitlines():
            miembro = miembro.strip()
            atributo = re.match(r"^[-#+]\s*(?:\{static\}\s*)?(_?\w+)\s*:", miembro)
            metodo = re.match(r"^[-#+]\s*(?:\{abstract\}\s*)?(\w+)\(", miembro)
            if metodo and metodo.group(1) not in codigo:
                error("Código", f"{clase}.{metodo.group(1)}() está en el diagrama y no en {ruta.name}")
            elif atributo and not metodo:
                nombre = atributo.group(1)
                if nombre not in codigo and nombre not in {"Tipo"}:
                    error("Código", f"{clase}.{nombre} está en el diagrama y no en {ruta.name}")
    for archivo in sorted(logica.glob("*.cs")):
        tipo = archivo.stem
        declaracion = re.search(rf"\b(class|enum)\s+{tipo}\b", archivo.read_text(encoding="utf-8"))
        if declaracion and declaracion.group(1) == "class" and tipo not in clases and tipo not in {"MoneyFormatter", "Encryption"}:
            error("Código", f"la clase {tipo} del código no está en el diagrama de clases")

    enum = (logica / "SessionState.cs").read_text(encoding="utf-8")
    estados_codigo = set(re.findall(r"^\s*(\w+),", enum, flags=re.M))
    estados_diagrama = set(re.findall(r"\b(Started|Authenticating|Validating|Operating|Closing|Closed)\b", leer("07_estado_sesion.puml")))
    if estados_codigo != estados_diagrama:
        error("Código", f"SessionState {sorted(estados_codigo)} difiere del diagrama de estado {sorted(estados_diagrama)}")
    sesion = (logica / "Session.cs").read_text(encoding="utf-8")
    eventos = set(re.findall(r"--> \w+ : (\w+)\(", leer("07_estado_sesion.puml")))
    for evento in sorted(eventos):
        if f"public " not in sesion or not re.search(rf"\b{evento}\(", sesion):
            error("Código", f"el evento {evento}() del diagrama de estado no existe en Session.cs")

    for nombre in ("05_secuencia_autenticar.puml", "06_secuencia_extraccion.puml"):
        for mensaje in re.findall(r"->\s*(?:PC|SDB|CDB|\w+)\s*:\s*(\w+)\(", leer(nombre)):
            if mensaje in {"InsertCard", "EnterPin", "SelectOperation", "EnterAmount",
                           "DispenseCash", "RegisterOperation"}:
                continue  # acciones del cajero (:AtmForm = :PantallaCajero de la consigna: Presentation + Logic), no métodos de la capa Datos
            datos = "".join(f.read_text(encoding="utf-8") for f in (RAIZ / "src" / "Atm" / "Data").glob("*.cs"))
            if not re.search(rf"\b{mensaje}\(", datos):
                error("Código", f"el mensaje {mensaje}() de {nombre} no existe en la capa Datos")


def main() -> int:
    flujos, almacenes = verificar_contexto_y_dfd()
    verificar_diccionario(flujos, almacenes)
    verificar_casos_de_uso()
    verificar_clases()
    verificar_secuencias()
    verificar_estado()
    verificar_actividad()
    verificar_contra_codigo()

    for mensaje in avisos:
        print(f"AVISO  {mensaje}")
    for mensaje in errores:
        print(f"ERROR  {mensaje}")
    print(f"\n{len(errores)} error(es), {len(avisos)} aviso(s)")
    return 1 if errores else 0


if __name__ == "__main__":
    sys.exit(main())

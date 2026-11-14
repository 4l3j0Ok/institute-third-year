"""Scrapea Google Classroom usando una copia del perfil del navegador Helium.

Uso: uv run --with playwright python scripts/scrape_classroom.py

Genera en ./resources: archivos descargados, `stream.txt`, `classwork.txt`,
`tasks/*.txt` y `manifest.json` (nombre, id de Drive, link, ruta local).
"""
import json
import re
import shutil
from pathlib import Path

from playwright.sync_api import sync_playwright

COURSE = "ODcwNjI4NTgyOTYx"
BASE = "https://classroom.google.com"
HELIUM = "/opt/helium/helium"
SRC_PROFILE = Path.home() / ".config/net.imput.helium"
SRC_DIR = "Default"
TMP_PROFILE = Path("/tmp/opencode/helium-prof")
OUT = Path(__file__).resolve().parent.parent / "resources"


def clone_profile():
    shutil.rmtree(TMP_PROFILE, ignore_errors=True)
    (TMP_PROFILE / SRC_DIR).mkdir(parents=True)
    shutil.copy(SRC_PROFILE / "Local State", TMP_PROFILE)
    for name in ("Cookies", "Preferences", "Local Storage", "Session Storage", "IndexedDB"):
        src = SRC_PROFILE / SRC_DIR / name
        dst = TMP_PROFILE / SRC_DIR / name
        if src.is_dir():
            shutil.copytree(src, dst)
        elif src.exists():
            shutil.copy(src, dst)


def render(page, url, scrolls=10):
    page.goto(url, wait_until="domcontentloaded")
    page.wait_for_timeout(6000)
    for _ in range(scrolls):
        page.mouse.wheel(0, 5000)
        page.wait_for_timeout(600)
    text = page.inner_text("body")
    links = page.eval_on_selector_all(
        "a[href]", "els=>els.map(e=>({text:e.innerText.trim().split('\\n')[0],href:e.href}))"
    )
    return text, links


def drive_id(href):
    m = re.search(r"/file/d/([\w-]+)", href)
    return m.group(1) if m else None


def download(ctx, fid, name, dest):
    resp = ctx.request.get(f"https://drive.usercontent.google.com/download?id={fid}&export=download&confirm=t")
    if resp.ok and "text/html" not in resp.headers.get("content-type", ""):
        dest.write_bytes(resp.body())
        return True
    return False


def main():
    clone_profile()
    OUT.mkdir(exist_ok=True)
    (OUT / "tasks").mkdir(exist_ok=True)
    manifest = {}
    with sync_playwright() as p:
        ctx = p.chromium.launch_persistent_context(
            str(TMP_PROFILE),
            executable_path=HELIUM,
            headless=True,
            ignore_default_args=["--password-store=basic", "--use-mock-keychain"],
            args=["--password-store=kwallet6"],
        )
        page = ctx.new_page()
        pages = {
            "stream": f"{BASE}/c/{COURSE}",
            "classwork": f"{BASE}/w/{COURSE}/t/all",
        }
        all_links = []
        for name, url in pages.items():
            text, links = render(page, url)
            (OUT / f"{name}.txt").write_text(text)
            all_links += links

        # páginas de cada tarea/material del trabajo de clase
        task_links = {
            l["href"].split("?")[0]: l["text"]
            for l in all_links
            if re.search(r"classroom\.google\.com/(c|w)/[\w-]+/(a|m|sa)/", l["href"])
        }
        for i, (href, title) in enumerate(task_links.items()):
            text, links = render(page, href, scrolls=3)
            slug = re.sub(r"\W+", "_", title)[:50] or f"task{i}"
            (OUT / "tasks" / f"{i:02d}_{slug}.txt").write_text(f"{href}\n\n{text}")
            all_links += links

        for l in all_links:
            fid = drive_id(l["href"])
            name = l["text"]
            if fid and name and "." in name and fid not in manifest:
                manifest[fid] = {"name": name, "link": f"https://drive.google.com/file/d/{fid}/view"}
        for fid, info in manifest.items():
            dest = OUT / re.sub(r"[^\w.\- ]", "_", info["name"])
            info["downloaded"] = download(ctx, fid, info["name"], dest)
            info["path"] = str(dest.relative_to(OUT.parent)) if info["downloaded"] else None
            print(("OK  " if info["downloaded"] else "FAIL"), info["name"])
        (OUT / "manifest.json").write_text(json.dumps(manifest, indent=2, ensure_ascii=False))
        ctx.close()


if __name__ == "__main__":
    main()

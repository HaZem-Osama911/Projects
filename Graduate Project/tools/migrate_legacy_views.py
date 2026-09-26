"""Rebuild the Laravel Blade views from the preserved legacy PHP pages.

The legacy tree remains the content source. This script only copies assets and
writes presentation-only Blade views; authentication and profile views are
maintained directly in Laravel.
"""

from __future__ import annotations

import re
import shutil
from pathlib import Path


ROOT = Path(__file__).resolve().parent.parent
LEGACY_PAGES = ROOT / "legacy" / "php" / "frontend" / "pages"
VIEWS = ROOT / "web" / "resources" / "views"

PAGE_SOURCES = {
    ROOT / "index.php": VIEWS / "home.blade.php",
    LEGACY_PAGES / "articles" / "article.php": VIEWS / "articles" / "article.blade.php",
    LEGACY_PAGES / "dashboard" / "diagnose.php": VIEWS / "dashboard" / "diagnose.blade.php",
    LEGACY_PAGES / "language.php": VIEWS / "language.blade.php",
}

for source in (LEGACY_PAGES / "conditions").glob("*.php"):
    PAGE_SOURCES[source] = VIEWS / "conditions" / f"{source.stem}.blade.php"

for source in (LEGACY_PAGES / "exams").glob("*.php"):
    PAGE_SOURCES[source] = VIEWS / "exams" / f"{source.stem}.blade.php"


CONDITION_ROUTES = {
    "اللجلجه": "stuttering",
    "الخنف": "nasality",
    "الثغه": "lisping",
    "السرعه": "fast_speech",
    "تاخر": "delay",
    "الحبسه": "aphasia",
    "البحه": "hoarseness",
    "الوهن-الصوتي": "vocal_asthenia",
    "الحبسه-الهستيريه": "hysterical_aphasia",
    "الخرص": "mutism",
    "التوحد": "autism",
}

SIMPLE_ROUTES = {
    "index": "home",
    "home": "home",
    "article": "article",
    "artical": "article",
    "diagnose": "diagnose",
    "language": "conditions.index",
    "exam": "exam",
    "exam1": "exam.exam1",
    "exam2": "exam.exam2",
    "exam3": "exam.exam3",
    "exam4": "exam.exam4",
    "guidedexam": "exam.guidedexam",
    "signup": "signup",
    "login": "login",
    "profile": "profile",
}


def remove_div_by_class(html: str, class_name: str) -> str:
    start_match = re.search(
        rf'<div\b[^>]*\bclass=["\'][^"\']*\b{re.escape(class_name)}\b[^"\']*["\'][^>]*>',
        html,
        flags=re.IGNORECASE,
    )
    if not start_match:
        return html

    depth = 1
    token_pattern = re.compile(r"<div\b[^>]*>|</div\s*>", re.IGNORECASE)
    for token in token_pattern.finditer(html, start_match.end()):
        depth += -1 if token.group(0).lower().startswith("</div") else 1
        if depth == 0:
            return html[: start_match.start()] + html[token.end() :]

    raise ValueError(f"Unclosed .{class_name} element")


def extract_title(source: str, fallback: str) -> str:
    php_title = re.search(r"\$pageTitle\s*=\s*['\"]([^'\"]+)['\"]", source)
    if php_title:
        return php_title.group(1).strip()

    html_title = re.search(r"<title>(.*?)</title>", source, flags=re.IGNORECASE | re.DOTALL)
    if html_title:
        return re.sub(r"\s+", " ", html_title.group(1)).strip()

    return fallback


def strip_legacy_shell(source: str) -> str:
    body_match = re.search(r"<body\b[^>]*>(.*?)(?:</body\s*>|$)", source, re.IGNORECASE | re.DOTALL)

    if body_match:
        content = body_match.group(1)
        content = remove_div_by_class(content, "header")
    else:
        content = re.sub(
            r"\A\s*<\?php\b.*?header\.php.*?\?>",
            "",
            source,
            count=1,
            flags=re.IGNORECASE | re.DOTALL,
        )

    content = re.sub(
        r"<\?php\b.*?footer\.php.*?\?>",
        "",
        content,
        flags=re.IGNORECASE | re.DOTALL,
    )
    content = re.sub(r"<!doctype\b[^>]*>", "", content, flags=re.IGNORECASE)
    content = re.sub(r"<head\b[^>]*>.*?</head\s*>", "", content, flags=re.IGNORECASE | re.DOTALL)
    content = re.sub(r"</?(?:html|body)\b[^>]*>", "", content, flags=re.IGNORECASE)
    content = content.replace("https:fonts.", "https://fonts.")

    # A legacy page contains two unmatched section closers around its old header.
    while content.lstrip().startswith("</section>"):
        content = content.lstrip()[len("</section>") :]

    return content.strip()


def convert_assets(content: str) -> str:
    # Convert every quoted legacy path first. This covers both HTML attributes
    # and JavaScript question data without re-processing generated Blade code.
    quoted_asset_pattern = re.compile(
        r'(?P<quote>["\'])(?:\.\./)*(?:frontend/)?assets/(?P<path>[^"\']+)(?P=quote)'
    )

    def quoted_replacement(match: re.Match[str]) -> str:
        quote = match.group("quote")
        path = match.group("path").strip()
        return f"{quote}{{{{ asset('assets/{path}') }}}}{quote}"

    content = quoted_asset_pattern.sub(quoted_replacement, content)

    # Handle the uncommon unquoted or whitespace-padded attribute path.
    attr_pattern = re.compile(
        r'(?P<attr>src|href|poster)=(?P<quote>["\'])'
        r"(?:\.\./)*(?:frontend/)?assets/(?P<path>[^\"']+)(?P=quote)",
        flags=re.IGNORECASE,
    )

    def attr_replacement(match: re.Match[str]) -> str:
        path = match.group("path").strip()
        return f'{match.group("attr")}="{{{{ asset(\'assets/{path}\') }}}}"'

    return attr_pattern.sub(attr_replacement, content)


def route_expression(route: str, anchor: str = "") -> str:
    return f"{{{{ route('{route}') }}}}{anchor}"


def rewrite_href(match: re.Match[str]) -> str:
    quote = match.group("quote")
    target = match.group("target").strip()

    if not target or target.startswith(("#", "http://", "https://", "mailto:", "tel:", "javascript:")):
        return match.group(0)

    normalized = target.replace("\\", "/")
    path, separator, anchor = normalized.partition("#")
    anchor_suffix = f"#{anchor}" if separator else ""
    stem = Path(path).stem

    if "Speech_Project/language_selection" in path:
        route = "pronunciation"
    elif stem in CONDITION_ROUTES:
        route = f"diagnose.{CONDITION_ROUTES[stem]}"
    elif stem in SIMPLE_ROUTES:
        route = SIMPLE_ROUTES[stem]
    else:
        return match.group(0)

    return f'href={quote}{route_expression(route, anchor_suffix)}{quote}'


def rewrite_routes(content: str) -> str:
    content = re.sub(
        r'href=(?P<quote>["\'])(?P<target>[^"\']+)(?P=quote)',
        rewrite_href,
        content,
        flags=re.IGNORECASE,
    )

    content = re.sub(
        r'action=["\'][^"\']*backend/controllers/contact\.php["\']',
        'action="{{ route(\'contact.post\') }}"',
        content,
        flags=re.IGNORECASE,
    )

    js_routes = {
        "exam.html": "exam",
        "exam.php": "exam",
        "exam1.html": "exam.exam1",
        "exam1.php": "exam.exam1",
        "exam2.html": "exam.exam2",
        "exam2.php": "exam.exam2",
        "exam3.html": "exam.exam3",
        "exam3.php": "exam.exam3",
        "exam4.html": "exam.exam4",
        "exam4.php": "exam.exam4",
        "exam5.html": "pronunciation",
    }
    for old, route in js_routes.items():
        content = content.replace(f'"{old}"', f'"{{{{ route(\'{route}\') }}}}"')

    content = content.replace(
        '<form action="{{ route(\'contact.post\') }}" method="post">',
        '<form action="{{ route(\'contact.post\') }}" method="post">\n                    @csrf',
    )
    content = content.replace('name="FirstName"', 'name="first_name"')
    content = content.replace('name="LastName"', 'name="last_name"')

    return content


def styles_for(destination: Path) -> list[str]:
    if destination.name == "article.blade.php":
        return ["article.css"]
    if destination.name == "diagnose.blade.php":
        return ["diagnoses_all.css", "exercises.css"]
    if destination.name == "language.blade.php":
        return ["diagnoses_all.css"]
    if destination.parent.name == "conditions":
        return ["videos.css"]
    if destination.parent.name == "exams":
        return ["exam.css"] if destination.name == "exam.blade.php" else ["exams.css"]
    return []


def build_view(source_path: Path, destination: Path) -> str:
    source = source_path.read_text(encoding="utf-8")
    title = extract_title(source, source_path.stem)
    content = rewrite_routes(convert_assets(strip_legacy_shell(source)))
    content = content.replace(
        "let text = video.children[1].innerHTML;",
        "let text = video.children[1].textContent;",
    ).replace(
        "title.innerHTML = text;",
        "title.textContent = text;",
    )

    sections = [
        "@extends('layouts.app')",
        f"@section('title', '{title}')",
    ]

    styles = styles_for(destination)
    if styles:
        sections.append("@push('styles')")
        sections.extend(
            f'<link rel="stylesheet" href="{{{{ asset(\'assets/css/{stylesheet}\') }}}}">'
            for stylesheet in styles
        )
        sections.append("@endpush")

    sections.extend(["", "@section('content')", content, "@endsection"])

    if destination.name == "home.blade.php":
        sections.extend(
            [
                "",
                "@push('scripts')",
                '<script src="{{ asset(\'assets/js/home.js\') }}" defer></script>',
                "@endpush",
            ]
        )
    elif destination.name == "diagnose.blade.php":
        sections.extend(
            [
                "",
                "@push('scripts')",
                '<script src="{{ asset(\'assets/js/diagnose.js\') }}" defer></script>',
                "@endpush",
            ]
        )

    return "\n".join(sections).rstrip() + "\n"


def main() -> None:
    assets_source = ROOT / "legacy" / "php" / "frontend" / "assets"
    assets_destination = ROOT / "web" / "public" / "assets"
    shutil.copytree(assets_source, assets_destination, dirs_exist_ok=True)
    for executable in assets_destination.rglob("*.exe"):
        executable.unlink()

    for source, destination in PAGE_SOURCES.items():
        destination.parent.mkdir(parents=True, exist_ok=True)
        destination.write_text(build_view(source, destination), encoding="utf-8")
        print(f"Migrated {source.relative_to(ROOT)} -> {destination.relative_to(ROOT)}")


if __name__ == "__main__":
    main()

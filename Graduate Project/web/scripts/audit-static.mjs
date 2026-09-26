import { execFileSync } from 'node:child_process';
import { existsSync, readFileSync, readdirSync } from 'node:fs';
import { dirname, extname, join } from 'node:path';
import { fileURLToPath } from 'node:url';

const root = dirname(dirname(fileURLToPath(import.meta.url)));
const publicDirectory = join(root, 'public');
const viewsDirectory = join(root, 'resources', 'views');
const aiDirectory = join(root, '..', 'services', 'pronunciation');
const failures = [];

function walk(directory) {
    return readdirSync(directory, { withFileTypes: true }).flatMap((entry) => {
        const path = join(directory, entry.name);

        return entry.isDirectory() ? walk(path) : [path];
    });
}

function checkJavaScript(path) {
    try {
        execFileSync(process.execPath, ['--check', path], { stdio: 'pipe' });
    } catch (error) {
        failures.push(`Invalid JavaScript: ${path}\n${error.stderr?.toString() ?? error.message}`);
    }
}

const publicJavaScript = walk(publicDirectory).filter((path) => extname(path) === '.js');
const aiJavaScript = ['arabic', 'english'].flatMap((language) =>
    readdirSync(join(aiDirectory, language), { withFileTypes: true })
        .filter((entry) => entry.isFile() && extname(entry.name) === '.js')
        .map((entry) => join(aiDirectory, language, entry.name)),
);

for (const path of [...publicJavaScript, ...aiJavaScript]) {
    checkJavaScript(path);
}

const bladeFiles = walk(viewsDirectory).filter((path) => path.endsWith('.blade.php'));
const aiHtmlFiles = ['arabic', 'english'].flatMap((language) =>
    readdirSync(join(aiDirectory, language), { withFileTypes: true })
        .filter((entry) => entry.isFile() && extname(entry.name) === '.html')
        .map((entry) => join(aiDirectory, language, entry.name)),
);
let assetReferences = 0;
let inlineScripts = 0;

for (const path of bladeFiles) {
    const source = readFileSync(path, 'utf8');

    if (/asset\(\\['"]/.test(source)) {
        failures.push(`Escaped asset() expression: ${path}`);
    }

    if (/(?:href|action)=["'][^"']+\.php(?:[?#"']|$)/i.test(source)) {
        failures.push(`Legacy PHP URL: ${path}`);
    }

    for (const match of source.matchAll(/asset\(\s*['"]([^'"]+)['"]\s*\)/g)) {
        assetReferences += 1;
        const relativePath = match[1].split(/[?#]/, 1)[0];

        if (!existsSync(join(publicDirectory, relativePath))) {
            failures.push(`Missing asset "${relativePath}" referenced by ${path}`);
        }
    }

    for (const match of source.matchAll(/<script(?![^>]*\bsrc=)[^>]*>([\s\S]*?)<\/script>/gi)) {
        inlineScripts += 1;
        const script = match[1]
            .replace(/\{\{[\s\S]*?\}\}/g, '__BLADE_VALUE__')
            .replace(/@json\([\s\S]*?\)/g, 'null');

        try {
            new Function(script);
        } catch (error) {
            failures.push(`Invalid inline JavaScript in ${path}: ${error.message}`);
        }
    }
}

for (const path of aiHtmlFiles) {
    const source = readFileSync(path, 'utf8');

    for (const match of source.matchAll(/(?:src|href)=["']([^"'#]+)["']/gi)) {
        const reference = match[1];
        if (/^(?:https?:|data:|mailto:|tel:)/i.test(reference)) {
            continue;
        }

        const relativePath = reference.split(/[?#]/, 1)[0];
        if (!existsSync(join(dirname(path), relativePath))) {
            failures.push(`Missing AI service asset "${relativePath}" referenced by ${path}`);
        }
    }

    for (const match of source.matchAll(/<script(?![^>]*\bsrc=)[^>]*>([\s\S]*?)<\/script>/gi)) {
        inlineScripts += 1;

        try {
            new Function(match[1]);
        } catch (error) {
            failures.push(`Invalid inline JavaScript in ${path}: ${error.message}`);
        }
    }
}

const publicExecutables = walk(publicDirectory).filter((path) => path.toLowerCase().endsWith('.exe'));
for (const path of publicExecutables) {
    failures.push(`Executable exposed from public directory: ${path}`);
}

if (failures.length > 0) {
    console.error(failures.join('\n'));
    process.exit(1);
}

console.log(
    `Static audit passed: ${bladeFiles.length} Blade views, ${aiHtmlFiles.length} AI HTML pages, `
    + `${assetReferences} Laravel asset references, `
    + `${publicJavaScript.length + aiJavaScript.length} JavaScript files, ${inlineScripts} inline scripts.`,
);

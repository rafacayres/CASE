$ErrorActionPreference = 'Stop'
$repo = Split-Path $PSScriptRoot -Parent
$assets = Join-Path $repo 'CASE\Resources\Lucide'
$work = Join-Path $env:TEMP 'CASE-lucide-tools'
[IO.Directory]::CreateDirectory($assets) | Out-Null
[IO.Directory]::CreateDirectory($work) | Out-Null
$icons = @('layout-dashboard', 'tent', 'users-round', 'calendar-clock', 'clipboard-list',
    'messages-square', 'package', 'warehouse', 'wallet', 'calculator', 'settings', 'log-out',
    'search', 'bell', 'circle-user-round', 'package-plus', 'package-minus', 'triangle-alert', 'image-plus')
$headers = @{ 'User-Agent' = 'CASE-icon-assets' }
$commit = (Invoke-RestMethod -Uri 'https://api.github.com/repos/lucide-icons/lucide/commits/main' -Headers $headers -TimeoutSec 30).sha
$base = "https://raw.githubusercontent.com/lucide-icons/lucide/$commit"
foreach ($icon in $icons) {
    Invoke-WebRequest -UseBasicParsing -Uri "$base/icons/$icon.svg" -OutFile (Join-Path $assets "$icon.svg") -TimeoutSec 30
}
Invoke-WebRequest -UseBasicParsing -Uri "$base/LICENSE" -OutFile (Join-Path $assets 'LICENSE.txt') -TimeoutSec 30
$manifest = @{ commit = $commit; icons = $icons }
[IO.File]::WriteAllText((Join-Path $work 'icons.json'), ($manifest | ConvertTo-Json), [Text.UTF8Encoding]::new($false))
& npm install --prefix $work --cache (Join-Path $work 'npm-cache') --no-audit --no-fund --ignore-scripts --save-exact '@resvg/resvg-js@2.6.2'
if ($LASTEXITCODE -ne 0) { throw 'Não foi possível preparar a conversão SVG.' }
$renderer = @'
const fs = require('fs');
const path = require('path');
const { Resvg } = require('@resvg/resvg-js');
const assets = process.argv[2];
const { icons } = JSON.parse(fs.readFileSync(path.join(__dirname, 'icons.json'), 'utf8'));
for (const icon of icons) {
    const source = fs.readFileSync(path.join(assets, icon + '.svg'), 'utf8');
    const header = ['search', 'bell', 'circle-user-round'].includes(icon);
    const svg = source.replace(/currentColor/g, header ? '#102b49' : '#d5e1ef');
    const image = new Resvg(svg, { fitTo: { mode: 'width', value: header ? 72 : 40 } }).render().asPng();
    fs.writeFileSync(path.join(assets, icon + '.png'), image);
    const blue = new Resvg(source.replace(/currentColor/g, '#1769d2'), {
        fitTo: { mode: 'width', value: 72 }
    }).render().asPng();
    fs.writeFileSync(path.join(assets, icon + '-azul.png'), blue);
}
'@
[IO.File]::WriteAllText((Join-Path $work 'render.cjs'), $renderer, [Text.UTF8Encoding]::new($false))
& node (Join-Path $work 'render.cjs') $assets
if ($LASTEXITCODE -ne 0) { throw 'Não foi possível gerar os ícones PNG.' }
$readme = @"
# Ícones Lucide

Origem: https://github.com/lucide-icons/lucide
Revisão: $commit

Os SVG originais estão preservados nesta pasta. Os PNG transparentes são usados
nos PictureBox: azul claro no menu, azul marinho no cabeçalho e variantes
com sufixo -azul para os cabeçalhos, formulários e indicadores das telas.
Os recursos são incluídos no aplicativo; não há download durante a execução.

Licenças e atribuições: LICENSE.txt (Lucide e Feather).
Para atualizar: powershell -NoProfile -File scripts\Update-LucideIcons.ps1
O conversor é instalado apenas na pasta temporária, sem dependência Node no CASE.
"@
[IO.File]::WriteAllText((Join-Path $assets 'README.md'), $readme, [Text.UTF8Encoding]::new($true))
Write-Output "OK: $($icons.Count) ícones Lucide baixados e convertidos. Revisão $commit"

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
$repo = Split-Path $PSScriptRoot -Parent
[Reflection.Assembly]::LoadFrom((Join-Path $repo 'CASE/bin/Debug/CASE.exe')) | Out-Null

$surface = [Windows.Forms.UserControl]::new()
$surface.Size = [Drawing.Size]::new(440, 200)
$surface.BackColor = [Drawing.Color]::FromArgb(246, 248, 252)
$legacy = [Windows.Forms.Panel]::new()
$legacy.Bounds = [Drawing.Rectangle]::new(24, 40, 176, 112)
$legacy.BackColor = [Drawing.Color]::White
$smooth = [Windows.Forms.Panel]::new()
$smooth.Bounds = [Drawing.Rectangle]::new(240, 40, 176, 112)
$smooth.BackColor = [Drawing.Color]::White
$surface.Controls.Add($legacy)
$surface.Controls.Add($smooth)

$path = [CASE.Utils.VisualUtils]::CriarRetanguloArredondado([Drawing.Rectangle]::new(0, 0, 176, 112), 16)
$legacy.Region = [Drawing.Region]::new($path)
$path.Dispose()
[CASE.Utils.VisualUtils]::Arredondar($smooth, 16)
if ($null -ne $smooth.Region) { throw 'Rounded control still uses a hard Region clip.' }

$surface.CreateControl()
$legacy.CreateControl()
$smooth.CreateControl()
function Capture {
    $bitmap = [Drawing.Bitmap]::new($surface.Width, $surface.Height)
    $surface.DrawToBitmap($bitmap, [Drawing.Rectangle]::new(0, 0, $surface.Width, $surface.Height))
    # DrawToBitmap não aplica a região da janela; reproduzimos seu recorte binário.
    for ($x = 0; $x -lt $legacy.Width; $x++) {
        for ($y = 0; $y -lt $legacy.Height; $y++) {
            if (-not $legacy.Region.IsVisible($x, $y)) {
                $bitmap.SetPixel($legacy.Left + $x, $legacy.Top + $y, $surface.BackColor)
            }
        }
    }
    return $bitmap
}
function BlendedPixels($bitmap, $x) {
    $count = 0
    for ($dx = 0; $dx -lt 16; $dx++) {
        for ($dy = 0; $dy -lt 16; $dy++) {
            $pixel = $bitmap.GetPixel($x + $dx, 40 + $dy).ToArgb()
            if ($pixel -ne $surface.BackColor.ToArgb() -and $pixel -ne [Drawing.Color]::White.ToArgb()) { $count++ }
        }
    }
    return $count
}
$first = Capture
$oldCount = BlendedPixels $first 24
$newCount = BlendedPixels $first 240
if ($newCount -le $oldCount) { throw "No antialias improvement: old=$oldCount, new=$newCount" }

for ($i = 0; $i -lt 50; $i++) { [CASE.Utils.VisualUtils]::Arredondar($smooth, 16) }
$repeated = Capture
for ($x = 240; $x -lt 416; $x++) {
    for ($y = 40; $y -lt 152; $y++) {
        if ($first.GetPixel($x, $y) -ne $repeated.GetPixel($x, $y)) { throw 'Repeated styling accumulated paint handlers.' }
    }
}
$folder = Join-Path $env:TEMP 'CASE-new-screens'
New-Item -ItemType Directory -Force -Path $folder | Out-Null
$first.Save((Join-Path $folder 'rounded-comparison.png'))
$first.Dispose()
$repeated.Dispose()

foreach ($size in @(0, 1, 8, 40)) {
    $smooth.Size = [Drawing.Size]::new($size, $size)
    [CASE.Utils.VisualUtils]::Arredondar($smooth, 16)
    $bitmap = Capture
    $bitmap.Dispose()
}
$smooth.Size = [Drawing.Size]::new(176, 112)
[CASE.Utils.VisualUtils]::Arredondar($smooth, 0)
$square = Capture
if ($square.GetPixel(240, 40).ToArgb() -ne [Drawing.Color]::White.ToArgb()) { throw 'Radius zero did not restore square corners.' }
$square.Dispose()
$surface.Dispose()
Write-Output "PASS: antialias pixels ($oldCount -> $newCount), repeated calls, small controls and radius zero."

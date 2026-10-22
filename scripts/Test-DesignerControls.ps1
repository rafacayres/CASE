$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Design
Add-Type -AssemblyName System.Windows.Forms
$repo = Split-Path $PSScriptRoot -Parent
$assembly = [Reflection.Assembly]::LoadFrom((Join-Path $repo 'CASE\bin\Debug\CASE.exe'))
# Verifica a instanciação dos tipos compilados em ambiente de design.
# Não substitui a leitura de InitializeComponent pelo Designer do Visual Studio.
foreach ($name in @('CASE.DashboardControl', 'CASE.ProdutosControl', 'CASE.PrincipalForm')) {
    $surface = New-Object System.ComponentModel.Design.DesignSurface
    try {
        $surface.BeginLoad($assembly.GetType($name, $true))
        if ($surface.LoadErrors.Count -gt 0) {
            throw ($surface.LoadErrors -join [Environment]::NewLine)
        }
        if (-not $surface.IsLoaded) { throw "Designer não carregou: $name" }
        Write-Output "OK DesignSurface: $name"
    } finally { $surface.Dispose() }
}

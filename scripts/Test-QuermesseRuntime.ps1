param([string]$PreviewDirectory = (Join-Path $env:TEMP 'CASE-runtime-review'))
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;
public static class CaseWindowPreview
{
    [DllImport("user32.dll")]
    public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdc, uint flags);
}
'@
$repo = Split-Path $PSScriptRoot -Parent
$assembly = [Reflection.Assembly]::LoadFrom((Join-Path $repo 'CASE\bin\Debug\CASE.exe'))
[Windows.Forms.Application]::EnableVisualStyles()
[Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)
[IO.Directory]::CreateDirectory($PreviewDirectory) | Out-Null
$steps = @()
foreach ($pair in @(
    @('buttonDashboard','DashboardControl'), @('buttonQuermesses','QuermessesControl'),
    @('buttonEquipeSetores','EquipeSetoresControl'), @('buttonEscala','EscalaControl'),
    @('buttonMovimentacoes','RegistrosControl'), @('buttonSolicitacoes','SolicitacoesControl'),
    @('buttonProdutos','ProdutosControl'), @('buttonEstoque','EstoqueControl'),
    @('buttonFinanceiro','FinanceiroControl'), @('buttonRelatorios','ApuracaoControl'),
    @('buttonConfiguracoes','ConfiguracoesControl'))) {
    $steps += @{ Kind = 'Navigate'; Button = $pair[0]; Expected = $pair[1] }
}
$steps += @{ Kind = 'Seller' }
foreach ($modal in @(
    @('buttonQuermesses','btnNovaQuermesse','QuermesseForm'),
    @('buttonEscala','btnNovaAlocacao','EscalaForm'),
    @('buttonSolicitacoes','btnNovaSolicitacao','SolicitacaoForm'),
    @('buttonEquipeSetores','btnNovoFuncionario','PessoaSetorForm'),
    @('buttonProdutos','btnNovoProduto','ProdutoForm'))) {
    $steps += @{ Kind = 'OpenModal'; Menu = $modal[0]; Button = $modal[1] }
    $steps += @{ Kind = 'InspectModal'; Expected = $modal[2] }
}
$script:runtimeReview = @{ Index = 0; Error = $null; Main = $null; Count = 0 }
function Find-RuntimeControl($root, $name) {
    $found = $root.Controls.Find($name, $true)
    if ($found.Count -eq 0) { throw "Controle não encontrado: $name" }
    return $found[0]
}
function Save-RuntimePreview($form, $name) {
    $bitmap = [Drawing.Bitmap]::new($form.Width, $form.Height)
    try {
        $form.BringToFront()
        $form.Activate()
        $form.Refresh()
        $graphics = [Drawing.Graphics]::FromImage($bitmap)
        try {
            $hdc = $graphics.GetHdc()
            try { $rendered = [CaseWindowPreview]::PrintWindow($form.Handle, $hdc, 2) }
            finally { $graphics.ReleaseHdc($hdc) }
        } finally { $graphics.Dispose() }
        if (-not $rendered) {
            $form.DrawToBitmap($bitmap, [Drawing.Rectangle]::new(0, 0, $form.Width, $form.Height))
        }
        $bitmap.Save((Join-Path $PreviewDirectory ($name + '.png')))
    } finally { $bitmap.Dispose() }
}
$timer = [Windows.Forms.Timer]::new()
$timer.Interval = 500
$timer.add_Tick({
    try {
        if ($null -eq $script:runtimeReview.Main) {
            foreach ($form in [Windows.Forms.Application]::OpenForms) {
                if ($form.GetType().Name -eq 'PrincipalForm') { $script:runtimeReview.Main = $form; break }
            }
            if ($null -eq $script:runtimeReview.Main) { return }
        }
        $main = $script:runtimeReview.Main
        if ($script:runtimeReview.Index -ge $steps.Count) { $timer.Stop(); $main.Close(); return }
        $step = $steps[$script:runtimeReview.Index]
        $script:runtimeReview.Index++
        switch ($step.Kind) {
            'Navigate' {
                $button = Find-RuntimeControl $main $step.Button
                $button.Parent.ScrollControlIntoView($button)
                $button.PerformClick()
                $screen = (Find-RuntimeControl $main 'panelConteudo').Controls[0]
                if (-not $screen.Visible -or $screen.GetType().Name -ne $step.Expected) { throw "Tela não apareceu: $($step.Expected)" }
                $main.Refresh()
                Save-RuntimePreview $main $step.Expected
                $script:runtimeReview.Count++
            }
            'Seller' {
                (Find-RuntimeControl $main 'usuarioPreview').SelectedIndex = 2
                $button = Find-RuntimeControl $main 'buttonMovimentacoes'
                $button.Parent.ScrollControlIntoView($button)
                $button.PerformClick()
                if (-not (Find-RuntimeControl $main 'btnRegistrar').Visible) { throw 'Formulário do vendedor não apareceu.' }
                Save-RuntimePreview $main 'RegistrosVendedor'
                $script:runtimeReview.Count++
            }
            'OpenModal' {
                $button = Find-RuntimeControl $main $step.Menu
                $button.Parent.ScrollControlIntoView($button)
                $button.PerformClick()
                (Find-RuntimeControl $main $step.Button).PerformClick()
            }
            'InspectModal' {
                $modal = $null
                foreach ($form in [Windows.Forms.Application]::OpenForms) {
                    if ($form.GetType().Name -eq $step.Expected) { $modal = $form; break }
                }
                if ($null -eq $modal -or -not $modal.Visible -or $modal.Controls.Count -eq 0) { throw "Modal não apareceu: $($step.Expected)" }
                if ($step.Expected -eq 'ProdutoForm') {
                    foreach ($name in @('btnSalvarProduto', 'btnCancelarProduto')) {
                        $button = Find-RuntimeControl $modal $name
                        if (-not $button.Visible -or -not $button.Parent.ClientRectangle.Contains($button.Bounds)) {
                            throw "Botão fora do formulário: $name $($button.Bounds)"
                        }
                    }
                }
                Save-RuntimePreview $modal $step.Expected
                $modal.DialogResult = [Windows.Forms.DialogResult]::Cancel
                $modal.Close()
                $script:runtimeReview.Count++
            }
        }
    } catch {
        $script:runtimeReview.Error = $_.Exception.Message
        $timer.Stop()
        $windows = @([Windows.Forms.Application]::OpenForms | ForEach-Object { $_ })
        foreach ($window in $windows) { $window.Close() }
    }
})
try {
    $timer.Start()
    # Abre o mesmo formulário inicial do Program.cs, com o ciclo normal do WinForms.
    $main = [Activator]::CreateInstance($assembly.GetType('CASE.PrincipalForm'))
    [Windows.Forms.Application]::Run($main)
} finally { $timer.Stop(); $timer.Dispose() }
if ($script:runtimeReview.Error) { throw $script:runtimeReview.Error }
if ($script:runtimeReview.Count -ne 17) { throw "Verificação incompleta: $($script:runtimeReview.Count)/17" }
Write-Output 'OK: execução real do CASE, onze telas, apresentação do vendedor e cinco modais.'
Write-Output "Prévias: $PreviewDirectory"

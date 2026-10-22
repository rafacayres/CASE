$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
$repo = Split-Path $PSScriptRoot -Parent
$assembly = [Reflection.Assembly]::LoadFrom((Join-Path $repo 'CASE\bin\Debug\CASE.exe'))
function Assert-Preview($ok, $message) { if (-not $ok) { throw $message } }
function Find-Preview($control, $name) { return $control.Controls.Find($name, $true)[0] }
function Assert-PreviewIcons($control) {
    if ($control -is [Windows.Forms.PictureBox] -and $control.Name -match '^(pictureIcone|pictureIndicador|picIcone)') {
        Assert-Preview ($null -ne $control.Image) "Ícone ausente: $($control.Name)"
    }
    foreach ($child in $control.Controls) { Assert-PreviewIcons $child }
}
function Click-Preview($control) {
    $control.GetType().GetMethod('OnClick', [Reflection.BindingFlags]'Instance,NonPublic').Invoke($control, @([EventArgs]::Empty))
}
$principal = [Activator]::CreateInstance($assembly.GetType('CASE.PrincipalForm'))
try {
    $hostPanel = Find-Preview $principal 'panelConteudo'
    Assert-PreviewIcons $principal
    foreach ($pair in @(
        @('buttonDashboard','DashboardControl'), @('buttonQuermesses','QuermessesControl'),
        @('buttonEquipeSetores','EquipeSetoresControl'), @('buttonEscala','EscalaControl'),
        @('buttonMovimentacoes','RegistrosControl'), @('buttonSolicitacoes','SolicitacoesControl'),
        @('buttonProdutos','ProdutosControl'), @('buttonEstoque','EstoqueControl'),
        @('buttonFinanceiro','FinanceiroControl'), @('buttonRelatorios','ApuracaoControl'),
        @('buttonConfiguracoes','ConfiguracoesControl'))) {
        Click-Preview (Find-Preview $principal $pair[0])
        Assert-Preview ($hostPanel.Controls[0].GetType().Name -eq $pair[1]) "Falha de navegação: $($pair[0])"
        Assert-PreviewIcons $hostPanel.Controls[0]
    }
    (Find-Preview $principal 'usuarioPreview').SelectedIndex = 2
    Click-Preview (Find-Preview $principal 'buttonMovimentacoes')
    $screen = $hostPanel.Controls[0]
    $grid = Find-Preview $screen 'dgvRegistrosEquipe'
    $count = $grid.Rows.Count
    Click-Preview (Find-Preview $screen 'btnRegistrar')
    Assert-Preview ($grid.Rows.Count -eq $count) 'Botão visual alterou os registros.'
    Click-Preview (Find-Preview $principal 'buttonFinanceiro')
    $screen = $hostPanel.Controls[0]
    $grid = Find-Preview $screen 'dgvCustos'
    $count = $grid.Rows.Count
    Click-Preview (Find-Preview $screen 'btnAdicionarCusto')
    Assert-Preview ($grid.Rows.Count -eq $count) 'Botão visual alterou os custos.'
    Click-Preview (Find-Preview $principal 'buttonEscala')
    $screen = $hostPanel.Controls[0]
    (Find-Preview $screen 'cmbFiltroEscala').SelectedIndex = 1
    $grid = Find-Preview $screen 'dgvEscala'
    Assert-Preview (@($grid.Rows | Where-Object Visible).Count -eq 1) 'Filtro de apresentação da escala não funcionou.'
    Click-Preview (Find-Preview $principal 'buttonSolicitacoes')
    $screen = $hostPanel.Controls[0]
    (Find-Preview $screen 'cmbStatusListaSolicitacoes').SelectedIndex = 1
    $grid = Find-Preview $screen 'dgvSolicitacoesQuermesse'
    Assert-Preview (@($grid.Rows | Where-Object Visible).Count -eq 1) 'Filtro de apresentação das solicitações não funcionou.'
} finally { $principal.Dispose() }
foreach ($name in @('QuermesseForm', 'SolicitacaoForm')) {
    $form = [Activator]::CreateInstance($assembly.GetType("CASE.Quermesse.$name"), [object[]]@($false))
    try {
        Assert-PreviewIcons $form
        Click-Preview (Find-Preview $form 'btnSalvarVisual')
        Assert-Preview ($form.DialogResult -eq [Windows.Forms.DialogResult]::None) 'Formulário visual simulou gravação.'
    } finally { $form.Dispose() }
}
$sample = [Activator]::CreateInstance($assembly.GetType('CASE.Quermesse.QuermessePreview'))
$form = [Activator]::CreateInstance($assembly.GetType('CASE.Quermesse.PessoaSetorForm'), [object[]]@($sample, $null))
try {
    $count = $sample.Pessoas.Count
    (Find-Preview $form 'txtNomeFuncionario').Text = 'Exemplo visual'
    Click-Preview (Find-Preview $form 'btnSalvarFuncionario')
    Assert-Preview ($sample.Pessoas.Count -eq $count) 'Formulário visual alterou funcionários.'
} finally { $form.Dispose() }
Write-Output 'OK: navegação das onze telas, campos dos formulários, filtros visuais e botões sem gravação.'

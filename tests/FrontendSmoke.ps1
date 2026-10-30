param([switch]$Render)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
[Windows.Forms.Application]::EnableVisualStyles()
$repo = Split-Path $PSScriptRoot -Parent
$assembly = [Reflection.Assembly]::LoadFrom((Join-Path $repo 'CASE/bin/Debug/CASE.exe'))
$preview = $assembly.GetType('CASE.OperacoesPreview')
$flags = [Reflection.BindingFlags]'Static,Public,NonPublic'
$movimentos = $preview.GetField('Movimentos', $flags).GetValue($null)
$contas = $preview.GetField('Contas', $flags).GetValue($null)
$locais = $preview.GetField('Locais', $flags).GetValue($null)
$clickMethod = [Windows.Forms.Button].GetMethod('OnClick', [Reflection.BindingFlags]'Instance,NonPublic')
function Field($root, $name) { return $root.Controls.Find($name, $true)[0] }
function Click($root, $name) { $clickMethod.Invoke((Field $root $name), @([EventArgs]::Empty)) | Out-Null }
function Assert($condition, $message) { if (-not $condition) { throw $message } }
function SelectRow($grid, $text, $column = 0) {
    $grid.ClearSelection()
    foreach ($row in $grid.Rows) {
        if ([string]$row.Cells[$column].Value -eq $text) {
            $grid.CurrentCell = $row.Cells[0]
            $row.Selected = $true
            return
        }
    }
    throw "Row not found: $text"
}
function CheckBounds($control) {
    foreach ($child in $control.Controls) {
        if ($child -is [Windows.Forms.DataGridView]) { continue }
        if ($child -is [Windows.Forms.Panel] -or $child -is [Windows.Forms.Label] -or $child -is [Windows.Forms.Button]) {
            Assert ($child.Right -le $control.ClientSize.Width -and $child.Bottom -le $control.ClientSize.Height) "Outside parent: $($control.Name)/$($child.Name)"
        }
        if ($child -is [Windows.Forms.Panel]) { CheckBounds $child }
    }
}
function CreateHandles($control) {
    $null = $control.Handle
    foreach ($child in $control.Controls) { CreateHandles $child }
}

foreach ($name in @('FornecedoresControl','AcessosControl','OperacoesEstoqueControl','EntradaMercadoriaForm','TransferenciaEstoqueForm')) {
    $screen = [Activator]::CreateInstance($assembly.GetType("CASE.$name"))
    Assert ((Field $screen 'pictureIconeTela').Image -ne $null) "Missing screen icon: $name"
    $title = Field $screen 'lblTitulo'
    $textSize = [Windows.Forms.TextRenderer]::MeasureText($title.Text, $title.Font)
    Assert ($textSize.Width -le $title.Width) "Title clipped: $name"
    CheckBounds $screen
    if ($screen -is [Windows.Forms.UserControl]) {
        $screen.Size = [Drawing.Size]::new(1400, 900)
        $screen.PerformLayout()
        CheckBounds $screen
    }
    if ($Render) {
        $path = Join-Path $env:TEMP 'CASE-new-screens'
        New-Item -ItemType Directory -Force -Path $path | Out-Null
        $surface = $screen
        if ($screen -is [Windows.Forms.Form]) {
            $surface = [Windows.Forms.UserControl]::new()
            $surface.Size = $screen.ClientSize
            $surface.BackColor = $screen.BackColor
            $surface.Font = $screen.Font
            while ($screen.Controls.Count) { $surface.Controls.Add($screen.Controls[0]) }
        }
        CreateHandles $surface
        $bitmap = [Drawing.Bitmap]::new($surface.Width, $surface.Height)
        $surface.DrawToBitmap($bitmap, [Drawing.Rectangle]::new(0,0,$surface.Width,$surface.Height))
        $bitmap.Save((Join-Path $path "$name.png"))
        $bitmap.Dispose()
        if ($surface -ne $screen) { $surface.Dispose() }
    }
    $screen.Dispose()
}

$fornecedores = [CASE.FornecedoresControl]::new()
(Field $fornecedores 'txtNome').Text = 'Fornecedor do teste'
Click $fornecedores 'btnSalvar'
$fornecedores.Dispose()
$entrada = [CASE.EntradaMercadoriaForm]::new()
Assert ((Field $entrada 'cmbFornecedor').Items.Count -eq 4) 'New supplier unavailable in receiving form'
$before = $movimentos.Count
Click $entrada 'btnConfirmar'
Assert ($movimentos.Count -eq $before) 'Empty receiving changed stock'
(Field $entrada 'numQuantidade').Value = 7
(Field $entrada 'numCusto').Value = 4
Click $entrada 'btnAdicionar'
Click $entrada 'btnConfirmar'
Assert ($movimentos.Count -eq $before) 'Purchase without supplier changed stock'
(Field $entrada 'cmbFornecedor').SelectedIndex = 1
Click $entrada 'btnConfirmar'
Assert ($movimentos.Count -eq $before + 1) 'Receiving did not create exactly one movement'
Assert ($movimentos[$before].Quantidade -eq 7) 'Incorrect received quantity'
$entrada.Dispose()

$cancel = [CASE.EntradaMercadoriaForm]::new()
Click $cancel 'btnAdicionar'
Click $cancel 'btnCancelar'
$cancel.Dispose()
Assert ($movimentos.Count -eq $before + 1) 'Cancelled receiving changed stock'

$total = ($movimentos | Measure-Object -Property Quantidade -Sum).Sum
$transfer = [CASE.TransferenciaEstoqueForm]::new()
(Field $transfer 'cmbDestino').SelectedIndex = 0
Click $transfer 'btnConfirmar'
Assert ($movimentos.Count -eq $before + 1) 'Same-location transfer accepted'
(Field $transfer 'cmbDestino').SelectedIndex = 1
(Field $transfer 'numQuantidade').Value = 1000
Click $transfer 'btnConfirmar'
Assert ($movimentos.Count -eq $before + 1) 'Transfer exceeded available quantity'
(Field $transfer 'numQuantidade').Value = 5
Click $transfer 'btnConfirmar'
Assert ($movimentos.Count -eq $before + 3) 'Transfer did not produce two movements'
Assert (($movimentos | Measure-Object -Property Quantidade -Sum).Sum -eq $total) 'Transfer changed institution total'
$transfer.Dispose()

$access = [CASE.AcessosControl]::new()
$grid = Field $access 'dgvContas'
SelectRow $grid 'Rafael Cayres'
(Field $access 'txtMotivo').Text = 'Teste'
Click $access 'btnSuspender'
Assert ($contas[3].Situacao -eq 'Aprovado') 'Last administrator was suspended'
SelectRow $grid 'Pedro Lima'
(Field $access 'txtMotivo').Clear()
Click $access 'btnRejeitar'
Assert ($contas[1].Situacao -eq 'Pendente') 'Rejection accepted without reason'
(Field $access 'txtMotivo').Text = 'Cadastro duplicado'
Click $access 'btnRejeitar'
Assert ($contas[1].Situacao -eq 'Rejeitado') 'Rejection failed'
SelectRow $grid 'Marina Souza'
Click $access 'btnAprovar'
Assert ($contas[0].Situacao -eq 'Aprovado') 'Approval failed'
$access.Dispose()

$principal = [CASE.PrincipalForm]::new()
$stock = [CASE.OperacoesEstoqueControl]::new()
Assert ((Field $stock 'panelSaldos').Visible) 'Initial stock tab is hidden'
Click $stock 'btnAbaHistorico'
Assert ((Field $stock 'panelHistorico').Visible -and -not (Field $stock 'panelSaldos').Visible) 'History tab did not switch'
Click $stock 'btnAbaSaldo'
Assert ((Field $stock 'panelSaldos').Visible -and -not (Field $stock 'panelHistorico').Visible) 'Stock tab did not switch'
$stock.Dispose()
Click $principal 'buttonEstoque'
Assert ((Field $principal 'panelConteudo').Controls[0] -is [CASE.OperacoesEstoqueControl]) 'Stock navigation failed'
Click $principal 'btnFornecedores'
Assert ((Field $principal 'panelConteudo').Controls[0] -is [CASE.FornecedoresControl]) 'Supplier navigation failed'
Click $principal 'buttonAcessos'
Assert ((Field $principal 'panelConteudo').Controls[0] -is [CASE.AcessosControl]) 'Account navigation failed'
(Field $principal 'usuarioPreview').SelectedIndex = 2
Click $principal 'buttonAcessos'
Assert ((Field $principal 'panelConteudo').Controls[0] -isnot [CASE.AcessosControl]) 'Non-admin preview entered administration'
$principal.Dispose()
foreach ($type in $assembly.GetTypes() | Where-Object {
    $_.IsPublic -and ($_.IsSubclassOf([Windows.Forms.UserControl]) -or $_.IsSubclassOf([Windows.Forms.Form]))
}) {
    $control = [Activator]::CreateInstance($type)
    $control.Dispose()
}
Write-Output 'PASS: layouts, suppliers, receiving, cancellation, balanced transfers, account decisions and navigation.'
if ($Render) { Write-Output "Previews: $path" }

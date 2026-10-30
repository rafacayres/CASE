namespace CASE
{
    partial class RelatoriosControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.pictureIconeTela = new System.Windows.Forms.PictureBox();
            this.lblTituloRelatorios = new System.Windows.Forms.Label();
            this.lblSubtituloRelatorios = new System.Windows.Forms.Label();
            this.panelAcoesRelatorios = new System.Windows.Forms.Panel();
            this.cmbRelatorioCategoria = new System.Windows.Forms.ComboBox();
            this.dtpRelatorioInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpRelatorioFim = new System.Windows.Forms.DateTimePicker();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.btnExportarCsv = new System.Windows.Forms.Button();
            this.panelCardRelBaixo = new System.Windows.Forms.Panel();
            this.lblCardRelBaixoTitulo = new System.Windows.Forms.Label();
            this.lblCardRelBaixoValor = new System.Windows.Forms.Label();
            this.panelCardRelSemEstoque = new System.Windows.Forms.Panel();
            this.lblCardRelSemEstoqueTitulo = new System.Windows.Forms.Label();
            this.lblCardRelSemEstoqueValor = new System.Windows.Forms.Label();
            this.panelCardRelValor = new System.Windows.Forms.Panel();
            this.lblCardRelValorTitulo = new System.Windows.Forms.Label();
            this.lblCardRelValorValor = new System.Windows.Forms.Label();
            this.panelCardRelSugestao = new System.Windows.Forms.Panel();
            this.lblCardRelSugestaoTitulo = new System.Windows.Forms.Label();
            this.lblCardRelSugestaoValor = new System.Windows.Forms.Label();
            this.panelGraficoEsquerdo = new System.Windows.Forms.Panel();
            this.lblGraficoEsquerdo = new System.Windows.Forms.Label();
            this.panelGraficoEsquerdoArea = new System.Windows.Forms.Panel();
            this.panelGraficoDireito = new System.Windows.Forms.Panel();
            this.lblGraficoDireito = new System.Windows.Forms.Label();
            this.panelGraficoDireitoArea = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).BeginInit();
            this.SuspendLayout();
            this.panelAcoesRelatorios.SuspendLayout();
            this.panelCardRelBaixo.SuspendLayout();
            this.panelCardRelSemEstoque.SuspendLayout();
            this.panelCardRelValor.SuspendLayout();
            this.panelCardRelSugestao.SuspendLayout();
            this.panelGraficoEsquerdo.SuspendLayout();
            this.panelGraficoEsquerdoArea.SuspendLayout();
            this.panelGraficoDireito.SuspendLayout();
            this.panelGraficoDireitoArea.SuspendLayout();
            // lblTituloRelatorios
            this.lblTituloRelatorios.Name = "lblTituloRelatorios";
            this.lblTituloRelatorios.Location = new System.Drawing.Point(80, 25);
            this.lblTituloRelatorios.Size = new System.Drawing.Size(350, 40);
            this.lblTituloRelatorios.TabIndex = 0;
            this.lblTituloRelatorios.Text = "Relatórios";
            this.lblTituloRelatorios.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTituloRelatorios.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // lblSubtituloRelatorios
            this.lblSubtituloRelatorios.Name = "lblSubtituloRelatorios";
            this.lblSubtituloRelatorios.Location = new System.Drawing.Point(82, 68);
            this.lblSubtituloRelatorios.Size = new System.Drawing.Size(650, 25);
            this.lblSubtituloRelatorios.TabIndex = 1;
            this.lblSubtituloRelatorios.Text = "Visualize indicadores e exportações do sistema";
            this.lblSubtituloRelatorios.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblSubtituloRelatorios.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // panelAcoesRelatorios
            this.panelAcoesRelatorios.Name = "panelAcoesRelatorios";
            this.panelAcoesRelatorios.Location = new System.Drawing.Point(30, 115);
            this.panelAcoesRelatorios.Size = new System.Drawing.Size(910, 75);
            this.panelAcoesRelatorios.TabIndex = 2;
            this.panelAcoesRelatorios.BackColor = System.Drawing.Color.White;
            this.panelAcoesRelatorios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelAcoesRelatorios.MinimumSize = new System.Drawing.Size(910, 75);
            // cmbRelatorioCategoria
            this.cmbRelatorioCategoria.Name = "cmbRelatorioCategoria";
            this.cmbRelatorioCategoria.Location = new System.Drawing.Point(20, 19);
            this.cmbRelatorioCategoria.Size = new System.Drawing.Size(180, 27);
            this.cmbRelatorioCategoria.TabIndex = 3;
            this.cmbRelatorioCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelatorioCategoria.Items.AddRange(new object[] { "Todas as categorias", "Bebidas", "Alimentos", "Descartáveis", "Outros" });
            this.cmbRelatorioCategoria.SelectedIndex = 0;
            // dtpRelatorioInicio
            this.dtpRelatorioInicio.Name = "dtpRelatorioInicio";
            this.dtpRelatorioInicio.Location = new System.Drawing.Point(220, 19);
            this.dtpRelatorioInicio.Size = new System.Drawing.Size(140, 27);
            this.dtpRelatorioInicio.TabIndex = 4;
            this.dtpRelatorioInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRelatorioInicio.Value = new System.DateTime(2026, 10, 1);
            // dtpRelatorioFim
            this.dtpRelatorioFim.Name = "dtpRelatorioFim";
            this.dtpRelatorioFim.Location = new System.Drawing.Point(380, 19);
            this.dtpRelatorioFim.Size = new System.Drawing.Size(140, 27);
            this.dtpRelatorioFim.TabIndex = 5;
            this.dtpRelatorioFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRelatorioFim.Value = new System.DateTime(2026, 10, 1);
            // btnExportarPdf
            this.btnExportarPdf.Name = "btnExportarPdf";
            this.btnExportarPdf.Location = new System.Drawing.Point(610, 17);
            this.btnExportarPdf.Size = new System.Drawing.Size(130, 40);
            this.btnExportarPdf.TabIndex = 6;
            this.btnExportarPdf.Text = "Exportar PDF";
            this.btnExportarPdf.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnExportarPdf.ForeColor = System.Drawing.Color.White;
            this.btnExportarPdf.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExportarPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarPdf.FlatAppearance.BorderSize = 0;
            this.btnExportarPdf.UseVisualStyleBackColor = false;
            this.btnExportarPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarPdf.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            // btnExportarCsv
            this.btnExportarCsv.Name = "btnExportarCsv";
            this.btnExportarCsv.Location = new System.Drawing.Point(760, 17);
            this.btnExportarCsv.Size = new System.Drawing.Size(130, 40);
            this.btnExportarCsv.TabIndex = 7;
            this.btnExportarCsv.Text = "Exportar CSV";
            this.btnExportarCsv.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnExportarCsv.ForeColor = System.Drawing.Color.White;
            this.btnExportarCsv.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExportarCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarCsv.FlatAppearance.BorderSize = 0;
            this.btnExportarCsv.UseVisualStyleBackColor = false;
            this.btnExportarCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarCsv.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            // panelCardRelBaixo
            this.panelCardRelBaixo.Name = "panelCardRelBaixo";
            this.panelCardRelBaixo.Location = new System.Drawing.Point(30, 210);
            this.panelCardRelBaixo.Size = new System.Drawing.Size(210, 85);
            this.panelCardRelBaixo.TabIndex = 8;
            this.panelCardRelBaixo.BackColor = System.Drawing.Color.White;
            // lblCardRelBaixoTitulo
            this.lblCardRelBaixoTitulo.Name = "lblCardRelBaixoTitulo";
            this.lblCardRelBaixoTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblCardRelBaixoTitulo.Size = new System.Drawing.Size(145, 22);
            this.lblCardRelBaixoTitulo.TabIndex = 9;
            this.lblCardRelBaixoTitulo.Text = "Estoque baixo";
            this.lblCardRelBaixoTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCardRelBaixoTitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // lblCardRelBaixoValor
            this.lblCardRelBaixoValor.Name = "lblCardRelBaixoValor";
            this.lblCardRelBaixoValor.Location = new System.Drawing.Point(15, 34);
            this.lblCardRelBaixoValor.Size = new System.Drawing.Size(145, 40);
            this.lblCardRelBaixoValor.TabIndex = 10;
            this.lblCardRelBaixoValor.Text = "1";
            this.lblCardRelBaixoValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblCardRelBaixoValor.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelCardRelSemEstoque
            this.panelCardRelSemEstoque.Name = "panelCardRelSemEstoque";
            this.panelCardRelSemEstoque.Location = new System.Drawing.Point(260, 210);
            this.panelCardRelSemEstoque.Size = new System.Drawing.Size(210, 85);
            this.panelCardRelSemEstoque.TabIndex = 11;
            this.panelCardRelSemEstoque.BackColor = System.Drawing.Color.White;
            // lblCardRelSemEstoqueTitulo
            this.lblCardRelSemEstoqueTitulo.Name = "lblCardRelSemEstoqueTitulo";
            this.lblCardRelSemEstoqueTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblCardRelSemEstoqueTitulo.Size = new System.Drawing.Size(145, 22);
            this.lblCardRelSemEstoqueTitulo.TabIndex = 12;
            this.lblCardRelSemEstoqueTitulo.Text = "Sem estoque";
            this.lblCardRelSemEstoqueTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCardRelSemEstoqueTitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // lblCardRelSemEstoqueValor
            this.lblCardRelSemEstoqueValor.Name = "lblCardRelSemEstoqueValor";
            this.lblCardRelSemEstoqueValor.Location = new System.Drawing.Point(15, 34);
            this.lblCardRelSemEstoqueValor.Size = new System.Drawing.Size(145, 40);
            this.lblCardRelSemEstoqueValor.TabIndex = 13;
            this.lblCardRelSemEstoqueValor.Text = "1";
            this.lblCardRelSemEstoqueValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblCardRelSemEstoqueValor.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelCardRelValor
            this.panelCardRelValor.Name = "panelCardRelValor";
            this.panelCardRelValor.Location = new System.Drawing.Point(490, 210);
            this.panelCardRelValor.Size = new System.Drawing.Size(210, 85);
            this.panelCardRelValor.TabIndex = 14;
            this.panelCardRelValor.BackColor = System.Drawing.Color.White;
            // lblCardRelValorTitulo
            this.lblCardRelValorTitulo.Name = "lblCardRelValorTitulo";
            this.lblCardRelValorTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblCardRelValorTitulo.Size = new System.Drawing.Size(145, 22);
            this.lblCardRelValorTitulo.TabIndex = 15;
            this.lblCardRelValorTitulo.Text = "Valor total";
            this.lblCardRelValorTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCardRelValorTitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // lblCardRelValorValor
            this.lblCardRelValorValor.Name = "lblCardRelValorValor";
            this.lblCardRelValorValor.Location = new System.Drawing.Point(15, 34);
            this.lblCardRelValorValor.Size = new System.Drawing.Size(145, 40);
            this.lblCardRelValorValor.TabIndex = 16;
            this.lblCardRelValorValor.Text = "R$ 445,50";
            this.lblCardRelValorValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblCardRelValorValor.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelCardRelSugestao
            this.panelCardRelSugestao.Name = "panelCardRelSugestao";
            this.panelCardRelSugestao.Location = new System.Drawing.Point(720, 210);
            this.panelCardRelSugestao.Size = new System.Drawing.Size(210, 85);
            this.panelCardRelSugestao.TabIndex = 17;
            this.panelCardRelSugestao.BackColor = System.Drawing.Color.White;
            // lblCardRelSugestaoTitulo
            this.lblCardRelSugestaoTitulo.Name = "lblCardRelSugestaoTitulo";
            this.lblCardRelSugestaoTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblCardRelSugestaoTitulo.Size = new System.Drawing.Size(145, 22);
            this.lblCardRelSugestaoTitulo.TabIndex = 18;
            this.lblCardRelSugestaoTitulo.Text = "Sugestões";
            this.lblCardRelSugestaoTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCardRelSugestaoTitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // lblCardRelSugestaoValor
            this.lblCardRelSugestaoValor.Name = "lblCardRelSugestaoValor";
            this.lblCardRelSugestaoValor.Location = new System.Drawing.Point(15, 34);
            this.lblCardRelSugestaoValor.Size = new System.Drawing.Size(145, 40);
            this.lblCardRelSugestaoValor.TabIndex = 19;
            this.lblCardRelSugestaoValor.Text = "2";
            this.lblCardRelSugestaoValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblCardRelSugestaoValor.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelGraficoEsquerdo
            this.panelGraficoEsquerdo.Name = "panelGraficoEsquerdo";
            this.panelGraficoEsquerdo.Location = new System.Drawing.Point(30, 315);
            this.panelGraficoEsquerdo.Size = new System.Drawing.Size(445, 250);
            this.panelGraficoEsquerdo.TabIndex = 20;
            this.panelGraficoEsquerdo.BackColor = System.Drawing.Color.White;
            // lblGraficoEsquerdo
            this.lblGraficoEsquerdo.Name = "lblGraficoEsquerdo";
            this.lblGraficoEsquerdo.Location = new System.Drawing.Point(20, 15);
            this.lblGraficoEsquerdo.Size = new System.Drawing.Size(400, 30);
            this.lblGraficoEsquerdo.TabIndex = 21;
            this.lblGraficoEsquerdo.Text = "Produtos por categoria";
            this.lblGraficoEsquerdo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblGraficoEsquerdo.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelGraficoEsquerdoArea
            this.panelGraficoEsquerdoArea.Name = "panelGraficoEsquerdoArea";
            this.panelGraficoEsquerdoArea.Location = new System.Drawing.Point(20, 60);
            this.panelGraficoEsquerdoArea.Size = new System.Drawing.Size(405, 170);
            this.panelGraficoEsquerdoArea.TabIndex = 22;
            this.panelGraficoEsquerdoArea.BackColor = System.Drawing.Color.FromArgb(243, 247, 252);
            this.panelGraficoEsquerdoArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            // panelGraficoDireito
            this.panelGraficoDireito.Name = "panelGraficoDireito";
            this.panelGraficoDireito.Location = new System.Drawing.Point(495, 315);
            this.panelGraficoDireito.Size = new System.Drawing.Size(445, 250);
            this.panelGraficoDireito.TabIndex = 23;
            this.panelGraficoDireito.BackColor = System.Drawing.Color.White;
            // lblGraficoDireito
            this.lblGraficoDireito.Name = "lblGraficoDireito";
            this.lblGraficoDireito.Location = new System.Drawing.Point(20, 15);
            this.lblGraficoDireito.Size = new System.Drawing.Size(400, 30);
            this.lblGraficoDireito.TabIndex = 24;
            this.lblGraficoDireito.Text = "Resumo do estoque";
            this.lblGraficoDireito.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblGraficoDireito.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelGraficoDireitoArea
            this.panelGraficoDireitoArea.Name = "panelGraficoDireitoArea";
            this.panelGraficoDireitoArea.Location = new System.Drawing.Point(20, 60);
            this.panelGraficoDireitoArea.Size = new System.Drawing.Size(405, 170);
            this.panelGraficoDireitoArea.TabIndex = 25;
            this.panelGraficoDireitoArea.BackColor = System.Drawing.Color.FromArgb(243, 247, 252);
            this.panelGraficoDireitoArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(this.lblTituloRelatorios);
            this.Controls.Add(this.lblSubtituloRelatorios);
            this.Controls.Add(this.panelAcoesRelatorios);
            this.panelAcoesRelatorios.Controls.Add(this.cmbRelatorioCategoria);
            this.panelAcoesRelatorios.Controls.Add(this.dtpRelatorioInicio);
            this.panelAcoesRelatorios.Controls.Add(this.dtpRelatorioFim);
            this.panelAcoesRelatorios.Controls.Add(this.btnExportarPdf);
            this.panelAcoesRelatorios.Controls.Add(this.btnExportarCsv);
            this.Controls.Add(this.panelCardRelBaixo);
            this.panelCardRelBaixo.Controls.Add(this.lblCardRelBaixoTitulo);
            this.panelCardRelBaixo.Controls.Add(this.lblCardRelBaixoValor);
            this.Controls.Add(this.panelCardRelSemEstoque);
            this.panelCardRelSemEstoque.Controls.Add(this.lblCardRelSemEstoqueTitulo);
            this.panelCardRelSemEstoque.Controls.Add(this.lblCardRelSemEstoqueValor);
            this.Controls.Add(this.panelCardRelValor);
            this.panelCardRelValor.Controls.Add(this.lblCardRelValorTitulo);
            this.panelCardRelValor.Controls.Add(this.lblCardRelValorValor);
            this.Controls.Add(this.panelCardRelSugestao);
            this.panelCardRelSugestao.Controls.Add(this.lblCardRelSugestaoTitulo);
            this.panelCardRelSugestao.Controls.Add(this.lblCardRelSugestaoValor);
            this.Controls.Add(this.panelGraficoEsquerdo);
            this.panelGraficoEsquerdo.Controls.Add(this.lblGraficoEsquerdo);
            this.panelGraficoEsquerdo.Controls.Add(this.panelGraficoEsquerdoArea);
            this.Controls.Add(this.panelGraficoDireito);
            this.panelGraficoDireito.Controls.Add(this.lblGraficoDireito);
            this.panelGraficoDireito.Controls.Add(this.panelGraficoDireitoArea);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor = System.Drawing.Color.FromArgb(243, 247, 252);
            this.Name = "RelatoriosControl";
            this.Size = new System.Drawing.Size(970, 680);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(970, 660);
            this.panelAcoesRelatorios.ResumeLayout(false);
            this.panelAcoesRelatorios.PerformLayout();
            this.panelCardRelBaixo.ResumeLayout(false);
            this.panelCardRelBaixo.PerformLayout();
            this.panelCardRelSemEstoque.ResumeLayout(false);
            this.panelCardRelSemEstoque.PerformLayout();
            this.panelCardRelValor.ResumeLayout(false);
            this.panelCardRelValor.PerformLayout();
            this.panelCardRelSugestao.ResumeLayout(false);
            this.panelCardRelSugestao.PerformLayout();
            this.panelGraficoEsquerdo.ResumeLayout(false);
            this.panelGraficoEsquerdo.PerformLayout();
            this.panelGraficoEsquerdoArea.ResumeLayout(false);
            this.panelGraficoEsquerdoArea.PerformLayout();
            this.panelGraficoDireito.ResumeLayout(false);
            this.panelGraficoDireito.PerformLayout();
            this.panelGraficoDireitoArea.ResumeLayout(false);
            this.panelGraficoDireitoArea.PerformLayout();
            // pictureIconeTela — Lucide
            this.pictureIconeTela.Name = "pictureIconeTela";
            this.pictureIconeTela.Image = global::CASE.Properties.Resources.LucideAzulApuracao;
            this.pictureIconeTela.Location = new System.Drawing.Point(30, 28);
            this.pictureIconeTela.Size = new System.Drawing.Size(36, 36);
            this.pictureIconeTela.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureIconeTela.BackColor = System.Drawing.Color.FromArgb(232, 240, 252);
            this.pictureIconeTela.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.pictureIconeTela.TabStop = false;
            this.pictureIconeTela.Enabled = false;
            this.Controls.Add(this.pictureIconeTela);
            this.pictureIconeTela.BringToFront();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Label lblTituloRelatorios;
        private System.Windows.Forms.Label lblSubtituloRelatorios;
        private System.Windows.Forms.Panel panelAcoesRelatorios;
        private System.Windows.Forms.ComboBox cmbRelatorioCategoria;
        private System.Windows.Forms.DateTimePicker dtpRelatorioInicio;
        private System.Windows.Forms.DateTimePicker dtpRelatorioFim;
        private System.Windows.Forms.Button btnExportarPdf;
        private System.Windows.Forms.Button btnExportarCsv;
        private System.Windows.Forms.Panel panelCardRelBaixo;
        private System.Windows.Forms.Label lblCardRelBaixoTitulo;
        private System.Windows.Forms.Label lblCardRelBaixoValor;
        private System.Windows.Forms.Panel panelCardRelSemEstoque;
        private System.Windows.Forms.Label lblCardRelSemEstoqueTitulo;
        private System.Windows.Forms.Label lblCardRelSemEstoqueValor;
        private System.Windows.Forms.Panel panelCardRelValor;
        private System.Windows.Forms.Label lblCardRelValorTitulo;
        private System.Windows.Forms.Label lblCardRelValorValor;
        private System.Windows.Forms.Panel panelCardRelSugestao;
        private System.Windows.Forms.Label lblCardRelSugestaoTitulo;
        private System.Windows.Forms.Label lblCardRelSugestaoValor;
        private System.Windows.Forms.Panel panelGraficoEsquerdo;
        private System.Windows.Forms.Label lblGraficoEsquerdo;
        private System.Windows.Forms.Panel panelGraficoEsquerdoArea;
        private System.Windows.Forms.Panel panelGraficoDireito;
        private System.Windows.Forms.Label lblGraficoDireito;
        private System.Windows.Forms.Panel panelGraficoDireitoArea;
        private System.Windows.Forms.PictureBox pictureIconeTela;
    }
}


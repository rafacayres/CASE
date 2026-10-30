namespace CASE
{
    partial class MovimentacoesControl
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
            System.Windows.Forms.DataGridViewCellStyle estiloCabecalho = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle estiloCelulas = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTituloMovimentacoes = new System.Windows.Forms.Label();
            this.lblSubtituloMovimentacoes = new System.Windows.Forms.Label();
            this.panelCardEntradasMes = new System.Windows.Forms.Panel();
            this.lblCardEntradasMesTitulo = new System.Windows.Forms.Label();
            this.lblCardEntradasMesValor = new System.Windows.Forms.Label();
            this.panelCardSaidasMes = new System.Windows.Forms.Panel();
            this.lblCardSaidasMesTitulo = new System.Windows.Forms.Label();
            this.lblCardSaidasMesValor = new System.Windows.Forms.Label();
            this.panelFiltrosMovimentacoes = new System.Windows.Forms.Panel();
            this.cmbTipoMovimentacao = new System.Windows.Forms.ComboBox();
            this.txtProdutoMovimentacao = new System.Windows.Forms.TextBox();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.btnNovaMovimentacao = new System.Windows.Forms.Button();
            this.panelListaMovimentacoes = new System.Windows.Forms.Panel();
            this.lblListaMovimentacoes = new System.Windows.Forms.Label();
            this.dgvMovimentacoes = new System.Windows.Forms.DataGridView();
            this.dgvMovimentacoesColData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMovimentacoesColProduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMovimentacoesColTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMovimentacoesColQuantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMovimentacoesColUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMovimentacoesColMotivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimentacoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).BeginInit();
            this.SuspendLayout();
            this.panelCardEntradasMes.SuspendLayout();
            this.panelCardSaidasMes.SuspendLayout();
            this.panelFiltrosMovimentacoes.SuspendLayout();
            this.panelListaMovimentacoes.SuspendLayout();
            // lblTituloMovimentacoes
            this.lblTituloMovimentacoes.Name = "lblTituloMovimentacoes";
            this.lblTituloMovimentacoes.Location = new System.Drawing.Point(80, 25);
            this.lblTituloMovimentacoes.Size = new System.Drawing.Size(350, 40);
            this.lblTituloMovimentacoes.TabIndex = 0;
            this.lblTituloMovimentacoes.Text = "Movimentações";
            this.lblTituloMovimentacoes.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTituloMovimentacoes.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // lblSubtituloMovimentacoes
            this.lblSubtituloMovimentacoes.Name = "lblSubtituloMovimentacoes";
            this.lblSubtituloMovimentacoes.Location = new System.Drawing.Point(82, 68);
            this.lblSubtituloMovimentacoes.Size = new System.Drawing.Size(650, 25);
            this.lblSubtituloMovimentacoes.TabIndex = 1;
            this.lblSubtituloMovimentacoes.Text = "Acompanhe entradas e saídas registradas no estoque";
            this.lblSubtituloMovimentacoes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblSubtituloMovimentacoes.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // panelCardEntradasMes
            this.panelCardEntradasMes.Name = "panelCardEntradasMes";
            this.panelCardEntradasMes.Location = new System.Drawing.Point(30, 110);
            this.panelCardEntradasMes.Size = new System.Drawing.Size(445, 85);
            this.panelCardEntradasMes.TabIndex = 2;
            this.panelCardEntradasMes.BackColor = System.Drawing.Color.White;
            // lblCardEntradasMesTitulo
            this.lblCardEntradasMesTitulo.Name = "lblCardEntradasMesTitulo";
            this.lblCardEntradasMesTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblCardEntradasMesTitulo.Size = new System.Drawing.Size(380, 22);
            this.lblCardEntradasMesTitulo.TabIndex = 3;
            this.lblCardEntradasMesTitulo.Text = "Entradas no mês";
            this.lblCardEntradasMesTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCardEntradasMesTitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // lblCardEntradasMesValor
            this.lblCardEntradasMesValor.Name = "lblCardEntradasMesValor";
            this.lblCardEntradasMesValor.Location = new System.Drawing.Point(15, 34);
            this.lblCardEntradasMesValor.Size = new System.Drawing.Size(380, 40);
            this.lblCardEntradasMesValor.TabIndex = 4;
            this.lblCardEntradasMesValor.Text = "193";
            this.lblCardEntradasMesValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblCardEntradasMesValor.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelCardSaidasMes
            this.panelCardSaidasMes.Name = "panelCardSaidasMes";
            this.panelCardSaidasMes.Location = new System.Drawing.Point(495, 110);
            this.panelCardSaidasMes.Size = new System.Drawing.Size(445, 85);
            this.panelCardSaidasMes.TabIndex = 5;
            this.panelCardSaidasMes.BackColor = System.Drawing.Color.White;
            // lblCardSaidasMesTitulo
            this.lblCardSaidasMesTitulo.Name = "lblCardSaidasMesTitulo";
            this.lblCardSaidasMesTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblCardSaidasMesTitulo.Size = new System.Drawing.Size(380, 22);
            this.lblCardSaidasMesTitulo.TabIndex = 6;
            this.lblCardSaidasMesTitulo.Text = "Saídas no mês";
            this.lblCardSaidasMesTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCardSaidasMesTitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // lblCardSaidasMesValor
            this.lblCardSaidasMesValor.Name = "lblCardSaidasMesValor";
            this.lblCardSaidasMesValor.Location = new System.Drawing.Point(15, 34);
            this.lblCardSaidasMesValor.Size = new System.Drawing.Size(380, 40);
            this.lblCardSaidasMesValor.TabIndex = 7;
            this.lblCardSaidasMesValor.Text = "32";
            this.lblCardSaidasMesValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblCardSaidasMesValor.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelFiltrosMovimentacoes
            this.panelFiltrosMovimentacoes.Name = "panelFiltrosMovimentacoes";
            this.panelFiltrosMovimentacoes.Location = new System.Drawing.Point(30, 215);
            this.panelFiltrosMovimentacoes.Size = new System.Drawing.Size(910, 90);
            this.panelFiltrosMovimentacoes.TabIndex = 8;
            this.panelFiltrosMovimentacoes.BackColor = System.Drawing.Color.White;
            this.panelFiltrosMovimentacoes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelFiltrosMovimentacoes.MinimumSize = new System.Drawing.Size(910, 90);
            // cmbTipoMovimentacao
            this.cmbTipoMovimentacao.Name = "cmbTipoMovimentacao";
            this.cmbTipoMovimentacao.Location = new System.Drawing.Point(20, 27);
            this.cmbTipoMovimentacao.Size = new System.Drawing.Size(150, 27);
            this.cmbTipoMovimentacao.TabIndex = 9;
            this.cmbTipoMovimentacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoMovimentacao.Items.AddRange(new object[] { "Todas", "Entrada", "Saída" });
            this.cmbTipoMovimentacao.SelectedIndex = 0;
            // txtProdutoMovimentacao
            this.txtProdutoMovimentacao.Name = "txtProdutoMovimentacao";
            this.txtProdutoMovimentacao.Location = new System.Drawing.Point(190, 27);
            this.txtProdutoMovimentacao.Size = new System.Drawing.Size(200, 25);
            this.txtProdutoMovimentacao.TabIndex = 10;
            this.txtProdutoMovimentacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProdutoMovimentacao.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // dtpDataInicial
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Location = new System.Drawing.Point(410, 27);
            this.dtpDataInicial.Size = new System.Drawing.Size(140, 27);
            this.dtpDataInicial.TabIndex = 11;
            this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataInicial.Value = new System.DateTime(2026, 10, 1);
            // dtpDataFinal
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Location = new System.Drawing.Point(570, 27);
            this.dtpDataFinal.Size = new System.Drawing.Size(140, 27);
            this.dtpDataFinal.TabIndex = 12;
            this.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataFinal.Value = new System.DateTime(2026, 10, 1);
            // btnNovaMovimentacao
            this.btnNovaMovimentacao.Name = "btnNovaMovimentacao";
            this.btnNovaMovimentacao.Location = new System.Drawing.Point(730, 25);
            this.btnNovaMovimentacao.Size = new System.Drawing.Size(160, 40);
            this.btnNovaMovimentacao.TabIndex = 13;
            this.btnNovaMovimentacao.Text = "Nova Movimentação";
            this.btnNovaMovimentacao.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnNovaMovimentacao.ForeColor = System.Drawing.Color.White;
            this.btnNovaMovimentacao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNovaMovimentacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovaMovimentacao.FlatAppearance.BorderSize = 0;
            this.btnNovaMovimentacao.UseVisualStyleBackColor = false;
            this.btnNovaMovimentacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovaMovimentacao.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            // panelListaMovimentacoes
            this.panelListaMovimentacoes.Name = "panelListaMovimentacoes";
            this.panelListaMovimentacoes.Location = new System.Drawing.Point(30, 325);
            this.panelListaMovimentacoes.Size = new System.Drawing.Size(910, 305);
            this.panelListaMovimentacoes.TabIndex = 14;
            this.panelListaMovimentacoes.BackColor = System.Drawing.Color.White;
            this.panelListaMovimentacoes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelListaMovimentacoes.MinimumSize = new System.Drawing.Size(910, 305);
            // lblListaMovimentacoes
            this.lblListaMovimentacoes.Name = "lblListaMovimentacoes";
            this.lblListaMovimentacoes.Location = new System.Drawing.Point(20, 15);
            this.lblListaMovimentacoes.Size = new System.Drawing.Size(350, 30);
            this.lblListaMovimentacoes.TabIndex = 15;
            this.lblListaMovimentacoes.Text = "Histórico de movimentações";
            this.lblListaMovimentacoes.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblListaMovimentacoes.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // dgvMovimentacoes
            this.dgvMovimentacoes.Name = "dgvMovimentacoes";
            this.dgvMovimentacoes.Location = new System.Drawing.Point(20, 60);
            this.dgvMovimentacoes.Size = new System.Drawing.Size(870, 205);
            this.dgvMovimentacoes.TabIndex = 16;
            this.dgvMovimentacoes.AllowUserToAddRows = false;
            this.dgvMovimentacoes.AllowUserToDeleteRows = false;
            this.dgvMovimentacoes.AllowUserToResizeRows = false;
            this.dgvMovimentacoes.ReadOnly = true;
            this.dgvMovimentacoes.RowHeadersVisible = false;
            this.dgvMovimentacoes.MultiSelect = false;
            this.dgvMovimentacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovimentacoes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMovimentacoes.BackgroundColor = System.Drawing.Color.White;
            this.dgvMovimentacoes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMovimentacoes.EnableHeadersVisualStyles = false;
            this.dgvMovimentacoes.ColumnHeadersHeight = 42;
            this.dgvMovimentacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMovimentacoes.RowTemplate.Height = 48;
            this.dgvMovimentacoes.GridColor = System.Drawing.Color.FromArgb(230, 235, 242);
            this.dgvMovimentacoes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            estiloCabecalho.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            estiloCabecalho.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            estiloCabecalho.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            estiloCabecalho.SelectionBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            estiloCabecalho.SelectionForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            this.dgvMovimentacoes.ColumnHeadersDefaultCellStyle = estiloCabecalho;
            estiloCelulas.BackColor = System.Drawing.Color.White;
            estiloCelulas.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            estiloCelulas.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            estiloCelulas.SelectionForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            this.dgvMovimentacoes.DefaultCellStyle = estiloCelulas;
            // dgvMovimentacoesColData
            this.dgvMovimentacoesColData.Name = "dgvMovimentacoesColData";
            this.dgvMovimentacoesColData.HeaderText = "Data";
            this.dgvMovimentacoesColData.ReadOnly = true;
            this.dgvMovimentacoesColData.FillWeight = 100F;
            // dgvMovimentacoesColProduto
            this.dgvMovimentacoesColProduto.Name = "dgvMovimentacoesColProduto";
            this.dgvMovimentacoesColProduto.HeaderText = "Produto";
            this.dgvMovimentacoesColProduto.ReadOnly = true;
            this.dgvMovimentacoesColProduto.FillWeight = 180F;
            // dgvMovimentacoesColTipo
            this.dgvMovimentacoesColTipo.Name = "dgvMovimentacoesColTipo";
            this.dgvMovimentacoesColTipo.HeaderText = "Tipo";
            this.dgvMovimentacoesColTipo.ReadOnly = true;
            this.dgvMovimentacoesColTipo.FillWeight = 80F;
            // dgvMovimentacoesColQuantidade
            this.dgvMovimentacoesColQuantidade.Name = "dgvMovimentacoesColQuantidade";
            this.dgvMovimentacoesColQuantidade.HeaderText = "Quantidade";
            this.dgvMovimentacoesColQuantidade.ReadOnly = true;
            this.dgvMovimentacoesColQuantidade.FillWeight = 80F;
            // dgvMovimentacoesColUsuario
            this.dgvMovimentacoesColUsuario.Name = "dgvMovimentacoesColUsuario";
            this.dgvMovimentacoesColUsuario.HeaderText = "Usuário";
            this.dgvMovimentacoesColUsuario.ReadOnly = true;
            this.dgvMovimentacoesColUsuario.FillWeight = 100F;
            // dgvMovimentacoesColMotivo
            this.dgvMovimentacoesColMotivo.Name = "dgvMovimentacoesColMotivo";
            this.dgvMovimentacoesColMotivo.HeaderText = "Motivo";
            this.dgvMovimentacoesColMotivo.ReadOnly = true;
            this.dgvMovimentacoesColMotivo.FillWeight = 140F;
            this.Controls.Add(this.lblTituloMovimentacoes);
            this.Controls.Add(this.lblSubtituloMovimentacoes);
            this.Controls.Add(this.panelCardEntradasMes);
            this.panelCardEntradasMes.Controls.Add(this.lblCardEntradasMesTitulo);
            this.panelCardEntradasMes.Controls.Add(this.lblCardEntradasMesValor);
            this.Controls.Add(this.panelCardSaidasMes);
            this.panelCardSaidasMes.Controls.Add(this.lblCardSaidasMesTitulo);
            this.panelCardSaidasMes.Controls.Add(this.lblCardSaidasMesValor);
            this.Controls.Add(this.panelFiltrosMovimentacoes);
            this.panelFiltrosMovimentacoes.Controls.Add(this.cmbTipoMovimentacao);
            this.panelFiltrosMovimentacoes.Controls.Add(this.txtProdutoMovimentacao);
            this.panelFiltrosMovimentacoes.Controls.Add(this.dtpDataInicial);
            this.panelFiltrosMovimentacoes.Controls.Add(this.dtpDataFinal);
            this.panelFiltrosMovimentacoes.Controls.Add(this.btnNovaMovimentacao);
            this.Controls.Add(this.panelListaMovimentacoes);
            this.panelListaMovimentacoes.Controls.Add(this.lblListaMovimentacoes);
            this.panelListaMovimentacoes.Controls.Add(this.dgvMovimentacoes);
            this.dgvMovimentacoes.Columns.Add(this.dgvMovimentacoesColData);
            this.dgvMovimentacoes.Columns.Add(this.dgvMovimentacoesColProduto);
            this.dgvMovimentacoes.Columns.Add(this.dgvMovimentacoesColTipo);
            this.dgvMovimentacoes.Columns.Add(this.dgvMovimentacoesColQuantidade);
            this.dgvMovimentacoes.Columns.Add(this.dgvMovimentacoesColUsuario);
            this.dgvMovimentacoes.Columns.Add(this.dgvMovimentacoesColMotivo);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor = System.Drawing.Color.FromArgb(243, 247, 252);
            this.Name = "MovimentacoesControl";
            this.Size = new System.Drawing.Size(970, 680);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(970, 660);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimentacoes)).EndInit();
            this.panelCardEntradasMes.ResumeLayout(false);
            this.panelCardEntradasMes.PerformLayout();
            this.panelCardSaidasMes.ResumeLayout(false);
            this.panelCardSaidasMes.PerformLayout();
            this.panelFiltrosMovimentacoes.ResumeLayout(false);
            this.panelFiltrosMovimentacoes.PerformLayout();
            this.panelListaMovimentacoes.ResumeLayout(false);
            this.panelListaMovimentacoes.PerformLayout();
            // pictureIconeTela — Lucide
            this.pictureIconeTela.Name = "pictureIconeTela";
            this.pictureIconeTela.Image = global::CASE.Properties.Resources.LucideAzulRegistros;
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

        private System.Windows.Forms.Label lblTituloMovimentacoes;
        private System.Windows.Forms.Label lblSubtituloMovimentacoes;
        private System.Windows.Forms.Panel panelCardEntradasMes;
        private System.Windows.Forms.Label lblCardEntradasMesTitulo;
        private System.Windows.Forms.Label lblCardEntradasMesValor;
        private System.Windows.Forms.Panel panelCardSaidasMes;
        private System.Windows.Forms.Label lblCardSaidasMesTitulo;
        private System.Windows.Forms.Label lblCardSaidasMesValor;
        private System.Windows.Forms.Panel panelFiltrosMovimentacoes;
        private System.Windows.Forms.ComboBox cmbTipoMovimentacao;
        private System.Windows.Forms.TextBox txtProdutoMovimentacao;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private System.Windows.Forms.Button btnNovaMovimentacao;
        private System.Windows.Forms.Panel panelListaMovimentacoes;
        private System.Windows.Forms.Label lblListaMovimentacoes;
        private System.Windows.Forms.DataGridView dgvMovimentacoes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMovimentacoesColData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMovimentacoesColProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMovimentacoesColTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMovimentacoesColQuantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMovimentacoesColUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMovimentacoesColMotivo;
        private System.Windows.Forms.PictureBox pictureIconeTela;
    }
}


namespace CASE
{
    partial class EstoqueControl
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
            this.lblTituloEstoque = new System.Windows.Forms.Label();
            this.lblSubtituloEstoque = new System.Windows.Forms.Label();
            this.panelCardEstoqueTotal = new System.Windows.Forms.Panel();
            this.lblCardEstoqueTotalTitulo = new System.Windows.Forms.Label();
            this.lblCardEstoqueTotalValor = new System.Windows.Forms.Label();
            this.panelCardEstoqueBaixo = new System.Windows.Forms.Panel();
            this.lblCardEstoqueBaixoTitulo = new System.Windows.Forms.Label();
            this.lblCardEstoqueBaixoValor = new System.Windows.Forms.Label();
            this.panelCardSemEstoque = new System.Windows.Forms.Panel();
            this.lblCardSemEstoqueTitulo = new System.Windows.Forms.Label();
            this.lblCardSemEstoqueValor = new System.Windows.Forms.Label();
            this.panelCardValorEstoque = new System.Windows.Forms.Panel();
            this.lblCardValorEstoqueTitulo = new System.Windows.Forms.Label();
            this.lblCardValorEstoqueValor = new System.Windows.Forms.Label();
            this.panelFiltrosEstoque = new System.Windows.Forms.Panel();
            this.txtPesquisarEstoque = new System.Windows.Forms.TextBox();
            this.cmbCategoriaEstoque = new System.Windows.Forms.ComboBox();
            this.cmbStatusEstoque = new System.Windows.Forms.ComboBox();
            this.btnAtualizarEstoque = new System.Windows.Forms.Button();
            this.panelListaEstoque = new System.Windows.Forms.Panel();
            this.lblListaEstoque = new System.Windows.Forms.Label();
            this.dgvEstoque = new System.Windows.Forms.DataGridView();
            this.dgvEstoqueColCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEstoqueColProduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEstoqueColCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEstoqueColAtual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEstoqueColMinimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEstoqueColIdeal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEstoqueColStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstoque)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).BeginInit();
            this.SuspendLayout();
            this.panelCardEstoqueTotal.SuspendLayout();
            this.panelCardEstoqueBaixo.SuspendLayout();
            this.panelCardSemEstoque.SuspendLayout();
            this.panelCardValorEstoque.SuspendLayout();
            this.panelFiltrosEstoque.SuspendLayout();
            this.panelListaEstoque.SuspendLayout();
            // lblTituloEstoque
            this.lblTituloEstoque.Name = "lblTituloEstoque";
            this.lblTituloEstoque.Location = new System.Drawing.Point(80, 25);
            this.lblTituloEstoque.Size = new System.Drawing.Size(350, 40);
            this.lblTituloEstoque.TabIndex = 0;
            this.lblTituloEstoque.Text = "Estoque";
            this.lblTituloEstoque.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTituloEstoque.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // lblSubtituloEstoque
            this.lblSubtituloEstoque.Name = "lblSubtituloEstoque";
            this.lblSubtituloEstoque.Location = new System.Drawing.Point(82, 68);
            this.lblSubtituloEstoque.Size = new System.Drawing.Size(650, 25);
            this.lblSubtituloEstoque.TabIndex = 1;
            this.lblSubtituloEstoque.Text = "Acompanhe níveis, mínimos e status dos produtos";
            this.lblSubtituloEstoque.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblSubtituloEstoque.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // panelCardEstoqueTotal
            this.panelCardEstoqueTotal.Name = "panelCardEstoqueTotal";
            this.panelCardEstoqueTotal.Location = new System.Drawing.Point(30, 110);
            this.panelCardEstoqueTotal.Size = new System.Drawing.Size(210, 85);
            this.panelCardEstoqueTotal.TabIndex = 2;
            this.panelCardEstoqueTotal.BackColor = System.Drawing.Color.White;
            // lblCardEstoqueTotalTitulo
            this.lblCardEstoqueTotalTitulo.Name = "lblCardEstoqueTotalTitulo";
            this.lblCardEstoqueTotalTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblCardEstoqueTotalTitulo.Size = new System.Drawing.Size(145, 22);
            this.lblCardEstoqueTotalTitulo.TabIndex = 3;
            this.lblCardEstoqueTotalTitulo.Text = "Itens em estoque";
            this.lblCardEstoqueTotalTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCardEstoqueTotalTitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // lblCardEstoqueTotalValor
            this.lblCardEstoqueTotalValor.Name = "lblCardEstoqueTotalValor";
            this.lblCardEstoqueTotalValor.Location = new System.Drawing.Point(15, 34);
            this.lblCardEstoqueTotalValor.Size = new System.Drawing.Size(145, 40);
            this.lblCardEstoqueTotalValor.TabIndex = 4;
            this.lblCardEstoqueTotalValor.Text = "193";
            this.lblCardEstoqueTotalValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblCardEstoqueTotalValor.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelCardEstoqueBaixo
            this.panelCardEstoqueBaixo.Name = "panelCardEstoqueBaixo";
            this.panelCardEstoqueBaixo.Location = new System.Drawing.Point(260, 110);
            this.panelCardEstoqueBaixo.Size = new System.Drawing.Size(210, 85);
            this.panelCardEstoqueBaixo.TabIndex = 5;
            this.panelCardEstoqueBaixo.BackColor = System.Drawing.Color.White;
            // lblCardEstoqueBaixoTitulo
            this.lblCardEstoqueBaixoTitulo.Name = "lblCardEstoqueBaixoTitulo";
            this.lblCardEstoqueBaixoTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblCardEstoqueBaixoTitulo.Size = new System.Drawing.Size(145, 22);
            this.lblCardEstoqueBaixoTitulo.TabIndex = 6;
            this.lblCardEstoqueBaixoTitulo.Text = "Estoque baixo";
            this.lblCardEstoqueBaixoTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCardEstoqueBaixoTitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // lblCardEstoqueBaixoValor
            this.lblCardEstoqueBaixoValor.Name = "lblCardEstoqueBaixoValor";
            this.lblCardEstoqueBaixoValor.Location = new System.Drawing.Point(15, 34);
            this.lblCardEstoqueBaixoValor.Size = new System.Drawing.Size(145, 40);
            this.lblCardEstoqueBaixoValor.TabIndex = 7;
            this.lblCardEstoqueBaixoValor.Text = "1";
            this.lblCardEstoqueBaixoValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblCardEstoqueBaixoValor.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelCardSemEstoque
            this.panelCardSemEstoque.Name = "panelCardSemEstoque";
            this.panelCardSemEstoque.Location = new System.Drawing.Point(490, 110);
            this.panelCardSemEstoque.Size = new System.Drawing.Size(210, 85);
            this.panelCardSemEstoque.TabIndex = 8;
            this.panelCardSemEstoque.BackColor = System.Drawing.Color.White;
            // lblCardSemEstoqueTitulo
            this.lblCardSemEstoqueTitulo.Name = "lblCardSemEstoqueTitulo";
            this.lblCardSemEstoqueTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblCardSemEstoqueTitulo.Size = new System.Drawing.Size(145, 22);
            this.lblCardSemEstoqueTitulo.TabIndex = 9;
            this.lblCardSemEstoqueTitulo.Text = "Sem estoque";
            this.lblCardSemEstoqueTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCardSemEstoqueTitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // lblCardSemEstoqueValor
            this.lblCardSemEstoqueValor.Name = "lblCardSemEstoqueValor";
            this.lblCardSemEstoqueValor.Location = new System.Drawing.Point(15, 34);
            this.lblCardSemEstoqueValor.Size = new System.Drawing.Size(145, 40);
            this.lblCardSemEstoqueValor.TabIndex = 10;
            this.lblCardSemEstoqueValor.Text = "1";
            this.lblCardSemEstoqueValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblCardSemEstoqueValor.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelCardValorEstoque
            this.panelCardValorEstoque.Name = "panelCardValorEstoque";
            this.panelCardValorEstoque.Location = new System.Drawing.Point(720, 110);
            this.panelCardValorEstoque.Size = new System.Drawing.Size(210, 85);
            this.panelCardValorEstoque.TabIndex = 11;
            this.panelCardValorEstoque.BackColor = System.Drawing.Color.White;
            // lblCardValorEstoqueTitulo
            this.lblCardValorEstoqueTitulo.Name = "lblCardValorEstoqueTitulo";
            this.lblCardValorEstoqueTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblCardValorEstoqueTitulo.Size = new System.Drawing.Size(145, 22);
            this.lblCardValorEstoqueTitulo.TabIndex = 12;
            this.lblCardValorEstoqueTitulo.Text = "Valor total";
            this.lblCardValorEstoqueTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCardValorEstoqueTitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // lblCardValorEstoqueValor
            this.lblCardValorEstoqueValor.Name = "lblCardValorEstoqueValor";
            this.lblCardValorEstoqueValor.Location = new System.Drawing.Point(15, 34);
            this.lblCardValorEstoqueValor.Size = new System.Drawing.Size(145, 40);
            this.lblCardValorEstoqueValor.TabIndex = 13;
            this.lblCardValorEstoqueValor.Text = "R$ 445,50";
            this.lblCardValorEstoqueValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblCardValorEstoqueValor.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // panelFiltrosEstoque
            this.panelFiltrosEstoque.Name = "panelFiltrosEstoque";
            this.panelFiltrosEstoque.Location = new System.Drawing.Point(30, 215);
            this.panelFiltrosEstoque.Size = new System.Drawing.Size(910, 75);
            this.panelFiltrosEstoque.TabIndex = 14;
            this.panelFiltrosEstoque.BackColor = System.Drawing.Color.White;
            this.panelFiltrosEstoque.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelFiltrosEstoque.MinimumSize = new System.Drawing.Size(910, 75);
            // txtPesquisarEstoque
            this.txtPesquisarEstoque.Name = "txtPesquisarEstoque";
            this.txtPesquisarEstoque.Location = new System.Drawing.Point(20, 19);
            this.txtPesquisarEstoque.Size = new System.Drawing.Size(250, 25);
            this.txtPesquisarEstoque.TabIndex = 15;
            this.txtPesquisarEstoque.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPesquisarEstoque.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // cmbCategoriaEstoque
            this.cmbCategoriaEstoque.Name = "cmbCategoriaEstoque";
            this.cmbCategoriaEstoque.Location = new System.Drawing.Point(290, 19);
            this.cmbCategoriaEstoque.Size = new System.Drawing.Size(170, 27);
            this.cmbCategoriaEstoque.TabIndex = 16;
            this.cmbCategoriaEstoque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoriaEstoque.Items.AddRange(new object[] { "Todas as categorias", "Bebidas", "Alimentos", "Descartáveis", "Outros" });
            this.cmbCategoriaEstoque.SelectedIndex = 0;
            // cmbStatusEstoque
            this.cmbStatusEstoque.Name = "cmbStatusEstoque";
            this.cmbStatusEstoque.Location = new System.Drawing.Point(480, 19);
            this.cmbStatusEstoque.Size = new System.Drawing.Size(170, 27);
            this.cmbStatusEstoque.TabIndex = 17;
            this.cmbStatusEstoque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusEstoque.Items.AddRange(new object[] { "Todos", "Normal", "Baixo", "Sem estoque" });
            this.cmbStatusEstoque.SelectedIndex = 0;
            // btnAtualizarEstoque
            this.btnAtualizarEstoque.Name = "btnAtualizarEstoque";
            this.btnAtualizarEstoque.Location = new System.Drawing.Point(760, 17);
            this.btnAtualizarEstoque.Size = new System.Drawing.Size(130, 40);
            this.btnAtualizarEstoque.TabIndex = 18;
            this.btnAtualizarEstoque.Text = "Atualizar";
            this.btnAtualizarEstoque.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAtualizarEstoque.ForeColor = System.Drawing.Color.White;
            this.btnAtualizarEstoque.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAtualizarEstoque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizarEstoque.FlatAppearance.BorderSize = 0;
            this.btnAtualizarEstoque.UseVisualStyleBackColor = false;
            this.btnAtualizarEstoque.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizarEstoque.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            // panelListaEstoque
            this.panelListaEstoque.Name = "panelListaEstoque";
            this.panelListaEstoque.Location = new System.Drawing.Point(30, 310);
            this.panelListaEstoque.Size = new System.Drawing.Size(910, 320);
            this.panelListaEstoque.TabIndex = 19;
            this.panelListaEstoque.BackColor = System.Drawing.Color.White;
            this.panelListaEstoque.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelListaEstoque.MinimumSize = new System.Drawing.Size(910, 320);
            // lblListaEstoque
            this.lblListaEstoque.Name = "lblListaEstoque";
            this.lblListaEstoque.Location = new System.Drawing.Point(20, 15);
            this.lblListaEstoque.Size = new System.Drawing.Size(300, 30);
            this.lblListaEstoque.TabIndex = 20;
            this.lblListaEstoque.Text = "Situação do estoque";
            this.lblListaEstoque.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblListaEstoque.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // dgvEstoque
            this.dgvEstoque.Name = "dgvEstoque";
            this.dgvEstoque.Location = new System.Drawing.Point(20, 60);
            this.dgvEstoque.Size = new System.Drawing.Size(870, 220);
            this.dgvEstoque.TabIndex = 21;
            this.dgvEstoque.AllowUserToAddRows = false;
            this.dgvEstoque.AllowUserToDeleteRows = false;
            this.dgvEstoque.AllowUserToResizeRows = false;
            this.dgvEstoque.ReadOnly = true;
            this.dgvEstoque.RowHeadersVisible = false;
            this.dgvEstoque.MultiSelect = false;
            this.dgvEstoque.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEstoque.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEstoque.BackgroundColor = System.Drawing.Color.White;
            this.dgvEstoque.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEstoque.EnableHeadersVisualStyles = false;
            this.dgvEstoque.ColumnHeadersHeight = 42;
            this.dgvEstoque.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEstoque.RowTemplate.Height = 48;
            this.dgvEstoque.GridColor = System.Drawing.Color.FromArgb(230, 235, 242);
            this.dgvEstoque.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            estiloCabecalho.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            estiloCabecalho.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            estiloCabecalho.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            estiloCabecalho.SelectionBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            estiloCabecalho.SelectionForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            this.dgvEstoque.ColumnHeadersDefaultCellStyle = estiloCabecalho;
            estiloCelulas.BackColor = System.Drawing.Color.White;
            estiloCelulas.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            estiloCelulas.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            estiloCelulas.SelectionForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            this.dgvEstoque.DefaultCellStyle = estiloCelulas;
            // dgvEstoqueColCodigo
            this.dgvEstoqueColCodigo.Name = "dgvEstoqueColCodigo";
            this.dgvEstoqueColCodigo.HeaderText = "Código";
            this.dgvEstoqueColCodigo.ReadOnly = true;
            this.dgvEstoqueColCodigo.FillWeight = 65F;
            // dgvEstoqueColProduto
            this.dgvEstoqueColProduto.Name = "dgvEstoqueColProduto";
            this.dgvEstoqueColProduto.HeaderText = "Produto";
            this.dgvEstoqueColProduto.ReadOnly = true;
            this.dgvEstoqueColProduto.FillWeight = 180F;
            // dgvEstoqueColCategoria
            this.dgvEstoqueColCategoria.Name = "dgvEstoqueColCategoria";
            this.dgvEstoqueColCategoria.HeaderText = "Categoria";
            this.dgvEstoqueColCategoria.ReadOnly = true;
            this.dgvEstoqueColCategoria.FillWeight = 100F;
            // dgvEstoqueColAtual
            this.dgvEstoqueColAtual.Name = "dgvEstoqueColAtual";
            this.dgvEstoqueColAtual.HeaderText = "Estoque Atual";
            this.dgvEstoqueColAtual.ReadOnly = true;
            this.dgvEstoqueColAtual.FillWeight = 85F;
            // dgvEstoqueColMinimo
            this.dgvEstoqueColMinimo.Name = "dgvEstoqueColMinimo";
            this.dgvEstoqueColMinimo.HeaderText = "Estoque Mínimo";
            this.dgvEstoqueColMinimo.ReadOnly = true;
            this.dgvEstoqueColMinimo.FillWeight = 85F;
            // dgvEstoqueColIdeal
            this.dgvEstoqueColIdeal.Name = "dgvEstoqueColIdeal";
            this.dgvEstoqueColIdeal.HeaderText = "Estoque Ideal";
            this.dgvEstoqueColIdeal.ReadOnly = true;
            this.dgvEstoqueColIdeal.FillWeight = 85F;
            // dgvEstoqueColStatus
            this.dgvEstoqueColStatus.Name = "dgvEstoqueColStatus";
            this.dgvEstoqueColStatus.HeaderText = "Status";
            this.dgvEstoqueColStatus.ReadOnly = true;
            this.dgvEstoqueColStatus.FillWeight = 100F;
            this.Controls.Add(this.lblTituloEstoque);
            this.Controls.Add(this.lblSubtituloEstoque);
            this.Controls.Add(this.panelCardEstoqueTotal);
            this.panelCardEstoqueTotal.Controls.Add(this.lblCardEstoqueTotalTitulo);
            this.panelCardEstoqueTotal.Controls.Add(this.lblCardEstoqueTotalValor);
            this.Controls.Add(this.panelCardEstoqueBaixo);
            this.panelCardEstoqueBaixo.Controls.Add(this.lblCardEstoqueBaixoTitulo);
            this.panelCardEstoqueBaixo.Controls.Add(this.lblCardEstoqueBaixoValor);
            this.Controls.Add(this.panelCardSemEstoque);
            this.panelCardSemEstoque.Controls.Add(this.lblCardSemEstoqueTitulo);
            this.panelCardSemEstoque.Controls.Add(this.lblCardSemEstoqueValor);
            this.Controls.Add(this.panelCardValorEstoque);
            this.panelCardValorEstoque.Controls.Add(this.lblCardValorEstoqueTitulo);
            this.panelCardValorEstoque.Controls.Add(this.lblCardValorEstoqueValor);
            this.Controls.Add(this.panelFiltrosEstoque);
            this.panelFiltrosEstoque.Controls.Add(this.txtPesquisarEstoque);
            this.panelFiltrosEstoque.Controls.Add(this.cmbCategoriaEstoque);
            this.panelFiltrosEstoque.Controls.Add(this.cmbStatusEstoque);
            this.panelFiltrosEstoque.Controls.Add(this.btnAtualizarEstoque);
            this.Controls.Add(this.panelListaEstoque);
            this.panelListaEstoque.Controls.Add(this.lblListaEstoque);
            this.panelListaEstoque.Controls.Add(this.dgvEstoque);
            this.dgvEstoque.Columns.Add(this.dgvEstoqueColCodigo);
            this.dgvEstoque.Columns.Add(this.dgvEstoqueColProduto);
            this.dgvEstoque.Columns.Add(this.dgvEstoqueColCategoria);
            this.dgvEstoque.Columns.Add(this.dgvEstoqueColAtual);
            this.dgvEstoque.Columns.Add(this.dgvEstoqueColMinimo);
            this.dgvEstoque.Columns.Add(this.dgvEstoqueColIdeal);
            this.dgvEstoque.Columns.Add(this.dgvEstoqueColStatus);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor = System.Drawing.Color.FromArgb(243, 247, 252);
            this.Name = "EstoqueControl";
            this.Size = new System.Drawing.Size(970, 680);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(970, 660);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstoque)).EndInit();
            this.panelCardEstoqueTotal.ResumeLayout(false);
            this.panelCardEstoqueTotal.PerformLayout();
            this.panelCardEstoqueBaixo.ResumeLayout(false);
            this.panelCardEstoqueBaixo.PerformLayout();
            this.panelCardSemEstoque.ResumeLayout(false);
            this.panelCardSemEstoque.PerformLayout();
            this.panelCardValorEstoque.ResumeLayout(false);
            this.panelCardValorEstoque.PerformLayout();
            this.panelFiltrosEstoque.ResumeLayout(false);
            this.panelFiltrosEstoque.PerformLayout();
            this.panelListaEstoque.ResumeLayout(false);
            this.panelListaEstoque.PerformLayout();
            // pictureIconeTela — Lucide
            this.pictureIconeTela.Name = "pictureIconeTela";
            this.pictureIconeTela.Image = global::CASE.Properties.Resources.LucideAzulEstoque;
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

        private System.Windows.Forms.Label lblTituloEstoque;
        private System.Windows.Forms.Label lblSubtituloEstoque;
        private System.Windows.Forms.Panel panelCardEstoqueTotal;
        private System.Windows.Forms.Label lblCardEstoqueTotalTitulo;
        private System.Windows.Forms.Label lblCardEstoqueTotalValor;
        private System.Windows.Forms.Panel panelCardEstoqueBaixo;
        private System.Windows.Forms.Label lblCardEstoqueBaixoTitulo;
        private System.Windows.Forms.Label lblCardEstoqueBaixoValor;
        private System.Windows.Forms.Panel panelCardSemEstoque;
        private System.Windows.Forms.Label lblCardSemEstoqueTitulo;
        private System.Windows.Forms.Label lblCardSemEstoqueValor;
        private System.Windows.Forms.Panel panelCardValorEstoque;
        private System.Windows.Forms.Label lblCardValorEstoqueTitulo;
        private System.Windows.Forms.Label lblCardValorEstoqueValor;
        private System.Windows.Forms.Panel panelFiltrosEstoque;
        private System.Windows.Forms.TextBox txtPesquisarEstoque;
        private System.Windows.Forms.ComboBox cmbCategoriaEstoque;
        private System.Windows.Forms.ComboBox cmbStatusEstoque;
        private System.Windows.Forms.Button btnAtualizarEstoque;
        private System.Windows.Forms.Panel panelListaEstoque;
        private System.Windows.Forms.Label lblListaEstoque;
        private System.Windows.Forms.DataGridView dgvEstoque;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEstoqueColCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEstoqueColProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEstoqueColCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEstoqueColAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEstoqueColMinimo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEstoqueColIdeal;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEstoqueColStatus;
        private System.Windows.Forms.PictureBox pictureIconeTela;
    }
}


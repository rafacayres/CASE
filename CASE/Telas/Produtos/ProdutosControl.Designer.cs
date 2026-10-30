namespace CASE
{
    partial class ProdutosControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        private void InitializeComponent()
        {
            this.pictureIconeTela = new System.Windows.Forms.PictureBox();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTituloProdutos = new System.Windows.Forms.Label();
            this.lblSubtituloProdutos = new System.Windows.Forms.Label();
            this.panelAcoesProdutos = new System.Windows.Forms.Panel();
            this.txtPesquisarProduto = new System.Windows.Forms.TextBox();
            this.cmbFiltroCategoria = new System.Windows.Forms.ComboBox();
            this.btnNovoProduto = new System.Windows.Forms.Button();
            this.panelListaProdutos = new System.Windows.Forms.Panel();
            this.lblListaProdutos = new System.Windows.Forms.Label();
            this.dgvProdutos = new System.Windows.Forms.DataGridView();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAcoes = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblPaginacao = new System.Windows.Forms.Label();
            this.btnPaginaAnterior = new System.Windows.Forms.Button();
            this.btnProximaPagina = new System.Windows.Forms.Button();
            this.panelAcoesProdutos.SuspendLayout();
            this.panelListaProdutos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTituloProdutos
            // 
            this.lblTituloProdutos.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloProdutos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblTituloProdutos.Location = new System.Drawing.Point(80, 25);
            this.lblTituloProdutos.Name = "lblTituloProdutos";
            this.lblTituloProdutos.Size = new System.Drawing.Size(300, 40);
            this.lblTituloProdutos.TabIndex = 0;
            this.lblTituloProdutos.Text = "Produtos";
            // 
            // lblSubtituloProdutos
            // 
            this.lblSubtituloProdutos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtituloProdutos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtituloProdutos.Location = new System.Drawing.Point(82, 68);
            this.lblSubtituloProdutos.Name = "lblSubtituloProdutos";
            this.lblSubtituloProdutos.Size = new System.Drawing.Size(600, 25);
            this.lblSubtituloProdutos.TabIndex = 1;
            this.lblSubtituloProdutos.Text = "Gerencie os produtos cadastrados no estoque";
            // 
            // panelAcoesProdutos
            // 
            this.panelAcoesProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAcoesProdutos.BackColor = System.Drawing.Color.White;
            this.panelAcoesProdutos.Controls.Add(this.txtPesquisarProduto);
            this.panelAcoesProdutos.Controls.Add(this.cmbFiltroCategoria);
            this.panelAcoesProdutos.Controls.Add(this.btnNovoProduto);
            this.panelAcoesProdutos.Location = new System.Drawing.Point(30, 115);
            this.panelAcoesProdutos.Name = "panelAcoesProdutos";
            this.panelAcoesProdutos.MinimumSize = new System.Drawing.Size(910, 75);
            this.panelAcoesProdutos.Size = new System.Drawing.Size(910, 75);
            this.panelAcoesProdutos.TabIndex = 2;
            // 
            // txtPesquisarProduto
            // 
            this.txtPesquisarProduto.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtPesquisarProduto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(253)))));
            this.txtPesquisarProduto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPesquisarProduto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPesquisarProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.txtPesquisarProduto.Location = new System.Drawing.Point(20, 19);
            this.txtPesquisarProduto.Name = "txtPesquisarProduto";
            this.txtPesquisarProduto.Size = new System.Drawing.Size(440, 25);
            this.txtPesquisarProduto.TabIndex = 0;
            // 
            // cmbFiltroCategoria
            // 
            this.cmbFiltroCategoria.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.cmbFiltroCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroCategoria.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbFiltroCategoria.FormattingEnabled = true;
            this.cmbFiltroCategoria.Items.AddRange(new object[] {
            "Todas as categorias",
            "Bebidas",
            "Alimentos",
            "Descartáveis",
            "Outros"});
            this.cmbFiltroCategoria.Location = new System.Drawing.Point(480, 19);
            this.cmbFiltroCategoria.Name = "cmbFiltroCategoria";
            this.cmbFiltroCategoria.Size = new System.Drawing.Size(190, 25);
            this.cmbFiltroCategoria.TabIndex = 1;
            // 
            // btnNovoProduto
            // 
            this.btnNovoProduto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovoProduto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnNovoProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovoProduto.FlatAppearance.BorderSize = 0;
            this.btnNovoProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovoProduto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNovoProduto.ForeColor = System.Drawing.Color.White;
            this.btnNovoProduto.Location = new System.Drawing.Point(690, 17);
            this.btnNovoProduto.Name = "btnNovoProduto";
            this.btnNovoProduto.Size = new System.Drawing.Size(200, 40);
            this.btnNovoProduto.TabIndex = 2;
            this.btnNovoProduto.Text = "+ Novo Produto";
            this.btnNovoProduto.UseVisualStyleBackColor = false;
            // 
            // panelListaProdutos
            // 
            this.panelListaProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelListaProdutos.BackColor = System.Drawing.Color.White;
            this.panelListaProdutos.Controls.Add(this.lblListaProdutos);
            this.panelListaProdutos.Controls.Add(this.dgvProdutos);
            this.panelListaProdutos.Controls.Add(this.lblPaginacao);
            this.panelListaProdutos.Controls.Add(this.btnPaginaAnterior);
            this.panelListaProdutos.Controls.Add(this.btnProximaPagina);
            this.panelListaProdutos.Location = new System.Drawing.Point(30, 210);
            this.panelListaProdutos.Name = "panelListaProdutos";
            this.panelListaProdutos.MinimumSize = new System.Drawing.Size(910, 400);
            this.panelListaProdutos.Size = new System.Drawing.Size(910, 400);
            this.panelListaProdutos.TabIndex = 3;
            // 
            // lblListaProdutos
            // 
            this.lblListaProdutos.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblListaProdutos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblListaProdutos.Location = new System.Drawing.Point(20, 15);
            this.lblListaProdutos.Name = "lblListaProdutos";
            this.lblListaProdutos.Size = new System.Drawing.Size(300, 30);
            this.lblListaProdutos.TabIndex = 0;
            this.lblListaProdutos.Text = "Lista de produtos";
            // 
            // dgvProdutos
            // 
            this.dgvProdutos.AllowUserToAddRows = false;
            this.dgvProdutos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.dgvProdutos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProdutos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProdutos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProdutos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProdutos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProdutos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProdutos.ColumnHeadersHeight = 42;
            this.dgvProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProdutos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigo,
            this.colProduto,
            this.colCategoria,
            this.colQuantidade,
            this.colPreco,
            this.colAcoes});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProdutos.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvProdutos.EnableHeadersVisualStyles = false;
            this.dgvProdutos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(242)))));
            this.dgvProdutos.Location = new System.Drawing.Point(20, 60);
            this.dgvProdutos.MultiSelect = false;
            this.dgvProdutos.Name = "dgvProdutos";
            this.dgvProdutos.ReadOnly = true;
            this.dgvProdutos.RowHeadersVisible = false;
            this.dgvProdutos.RowTemplate.Height = 48;
            this.dgvProdutos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProdutos.Size = new System.Drawing.Size(870, 280);
            this.dgvProdutos.TabIndex = 1;
            // 
            // colCodigo
            // 
            this.colCodigo.HeaderText = "Código";
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.ReadOnly = true;
            // 
            // colProduto
            // 
            this.colProduto.HeaderText = "Produto";
            this.colProduto.Name = "colProduto";
            this.colProduto.ReadOnly = true;
            // 
            // colCategoria
            // 
            this.colCategoria.HeaderText = "Categoria";
            this.colCategoria.Name = "colCategoria";
            this.colCategoria.ReadOnly = true;
            // 
            // colQuantidade
            // 
            this.colQuantidade.HeaderText = "Estoque";
            this.colQuantidade.Name = "colQuantidade";
            this.colQuantidade.ReadOnly = true;
            // 
            // colPreco
            // 
            this.colPreco.HeaderText = "Preço";
            this.colPreco.Name = "colPreco";
            this.colPreco.ReadOnly = true;
            // 
            // colAcoes
            // 
            this.colAcoes.DefaultCellStyle = dataGridViewCellStyle1;
            this.colAcoes.FillWeight = 115F;
            this.colAcoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colAcoes.HeaderText = "Ações";
            this.colAcoes.Name = "colAcoes";
            this.colAcoes.ReadOnly = true;
            this.colAcoes.Text = "Editar / Excluir";
            this.colAcoes.UseColumnTextForButtonValue = true;
            // 
            // lblPaginacao
            // 
            this.lblPaginacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPaginacao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPaginacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPaginacao.Location = new System.Drawing.Point(20, 355);
            this.lblPaginacao.Name = "lblPaginacao";
            this.lblPaginacao.Size = new System.Drawing.Size(350, 25);
            this.lblPaginacao.TabIndex = 2;
            this.lblPaginacao.Text = "Exibindo 1 a 3 de 3 produtos";
            // 
            // btnPaginaAnterior
            // 
            this.btnPaginaAnterior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPaginaAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(253)))));
            this.btnPaginaAnterior.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnPaginaAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPaginaAnterior.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPaginaAnterior.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.btnPaginaAnterior.Location = new System.Drawing.Point(665, 350);
            this.btnPaginaAnterior.Name = "btnPaginaAnterior";
            this.btnPaginaAnterior.Size = new System.Drawing.Size(95, 30);
            this.btnPaginaAnterior.TabIndex = 3;
            this.btnPaginaAnterior.Text = "Anterior";
            this.btnPaginaAnterior.UseVisualStyleBackColor = false;
            // 
            // btnProximaPagina
            // 
            this.btnProximaPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProximaPagina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(253)))));
            this.btnProximaPagina.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnProximaPagina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProximaPagina.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnProximaPagina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.btnProximaPagina.Location = new System.Drawing.Point(770, 350);
            this.btnProximaPagina.Name = "btnProximaPagina";
            this.btnProximaPagina.Size = new System.Drawing.Size(95, 30);
            this.btnProximaPagina.TabIndex = 4;
            this.btnProximaPagina.Text = "Próximo";
            this.btnProximaPagina.UseVisualStyleBackColor = false;
            // 
            // ProdutosControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.lblTituloProdutos);
            this.Controls.Add(this.lblSubtituloProdutos);
            this.Controls.Add(this.panelAcoesProdutos);
            this.Controls.Add(this.panelListaProdutos);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ProdutosControl";
            this.Size = new System.Drawing.Size(970, 680);
            this.AutoScrollMinSize = new System.Drawing.Size(970, 660);
            this.panelAcoesProdutos.ResumeLayout(false);
            this.panelAcoesProdutos.PerformLayout();
            this.panelListaProdutos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).EndInit();
            // pictureIconeTela — Lucide
            this.pictureIconeTela.Name = "pictureIconeTela";
            this.pictureIconeTela.Image = global::CASE.Properties.Resources.LucideAzulProdutos;
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

        private System.Windows.Forms.Label lblTituloProdutos;
        private System.Windows.Forms.Label lblSubtituloProdutos;
        private System.Windows.Forms.Panel panelAcoesProdutos;
        private System.Windows.Forms.TextBox txtPesquisarProduto;
        private System.Windows.Forms.ComboBox cmbFiltroCategoria;
        private System.Windows.Forms.Button btnNovoProduto;
        private System.Windows.Forms.Panel panelListaProdutos;
        private System.Windows.Forms.Label lblListaProdutos;
        private System.Windows.Forms.DataGridView dgvProdutos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPreco;
        private System.Windows.Forms.DataGridViewButtonColumn colAcoes;
        private System.Windows.Forms.Label lblPaginacao;
        private System.Windows.Forms.Button btnPaginaAnterior;
        private System.Windows.Forms.Button btnProximaPagina;
        private System.Windows.Forms.PictureBox pictureIconeTela;
    }
}

namespace CASE.Quermesse
{
    partial class SolicitacoesControl
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.panelCorpo = new System.Windows.Forms.Panel();
            this.panelFiltrosSolicitacoes = new System.Windows.Forms.Panel();
            this.lblSetor = new System.Windows.Forms.Label();
            this.cmbFiltroSolicitacoes = new System.Windows.Forms.ComboBox();
            this.lblSituacao = new System.Windows.Forms.Label();
            this.cmbStatusListaSolicitacoes = new System.Windows.Forms.ComboBox();
            this.btnNovaSolicitacao = new System.Windows.Forms.Button();
            this.panelListaSolicitacoes = new System.Windows.Forms.Panel();
            this.lblAcompanhamentodospedidos = new System.Windows.Forms.Label();
            this.btnDetalhesSolicitacao = new System.Windows.Forms.Button();
            this.dgvSolicitacoesQuermesse = new System.Windows.Forms.DataGridView();
            this.colSolicitacoesQuermesseDatahora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSolicitacoesQuermesseSolicitante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSolicitacoesQuermesseSetor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSolicitacoesQuermesseTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSolicitacoesQuermesseProduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSolicitacoesQuermesseQtd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSolicitacoesQuermesseSituacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelAviso = new System.Windows.Forms.Label();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).BeginInit();
            this.panelCorpo.SuspendLayout();
            this.panelFiltrosSolicitacoes.SuspendLayout();
            this.panelListaSolicitacoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitacoesQuermesse)).BeginInit();
            // pictureIconeTela
            this.pictureIconeTela.Name = "pictureIconeTela";
            this.pictureIconeTela.Location = new System.Drawing.Point(30, 28);
            this.pictureIconeTela.Size = new System.Drawing.Size(36, 36);
            this.pictureIconeTela.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.pictureIconeTela.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.pictureIconeTela.BackColor = System.Drawing.Color.FromArgb(255, 232, 240, 252);
            this.pictureIconeTela.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.pictureIconeTela.TabIndex = 0;
            this.pictureIconeTela.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureIconeTela.Image = global::CASE.Properties.Resources.LucideAzulSolicitacoes;
            this.pictureIconeTela.TabStop = false;
            // lblTitulo
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Location = new System.Drawing.Point(80, 22);
            this.lblTitulo.Size = new System.Drawing.Size(850, 42);
            this.lblTitulo.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, (System.Drawing.FontStyle)1);
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(255, 246, 248, 252);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(255, 16, 43, 73);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Solicitações e ocorrências";
            this.lblTitulo.TextAlign = (System.Drawing.ContentAlignment)1;
            // lblSubtitulo
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Location = new System.Drawing.Point(82, 66);
            this.lblSubtitulo.Size = new System.Drawing.Size(880, 24);
            this.lblSubtitulo.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblSubtitulo.BackColor = System.Drawing.Color.FromArgb(255, 246, 248, 252);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Pedidos de reposição e comunicação entre as barracas e a organização";
            this.lblSubtitulo.TextAlign = (System.Drawing.ContentAlignment)1;
            // panelCorpo
            this.panelCorpo.Name = "panelCorpo";
            this.panelCorpo.Location = new System.Drawing.Point(30, 112);
            this.panelCorpo.Size = new System.Drawing.Size(990, 530);
            this.panelCorpo.Anchor = (System.Windows.Forms.AnchorStyles)13;
            this.panelCorpo.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.panelCorpo.BackColor = System.Drawing.Color.FromArgb(255, 246, 248, 252);
            this.panelCorpo.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.panelCorpo.TabIndex = 3;
            // panelFiltrosSolicitacoes
            this.panelFiltrosSolicitacoes.Name = "panelFiltrosSolicitacoes";
            this.panelFiltrosSolicitacoes.Location = new System.Drawing.Point(0, 0);
            this.panelFiltrosSolicitacoes.Size = new System.Drawing.Size(990, 95);
            this.panelFiltrosSolicitacoes.Anchor = (System.Windows.Forms.AnchorStyles)13;
            this.panelFiltrosSolicitacoes.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.panelFiltrosSolicitacoes.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.panelFiltrosSolicitacoes.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.panelFiltrosSolicitacoes.TabIndex = 0;
            // lblSetor
            this.lblSetor.Name = "lblSetor";
            this.lblSetor.Location = new System.Drawing.Point(18, 12);
            this.lblSetor.Size = new System.Drawing.Size(250, 24);
            this.lblSetor.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSetor.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)0);
            this.lblSetor.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblSetor.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSetor.TabIndex = 0;
            this.lblSetor.Text = "Setor";
            this.lblSetor.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbFiltroSolicitacoes
            this.cmbFiltroSolicitacoes.Name = "cmbFiltroSolicitacoes";
            this.cmbFiltroSolicitacoes.Location = new System.Drawing.Point(18, 44);
            this.cmbFiltroSolicitacoes.Size = new System.Drawing.Size(280, 29);
            this.cmbFiltroSolicitacoes.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbFiltroSolicitacoes.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbFiltroSolicitacoes.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbFiltroSolicitacoes.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbFiltroSolicitacoes.TabIndex = 1;
            this.cmbFiltroSolicitacoes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroSolicitacoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFiltroSolicitacoes.Items.AddRange(new object[] { "Todos os setores", "Bebidas", "Alimentação", "Doces", "Brincadeiras" });
            this.cmbFiltroSolicitacoes.SelectedIndex = 0;
            // lblSituacao
            this.lblSituacao.Name = "lblSituacao";
            this.lblSituacao.Location = new System.Drawing.Point(320, 12);
            this.lblSituacao.Size = new System.Drawing.Size(200, 24);
            this.lblSituacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSituacao.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)0);
            this.lblSituacao.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblSituacao.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSituacao.TabIndex = 2;
            this.lblSituacao.Text = "Situação";
            this.lblSituacao.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbStatusListaSolicitacoes
            this.cmbStatusListaSolicitacoes.Name = "cmbStatusListaSolicitacoes";
            this.cmbStatusListaSolicitacoes.Location = new System.Drawing.Point(320, 44);
            this.cmbStatusListaSolicitacoes.Size = new System.Drawing.Size(220, 29);
            this.cmbStatusListaSolicitacoes.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbStatusListaSolicitacoes.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbStatusListaSolicitacoes.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbStatusListaSolicitacoes.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbStatusListaSolicitacoes.TabIndex = 3;
            this.cmbStatusListaSolicitacoes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusListaSolicitacoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatusListaSolicitacoes.Items.AddRange(new object[] { "Todas", "Pendente", "Em atendimento", "Concluída" });
            this.cmbStatusListaSolicitacoes.SelectedIndex = 0;
            // btnNovaSolicitacao
            this.btnNovaSolicitacao.Name = "btnNovaSolicitacao";
            this.btnNovaSolicitacao.Location = new System.Drawing.Point(735, 34);
            this.btnNovaSolicitacao.Size = new System.Drawing.Size(235, 40);
            this.btnNovaSolicitacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.btnNovaSolicitacao.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)1);
            this.btnNovaSolicitacao.BackColor = System.Drawing.Color.FromArgb(255, 23, 105, 210);
            this.btnNovaSolicitacao.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnNovaSolicitacao.TabIndex = 4;
            this.btnNovaSolicitacao.Text = "+ Nova solicitação";
            this.btnNovaSolicitacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovaSolicitacao.FlatAppearance.BorderSize = 0;
            this.btnNovaSolicitacao.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 228, 234, 242);
            this.btnNovaSolicitacao.UseVisualStyleBackColor = false;
            this.btnNovaSolicitacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovaSolicitacao.DialogResult = (System.Windows.Forms.DialogResult)0;
            // panelListaSolicitacoes
            this.panelListaSolicitacoes.Name = "panelListaSolicitacoes";
            this.panelListaSolicitacoes.Location = new System.Drawing.Point(0, 115);
            this.panelListaSolicitacoes.Size = new System.Drawing.Size(990, 415);
            this.panelListaSolicitacoes.Anchor = (System.Windows.Forms.AnchorStyles)13;
            this.panelListaSolicitacoes.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.panelListaSolicitacoes.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.panelListaSolicitacoes.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.panelListaSolicitacoes.TabIndex = 1;
            // lblAcompanhamentodospedidos
            this.lblAcompanhamentodospedidos.Name = "lblAcompanhamentodospedidos";
            this.lblAcompanhamentodospedidos.Location = new System.Drawing.Point(18, 14);
            this.lblAcompanhamentodospedidos.Size = new System.Drawing.Size(580, 28);
            this.lblAcompanhamentodospedidos.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblAcompanhamentodospedidos.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)1);
            this.lblAcompanhamentodospedidos.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblAcompanhamentodospedidos.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.lblAcompanhamentodospedidos.TabIndex = 0;
            this.lblAcompanhamentodospedidos.Text = "Acompanhamento dos pedidos";
            this.lblAcompanhamentodospedidos.TextAlign = (System.Drawing.ContentAlignment)1;
            // btnDetalhesSolicitacao
            this.btnDetalhesSolicitacao.Name = "btnDetalhesSolicitacao";
            this.btnDetalhesSolicitacao.Location = new System.Drawing.Point(735, 12);
            this.btnDetalhesSolicitacao.Size = new System.Drawing.Size(235, 36);
            this.btnDetalhesSolicitacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.btnDetalhesSolicitacao.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)0);
            this.btnDetalhesSolicitacao.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnDetalhesSolicitacao.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.btnDetalhesSolicitacao.TabIndex = 1;
            this.btnDetalhesSolicitacao.Text = "Ver exemplo";
            this.btnDetalhesSolicitacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetalhesSolicitacao.FlatAppearance.BorderSize = 1;
            this.btnDetalhesSolicitacao.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 228, 234, 242);
            this.btnDetalhesSolicitacao.UseVisualStyleBackColor = false;
            this.btnDetalhesSolicitacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetalhesSolicitacao.DialogResult = (System.Windows.Forms.DialogResult)0;
            // dgvSolicitacoesQuermesse
            this.dgvSolicitacoesQuermesse.Name = "dgvSolicitacoesQuermesse";
            this.dgvSolicitacoesQuermesse.Location = new System.Drawing.Point(12, 64);
            this.dgvSolicitacoesQuermesse.Size = new System.Drawing.Size(966, 335);
            this.dgvSolicitacoesQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)15;
            this.dgvSolicitacoesQuermesse.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.dgvSolicitacoesQuermesse.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.dgvSolicitacoesQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.dgvSolicitacoesQuermesse.TabIndex = 2;
            this.dgvSolicitacoesQuermesse.AllowUserToAddRows = false;
            this.dgvSolicitacoesQuermesse.AllowUserToDeleteRows = false;
            this.dgvSolicitacoesQuermesse.ReadOnly = true;
            this.dgvSolicitacoesQuermesse.RowHeadersVisible = false;
            this.dgvSolicitacoesQuermesse.MultiSelect = false;
            this.dgvSolicitacoesQuermesse.EnableHeadersVisualStyles = false;
            this.dgvSolicitacoesQuermesse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSolicitacoesQuermesse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSolicitacoesQuermesse.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSolicitacoesQuermesse.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSolicitacoesQuermesse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSolicitacoesQuermesse.BackgroundColor = System.Drawing.Color.White;
            this.dgvSolicitacoesQuermesse.GridColor = System.Drawing.Color.FromArgb(255, 240, 243, 248);
            this.dgvSolicitacoesQuermesse.RowTemplate.Height = 50;
            this.dgvSolicitacoesQuermesse.ColumnHeadersHeight = 42;
            this.dgvSolicitacoesQuermesse.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 248, 250, 253);
            this.dgvSolicitacoesQuermesse.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.colSolicitacoesQuermesseDatahora.Name = "colSolicitacoesQuermesseDatahora";
            this.colSolicitacoesQuermesseDatahora.HeaderText = "Data / hora";
            this.colSolicitacoesQuermesseDatahora.ReadOnly = true;
            this.colSolicitacoesQuermesseSolicitante.Name = "colSolicitacoesQuermesseSolicitante";
            this.colSolicitacoesQuermesseSolicitante.HeaderText = "Solicitante";
            this.colSolicitacoesQuermesseSolicitante.ReadOnly = true;
            this.colSolicitacoesQuermesseSetor.Name = "colSolicitacoesQuermesseSetor";
            this.colSolicitacoesQuermesseSetor.HeaderText = "Setor";
            this.colSolicitacoesQuermesseSetor.ReadOnly = true;
            this.colSolicitacoesQuermesseTipo.Name = "colSolicitacoesQuermesseTipo";
            this.colSolicitacoesQuermesseTipo.HeaderText = "Tipo";
            this.colSolicitacoesQuermesseTipo.ReadOnly = true;
            this.colSolicitacoesQuermesseProduto.Name = "colSolicitacoesQuermesseProduto";
            this.colSolicitacoesQuermesseProduto.HeaderText = "Produto";
            this.colSolicitacoesQuermesseProduto.ReadOnly = true;
            this.colSolicitacoesQuermesseQtd.Name = "colSolicitacoesQuermesseQtd";
            this.colSolicitacoesQuermesseQtd.HeaderText = "Qtd.";
            this.colSolicitacoesQuermesseQtd.ReadOnly = true;
            this.colSolicitacoesQuermesseSituacao.Name = "colSolicitacoesQuermesseSituacao";
            this.colSolicitacoesQuermesseSituacao.HeaderText = "Situação";
            this.colSolicitacoesQuermesseSituacao.ReadOnly = true;
            this.dgvSolicitacoesQuermesse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colSolicitacoesQuermesseDatahora, this.colSolicitacoesQuermesseSolicitante, this.colSolicitacoesQuermesseSetor, this.colSolicitacoesQuermesseTipo, this.colSolicitacoesQuermesseProduto, this.colSolicitacoesQuermesseQtd, this.colSolicitacoesQuermesseSituacao });
            // labelAviso
            this.labelAviso.Name = "labelAviso";
            this.labelAviso.Location = new System.Drawing.Point(32, 652);
            this.labelAviso.Size = new System.Drawing.Size(900, 28);
            this.labelAviso.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.labelAviso.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.labelAviso.BackColor = System.Drawing.Color.FromArgb(255, 246, 248, 252);
            this.labelAviso.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.labelAviso.TabIndex = 4;
            this.labelAviso.Text = "";
            this.labelAviso.TextAlign = (System.Drawing.ContentAlignment)1;
            this.Controls.Add(this.pictureIconeTela);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.panelCorpo);
            this.panelCorpo.Controls.Add(this.panelFiltrosSolicitacoes);
            this.panelFiltrosSolicitacoes.Controls.Add(this.lblSetor);
            this.panelFiltrosSolicitacoes.Controls.Add(this.cmbFiltroSolicitacoes);
            this.panelFiltrosSolicitacoes.Controls.Add(this.lblSituacao);
            this.panelFiltrosSolicitacoes.Controls.Add(this.cmbStatusListaSolicitacoes);
            this.panelFiltrosSolicitacoes.Controls.Add(this.btnNovaSolicitacao);
            this.panelCorpo.Controls.Add(this.panelListaSolicitacoes);
            this.panelListaSolicitacoes.Controls.Add(this.lblAcompanhamentodospedidos);
            this.panelListaSolicitacoes.Controls.Add(this.btnDetalhesSolicitacao);
            this.panelListaSolicitacoes.Controls.Add(this.dgvSolicitacoesQuermesse);
            this.Controls.Add(this.labelAviso);
            this.Name = "SolicitacoesControl";
            this.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.BackColor = System.Drawing.Color.FromArgb(255, 246, 248, 252);
            this.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Size = new System.Drawing.Size(1050, 720);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(970, 690);
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).EndInit();
            this.panelCorpo.ResumeLayout(false);
            this.panelCorpo.PerformLayout();
            this.panelFiltrosSolicitacoes.ResumeLayout(false);
            this.panelFiltrosSolicitacoes.PerformLayout();
            this.panelListaSolicitacoes.ResumeLayout(false);
            this.panelListaSolicitacoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitacoesQuermesse)).EndInit();
            this.btnNovaSolicitacao.Click += new System.EventHandler(this.btnNovaSolicitacao_Click);
            this.btnDetalhesSolicitacao.Click += new System.EventHandler(this.btnDetalhesSolicitacao_Click);
            this.cmbFiltroSolicitacoes.SelectedIndexChanged += new System.EventHandler(this.Filtros_SelectedIndexChanged);
            this.cmbStatusListaSolicitacoes.SelectedIndexChanged += new System.EventHandler(this.Filtros_SelectedIndexChanged);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.PictureBox pictureIconeTela;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelCorpo;
        private System.Windows.Forms.Panel panelFiltrosSolicitacoes;
        private System.Windows.Forms.Label lblSetor;
        private System.Windows.Forms.ComboBox cmbFiltroSolicitacoes;
        private System.Windows.Forms.Label lblSituacao;
        private System.Windows.Forms.ComboBox cmbStatusListaSolicitacoes;
        private System.Windows.Forms.Button btnNovaSolicitacao;
        private System.Windows.Forms.Panel panelListaSolicitacoes;
        private System.Windows.Forms.Label lblAcompanhamentodospedidos;
        private System.Windows.Forms.Button btnDetalhesSolicitacao;
        private System.Windows.Forms.DataGridView dgvSolicitacoesQuermesse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSolicitacoesQuermesseDatahora;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSolicitacoesQuermesseSolicitante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSolicitacoesQuermesseSetor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSolicitacoesQuermesseTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSolicitacoesQuermesseProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSolicitacoesQuermesseQtd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSolicitacoesQuermesseSituacao;
        private System.Windows.Forms.Label labelAviso;
    }
}
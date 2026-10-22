namespace CASE.Quermesse
{
    partial class QuermessesControl
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
            this.panelEventoResumo = new System.Windows.Forms.Panel();
            this.pictureIconeEvento = new System.Windows.Forms.PictureBox();
            this.lblQuermessedeSaoJoao2026 = new System.Windows.Forms.Label();
            this.lblPatiodacomunidadeEmandamento = new System.Windows.Forms.Label();
            this.btnNovaQuermesse = new System.Windows.Forms.Button();
            this.panelListaQuermesses = new System.Windows.Forms.Panel();
            this.lblEventoscadastrados = new System.Windows.Forms.Label();
            this.btnEditarQuermesse = new System.Windows.Forms.Button();
            this.dgvQuermesses = new System.Windows.Forms.DataGridView();
            this.colQuermessesEvento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuermessesLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuermessesInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuermessesFim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuermessesResponsavel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuermessesSituacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelAviso = new System.Windows.Forms.Label();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).BeginInit();
            this.panelCorpo.SuspendLayout();
            this.panelEventoResumo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeEvento)).BeginInit();
            this.panelListaQuermesses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuermesses)).BeginInit();
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
            this.pictureIconeTela.Image = global::CASE.Properties.Resources.LucideAzulQuermesses;
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
            this.lblTitulo.Text = "Quermesses";
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
            this.lblSubtitulo.Text = "Eventos, datas e responsáveis pela organização";
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
            // panelEventoResumo
            this.panelEventoResumo.Name = "panelEventoResumo";
            this.panelEventoResumo.Location = new System.Drawing.Point(0, 0);
            this.panelEventoResumo.Size = new System.Drawing.Size(990, 100);
            this.panelEventoResumo.Anchor = (System.Windows.Forms.AnchorStyles)13;
            this.panelEventoResumo.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.panelEventoResumo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.panelEventoResumo.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.panelEventoResumo.TabIndex = 0;
            // pictureIconeEvento
            this.pictureIconeEvento.Name = "pictureIconeEvento";
            this.pictureIconeEvento.Location = new System.Drawing.Point(20, 26);
            this.pictureIconeEvento.Size = new System.Drawing.Size(38, 38);
            this.pictureIconeEvento.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.pictureIconeEvento.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.pictureIconeEvento.BackColor = System.Drawing.Color.FromArgb(255, 237, 244, 254);
            this.pictureIconeEvento.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.pictureIconeEvento.TabIndex = 0;
            this.pictureIconeEvento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureIconeEvento.Image = global::CASE.Properties.Resources.LucideAzulQuermesses;
            this.pictureIconeEvento.TabStop = false;
            // lblQuermessedeSaoJoao2026
            this.lblQuermessedeSaoJoao2026.Name = "lblQuermessedeSaoJoao2026";
            this.lblQuermessedeSaoJoao2026.Location = new System.Drawing.Point(76, 20);
            this.lblQuermessedeSaoJoao2026.Size = new System.Drawing.Size(620, 28);
            this.lblQuermessedeSaoJoao2026.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblQuermessedeSaoJoao2026.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)1);
            this.lblQuermessedeSaoJoao2026.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblQuermessedeSaoJoao2026.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.lblQuermessedeSaoJoao2026.TabIndex = 1;
            this.lblQuermessedeSaoJoao2026.Text = "Quermesse de São João 2026";
            this.lblQuermessedeSaoJoao2026.TextAlign = (System.Drawing.ContentAlignment)1;
            // lblPatiodacomunidadeEmandamento
            this.lblPatiodacomunidadeEmandamento.Name = "lblPatiodacomunidadeEmandamento";
            this.lblPatiodacomunidadeEmandamento.Location = new System.Drawing.Point(76, 52);
            this.lblPatiodacomunidadeEmandamento.Size = new System.Drawing.Size(620, 24);
            this.lblPatiodacomunidadeEmandamento.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblPatiodacomunidadeEmandamento.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)0);
            this.lblPatiodacomunidadeEmandamento.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblPatiodacomunidadeEmandamento.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblPatiodacomunidadeEmandamento.TabIndex = 2;
            this.lblPatiodacomunidadeEmandamento.Text = "Pátio da comunidade · Em andamento";
            this.lblPatiodacomunidadeEmandamento.TextAlign = (System.Drawing.ContentAlignment)1;
            // btnNovaQuermesse
            this.btnNovaQuermesse.Name = "btnNovaQuermesse";
            this.btnNovaQuermesse.Location = new System.Drawing.Point(750, 28);
            this.btnNovaQuermesse.Size = new System.Drawing.Size(220, 40);
            this.btnNovaQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.btnNovaQuermesse.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)1);
            this.btnNovaQuermesse.BackColor = System.Drawing.Color.FromArgb(255, 23, 105, 210);
            this.btnNovaQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnNovaQuermesse.TabIndex = 3;
            this.btnNovaQuermesse.Text = "+ Nova quermesse";
            this.btnNovaQuermesse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovaQuermesse.FlatAppearance.BorderSize = 0;
            this.btnNovaQuermesse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 228, 234, 242);
            this.btnNovaQuermesse.UseVisualStyleBackColor = false;
            this.btnNovaQuermesse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovaQuermesse.DialogResult = (System.Windows.Forms.DialogResult)0;
            // panelListaQuermesses
            this.panelListaQuermesses.Name = "panelListaQuermesses";
            this.panelListaQuermesses.Location = new System.Drawing.Point(0, 120);
            this.panelListaQuermesses.Size = new System.Drawing.Size(990, 410);
            this.panelListaQuermesses.Anchor = (System.Windows.Forms.AnchorStyles)13;
            this.panelListaQuermesses.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.panelListaQuermesses.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.panelListaQuermesses.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.panelListaQuermesses.TabIndex = 1;
            // lblEventoscadastrados
            this.lblEventoscadastrados.Name = "lblEventoscadastrados";
            this.lblEventoscadastrados.Location = new System.Drawing.Point(20, 15);
            this.lblEventoscadastrados.Size = new System.Drawing.Size(580, 28);
            this.lblEventoscadastrados.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblEventoscadastrados.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)1);
            this.lblEventoscadastrados.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblEventoscadastrados.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.lblEventoscadastrados.TabIndex = 0;
            this.lblEventoscadastrados.Text = "Eventos cadastrados";
            this.lblEventoscadastrados.TextAlign = (System.Drawing.ContentAlignment)1;
            // btnEditarQuermesse
            this.btnEditarQuermesse.Name = "btnEditarQuermesse";
            this.btnEditarQuermesse.Location = new System.Drawing.Point(750, 12);
            this.btnEditarQuermesse.Size = new System.Drawing.Size(220, 36);
            this.btnEditarQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.btnEditarQuermesse.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)0);
            this.btnEditarQuermesse.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnEditarQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.btnEditarQuermesse.TabIndex = 1;
            this.btnEditarQuermesse.Text = "Ver formulário";
            this.btnEditarQuermesse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarQuermesse.FlatAppearance.BorderSize = 1;
            this.btnEditarQuermesse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 228, 234, 242);
            this.btnEditarQuermesse.UseVisualStyleBackColor = false;
            this.btnEditarQuermesse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditarQuermesse.DialogResult = (System.Windows.Forms.DialogResult)0;
            // dgvQuermesses
            this.dgvQuermesses.Name = "dgvQuermesses";
            this.dgvQuermesses.Location = new System.Drawing.Point(12, 62);
            this.dgvQuermesses.Size = new System.Drawing.Size(966, 335);
            this.dgvQuermesses.Anchor = (System.Windows.Forms.AnchorStyles)15;
            this.dgvQuermesses.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.dgvQuermesses.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.dgvQuermesses.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.dgvQuermesses.TabIndex = 2;
            this.dgvQuermesses.AllowUserToAddRows = false;
            this.dgvQuermesses.AllowUserToDeleteRows = false;
            this.dgvQuermesses.ReadOnly = true;
            this.dgvQuermesses.RowHeadersVisible = false;
            this.dgvQuermesses.MultiSelect = false;
            this.dgvQuermesses.EnableHeadersVisualStyles = false;
            this.dgvQuermesses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQuermesses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvQuermesses.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvQuermesses.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvQuermesses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuermesses.BackgroundColor = System.Drawing.Color.White;
            this.dgvQuermesses.GridColor = System.Drawing.Color.FromArgb(255, 240, 243, 248);
            this.dgvQuermesses.RowTemplate.Height = 50;
            this.dgvQuermesses.ColumnHeadersHeight = 42;
            this.dgvQuermesses.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 248, 250, 253);
            this.dgvQuermesses.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.colQuermessesEvento.Name = "colQuermessesEvento";
            this.colQuermessesEvento.HeaderText = "Evento";
            this.colQuermessesEvento.ReadOnly = true;
            this.colQuermessesLocal.Name = "colQuermessesLocal";
            this.colQuermessesLocal.HeaderText = "Local";
            this.colQuermessesLocal.ReadOnly = true;
            this.colQuermessesInicio.Name = "colQuermessesInicio";
            this.colQuermessesInicio.HeaderText = "Início";
            this.colQuermessesInicio.ReadOnly = true;
            this.colQuermessesFim.Name = "colQuermessesFim";
            this.colQuermessesFim.HeaderText = "Fim";
            this.colQuermessesFim.ReadOnly = true;
            this.colQuermessesResponsavel.Name = "colQuermessesResponsavel";
            this.colQuermessesResponsavel.HeaderText = "Responsável";
            this.colQuermessesResponsavel.ReadOnly = true;
            this.colQuermessesSituacao.Name = "colQuermessesSituacao";
            this.colQuermessesSituacao.HeaderText = "Situação";
            this.colQuermessesSituacao.ReadOnly = true;
            this.dgvQuermesses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colQuermessesEvento, this.colQuermessesLocal, this.colQuermessesInicio, this.colQuermessesFim, this.colQuermessesResponsavel, this.colQuermessesSituacao });
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
            this.panelCorpo.Controls.Add(this.panelEventoResumo);
            this.panelEventoResumo.Controls.Add(this.pictureIconeEvento);
            this.panelEventoResumo.Controls.Add(this.lblQuermessedeSaoJoao2026);
            this.panelEventoResumo.Controls.Add(this.lblPatiodacomunidadeEmandamento);
            this.panelEventoResumo.Controls.Add(this.btnNovaQuermesse);
            this.panelCorpo.Controls.Add(this.panelListaQuermesses);
            this.panelListaQuermesses.Controls.Add(this.lblEventoscadastrados);
            this.panelListaQuermesses.Controls.Add(this.btnEditarQuermesse);
            this.panelListaQuermesses.Controls.Add(this.dgvQuermesses);
            this.Controls.Add(this.labelAviso);
            this.Name = "QuermessesControl";
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
            this.panelEventoResumo.ResumeLayout(false);
            this.panelEventoResumo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeEvento)).EndInit();
            this.panelListaQuermesses.ResumeLayout(false);
            this.panelListaQuermesses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuermesses)).EndInit();
            this.btnNovaQuermesse.Click += new System.EventHandler(this.btnNovaQuermesse_Click);
            this.btnEditarQuermesse.Click += new System.EventHandler(this.btnEditarQuermesse_Click);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.PictureBox pictureIconeTela;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelCorpo;
        private System.Windows.Forms.Panel panelEventoResumo;
        private System.Windows.Forms.PictureBox pictureIconeEvento;
        private System.Windows.Forms.Label lblQuermessedeSaoJoao2026;
        private System.Windows.Forms.Label lblPatiodacomunidadeEmandamento;
        private System.Windows.Forms.Button btnNovaQuermesse;
        private System.Windows.Forms.Panel panelListaQuermesses;
        private System.Windows.Forms.Label lblEventoscadastrados;
        private System.Windows.Forms.Button btnEditarQuermesse;
        private System.Windows.Forms.DataGridView dgvQuermesses;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuermessesEvento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuermessesLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuermessesInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuermessesFim;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuermessesResponsavel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuermessesSituacao;
        private System.Windows.Forms.Label labelAviso;
    }
}
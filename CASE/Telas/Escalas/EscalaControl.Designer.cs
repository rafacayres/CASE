namespace CASE.Quermesse
{
    partial class EscalaControl
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
            this.panelFiltrosEscala = new System.Windows.Forms.Panel();
            this.lblSetorbarraca = new System.Windows.Forms.Label();
            this.cmbFiltroEscala = new System.Windows.Forms.ComboBox();
            this.lblTurno = new System.Windows.Forms.Label();
            this.cmbFiltroTurnoEscala = new System.Windows.Forms.ComboBox();
            this.btnNovaAlocacao = new System.Windows.Forms.Button();
            this.panelListaEscala = new System.Windows.Forms.Panel();
            this.dgvEscala = new System.Windows.Forms.DataGridView();
            this.colEscalaFuncionario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEscalaSetor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEscalaData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEscalaTurno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEscalaEntrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEscalaSaida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEscalaSituacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelAviso = new System.Windows.Forms.Label();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).BeginInit();
            this.panelCorpo.SuspendLayout();
            this.panelFiltrosEscala.SuspendLayout();
            this.panelListaEscala.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEscala)).BeginInit();
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
            this.pictureIconeTela.Image = global::CASE.Properties.Resources.LucideAzulEscala;
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
            this.lblTitulo.Text = "Escala de trabalho";
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
            this.lblSubtitulo.Text = "Distribuição dos funcionários por barraca, data e horário";
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
            // panelFiltrosEscala
            this.panelFiltrosEscala.Name = "panelFiltrosEscala";
            this.panelFiltrosEscala.Location = new System.Drawing.Point(0, 0);
            this.panelFiltrosEscala.Size = new System.Drawing.Size(990, 95);
            this.panelFiltrosEscala.Anchor = (System.Windows.Forms.AnchorStyles)13;
            this.panelFiltrosEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.panelFiltrosEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.panelFiltrosEscala.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.panelFiltrosEscala.TabIndex = 0;
            // lblSetorbarraca
            this.lblSetorbarraca.Name = "lblSetorbarraca";
            this.lblSetorbarraca.Location = new System.Drawing.Point(18, 12);
            this.lblSetorbarraca.Size = new System.Drawing.Size(250, 24);
            this.lblSetorbarraca.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSetorbarraca.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)0);
            this.lblSetorbarraca.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblSetorbarraca.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSetorbarraca.TabIndex = 0;
            this.lblSetorbarraca.Text = "Setor / barraca";
            this.lblSetorbarraca.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbFiltroEscala
            this.cmbFiltroEscala.Name = "cmbFiltroEscala";
            this.cmbFiltroEscala.Location = new System.Drawing.Point(18, 44);
            this.cmbFiltroEscala.Size = new System.Drawing.Size(280, 29);
            this.cmbFiltroEscala.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbFiltroEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbFiltroEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbFiltroEscala.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbFiltroEscala.TabIndex = 1;
            this.cmbFiltroEscala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroEscala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFiltroEscala.Items.AddRange(new object[] { "Todos os setores", "Bebidas", "Alimentação", "Doces", "Brincadeiras" });
            this.cmbFiltroEscala.SelectedIndex = 0;
            // lblTurno
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Location = new System.Drawing.Point(320, 12);
            this.lblTurno.Size = new System.Drawing.Size(200, 24);
            this.lblTurno.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblTurno.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)0);
            this.lblTurno.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblTurno.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblTurno.TabIndex = 2;
            this.lblTurno.Text = "Turno";
            this.lblTurno.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbFiltroTurnoEscala
            this.cmbFiltroTurnoEscala.Name = "cmbFiltroTurnoEscala";
            this.cmbFiltroTurnoEscala.Location = new System.Drawing.Point(320, 44);
            this.cmbFiltroTurnoEscala.Size = new System.Drawing.Size(220, 29);
            this.cmbFiltroTurnoEscala.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbFiltroTurnoEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbFiltroTurnoEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbFiltroTurnoEscala.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbFiltroTurnoEscala.TabIndex = 3;
            this.cmbFiltroTurnoEscala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTurnoEscala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFiltroTurnoEscala.Items.AddRange(new object[] { "Todos os turnos", "Manhã", "Tarde", "Noite" });
            this.cmbFiltroTurnoEscala.SelectedIndex = 0;
            // btnNovaAlocacao
            this.btnNovaAlocacao.Name = "btnNovaAlocacao";
            this.btnNovaAlocacao.Location = new System.Drawing.Point(735, 34);
            this.btnNovaAlocacao.Size = new System.Drawing.Size(235, 40);
            this.btnNovaAlocacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.btnNovaAlocacao.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)1);
            this.btnNovaAlocacao.BackColor = System.Drawing.Color.FromArgb(255, 23, 105, 210);
            this.btnNovaAlocacao.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnNovaAlocacao.TabIndex = 4;
            this.btnNovaAlocacao.Text = "+ Alocar funcionário";
            this.btnNovaAlocacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovaAlocacao.FlatAppearance.BorderSize = 0;
            this.btnNovaAlocacao.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 228, 234, 242);
            this.btnNovaAlocacao.UseVisualStyleBackColor = false;
            this.btnNovaAlocacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovaAlocacao.DialogResult = (System.Windows.Forms.DialogResult)0;
            // panelListaEscala
            this.panelListaEscala.Name = "panelListaEscala";
            this.panelListaEscala.Location = new System.Drawing.Point(0, 115);
            this.panelListaEscala.Size = new System.Drawing.Size(990, 415);
            this.panelListaEscala.Anchor = (System.Windows.Forms.AnchorStyles)13;
            this.panelListaEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.panelListaEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.panelListaEscala.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.panelListaEscala.TabIndex = 1;
            // dgvEscala
            this.dgvEscala.Name = "dgvEscala";
            this.dgvEscala.Location = new System.Drawing.Point(12, 14);
            this.dgvEscala.Size = new System.Drawing.Size(966, 385);
            this.dgvEscala.Anchor = (System.Windows.Forms.AnchorStyles)15;
            this.dgvEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.dgvEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.dgvEscala.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.dgvEscala.TabIndex = 0;
            this.dgvEscala.AllowUserToAddRows = false;
            this.dgvEscala.AllowUserToDeleteRows = false;
            this.dgvEscala.ReadOnly = true;
            this.dgvEscala.RowHeadersVisible = false;
            this.dgvEscala.MultiSelect = false;
            this.dgvEscala.EnableHeadersVisualStyles = false;
            this.dgvEscala.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEscala.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEscala.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvEscala.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvEscala.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEscala.BackgroundColor = System.Drawing.Color.White;
            this.dgvEscala.GridColor = System.Drawing.Color.FromArgb(255, 240, 243, 248);
            this.dgvEscala.RowTemplate.Height = 50;
            this.dgvEscala.ColumnHeadersHeight = 42;
            this.dgvEscala.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 248, 250, 253);
            this.dgvEscala.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.colEscalaFuncionario.Name = "colEscalaFuncionario";
            this.colEscalaFuncionario.HeaderText = "Funcionário";
            this.colEscalaFuncionario.ReadOnly = true;
            this.colEscalaSetor.Name = "colEscalaSetor";
            this.colEscalaSetor.HeaderText = "Setor";
            this.colEscalaSetor.ReadOnly = true;
            this.colEscalaData.Name = "colEscalaData";
            this.colEscalaData.HeaderText = "Data";
            this.colEscalaData.ReadOnly = true;
            this.colEscalaTurno.Name = "colEscalaTurno";
            this.colEscalaTurno.HeaderText = "Turno";
            this.colEscalaTurno.ReadOnly = true;
            this.colEscalaEntrada.Name = "colEscalaEntrada";
            this.colEscalaEntrada.HeaderText = "Entrada";
            this.colEscalaEntrada.ReadOnly = true;
            this.colEscalaSaida.Name = "colEscalaSaida";
            this.colEscalaSaida.HeaderText = "Saída";
            this.colEscalaSaida.ReadOnly = true;
            this.colEscalaSituacao.Name = "colEscalaSituacao";
            this.colEscalaSituacao.HeaderText = "Situação";
            this.colEscalaSituacao.ReadOnly = true;
            this.dgvEscala.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colEscalaFuncionario, this.colEscalaSetor, this.colEscalaData, this.colEscalaTurno, this.colEscalaEntrada, this.colEscalaSaida, this.colEscalaSituacao });
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
            this.panelCorpo.Controls.Add(this.panelFiltrosEscala);
            this.panelFiltrosEscala.Controls.Add(this.lblSetorbarraca);
            this.panelFiltrosEscala.Controls.Add(this.cmbFiltroEscala);
            this.panelFiltrosEscala.Controls.Add(this.lblTurno);
            this.panelFiltrosEscala.Controls.Add(this.cmbFiltroTurnoEscala);
            this.panelFiltrosEscala.Controls.Add(this.btnNovaAlocacao);
            this.panelCorpo.Controls.Add(this.panelListaEscala);
            this.panelListaEscala.Controls.Add(this.dgvEscala);
            this.Controls.Add(this.labelAviso);
            this.Name = "EscalaControl";
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
            this.panelFiltrosEscala.ResumeLayout(false);
            this.panelFiltrosEscala.PerformLayout();
            this.panelListaEscala.ResumeLayout(false);
            this.panelListaEscala.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEscala)).EndInit();
            this.btnNovaAlocacao.Click += new System.EventHandler(this.btnNovaAlocacao_Click);
            this.cmbFiltroEscala.SelectedIndexChanged += new System.EventHandler(this.Filtros_SelectedIndexChanged);
            this.cmbFiltroTurnoEscala.SelectedIndexChanged += new System.EventHandler(this.Filtros_SelectedIndexChanged);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.PictureBox pictureIconeTela;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelCorpo;
        private System.Windows.Forms.Panel panelFiltrosEscala;
        private System.Windows.Forms.Label lblSetorbarraca;
        private System.Windows.Forms.ComboBox cmbFiltroEscala;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.ComboBox cmbFiltroTurnoEscala;
        private System.Windows.Forms.Button btnNovaAlocacao;
        private System.Windows.Forms.Panel panelListaEscala;
        private System.Windows.Forms.DataGridView dgvEscala;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEscalaFuncionario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEscalaSetor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEscalaData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEscalaTurno;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEscalaEntrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEscalaSaida;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEscalaSituacao;
        private System.Windows.Forms.Label labelAviso;
    }
}
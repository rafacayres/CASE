namespace CASE
{
    partial class ConfiguracoesControl
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
            this.lblTituloConfiguracoes = new System.Windows.Forms.Label();
            this.lblSubtituloConfiguracoes = new System.Windows.Forms.Label();
            this.panelPerfilUsuario = new System.Windows.Forms.Panel();
            this.lblTituloPerfilUsuario = new System.Windows.Forms.Label();
            this.lblPerfilNome = new System.Windows.Forms.Label();
            this.txtPerfilNome = new System.Windows.Forms.TextBox();
            this.lblPerfilUsuario = new System.Windows.Forms.Label();
            this.txtPerfilUsuario = new System.Windows.Forms.TextBox();
            this.lblPerfilEmail = new System.Windows.Forms.Label();
            this.txtPerfilEmail = new System.Windows.Forms.TextBox();
            this.btnSalvarPerfil = new System.Windows.Forms.Button();
            this.panelPreferencias = new System.Windows.Forms.Panel();
            this.lblPreferencias = new System.Windows.Forms.Label();
            this.chkAlertasEmail = new System.Windows.Forms.CheckBox();
            this.chkAvisosSistema = new System.Windows.Forms.CheckBox();
            this.btnSalvarPreferencias = new System.Windows.Forms.Button();
            this.panelSolicitacoesPendentes = new System.Windows.Forms.Panel();
            this.lblSolicitacoesPendentes = new System.Windows.Forms.Label();
            this.dgvSolicitacoes = new System.Windows.Forms.DataGridView();
            this.dgvSolicitacoesColNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSolicitacoesColUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSolicitacoesColEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSolicitacoesColData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSolicitacoesColStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAprovarSolicitacao = new System.Windows.Forms.Button();
            this.btnRejeitarSolicitacao = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitacoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).BeginInit();
            this.SuspendLayout();
            this.panelPerfilUsuario.SuspendLayout();
            this.panelPreferencias.SuspendLayout();
            this.panelSolicitacoesPendentes.SuspendLayout();
            // lblTituloConfiguracoes
            this.lblTituloConfiguracoes.Name = "lblTituloConfiguracoes";
            this.lblTituloConfiguracoes.Location = new System.Drawing.Point(80, 25);
            this.lblTituloConfiguracoes.Size = new System.Drawing.Size(350, 40);
            this.lblTituloConfiguracoes.TabIndex = 0;
            this.lblTituloConfiguracoes.Text = "Configurações";
            this.lblTituloConfiguracoes.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTituloConfiguracoes.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // lblSubtituloConfiguracoes
            this.lblSubtituloConfiguracoes.Name = "lblSubtituloConfiguracoes";
            this.lblSubtituloConfiguracoes.Location = new System.Drawing.Point(82, 68);
            this.lblSubtituloConfiguracoes.Size = new System.Drawing.Size(650, 25);
            this.lblSubtituloConfiguracoes.TabIndex = 1;
            this.lblSubtituloConfiguracoes.Text = "Gerencie perfil, preferências e solicitações";
            this.lblSubtituloConfiguracoes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblSubtituloConfiguracoes.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // panelPerfilUsuario
            this.panelPerfilUsuario.Name = "panelPerfilUsuario";
            this.panelPerfilUsuario.Location = new System.Drawing.Point(30, 115);
            this.panelPerfilUsuario.Size = new System.Drawing.Size(445, 180);
            this.panelPerfilUsuario.TabIndex = 2;
            this.panelPerfilUsuario.BackColor = System.Drawing.Color.White;
            // lblTituloPerfilUsuario
            this.lblTituloPerfilUsuario.Name = "lblTituloPerfilUsuario";
            this.lblTituloPerfilUsuario.Location = new System.Drawing.Point(20, 10);
            this.lblTituloPerfilUsuario.Size = new System.Drawing.Size(390, 28);
            this.lblTituloPerfilUsuario.TabIndex = 3;
            this.lblTituloPerfilUsuario.Text = "Perfil do usuário";
            this.lblTituloPerfilUsuario.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTituloPerfilUsuario.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // lblPerfilNome
            this.lblPerfilNome.Name = "lblPerfilNome";
            this.lblPerfilNome.Location = new System.Drawing.Point(20, 46);
            this.lblPerfilNome.Size = new System.Drawing.Size(75, 25);
            this.lblPerfilNome.TabIndex = 4;
            this.lblPerfilNome.Text = "Nome";
            this.lblPerfilNome.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblPerfilNome.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // txtPerfilNome
            this.txtPerfilNome.Name = "txtPerfilNome";
            this.txtPerfilNome.Location = new System.Drawing.Point(100, 46);
            this.txtPerfilNome.Size = new System.Drawing.Size(325, 25);
            this.txtPerfilNome.TabIndex = 5;
            this.txtPerfilNome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPerfilNome.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            this.txtPerfilNome.Text = "Ana Silva";
            // lblPerfilUsuario
            this.lblPerfilUsuario.Name = "lblPerfilUsuario";
            this.lblPerfilUsuario.Location = new System.Drawing.Point(20, 78);
            this.lblPerfilUsuario.Size = new System.Drawing.Size(75, 25);
            this.lblPerfilUsuario.TabIndex = 6;
            this.lblPerfilUsuario.Text = "Usuário";
            this.lblPerfilUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblPerfilUsuario.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // txtPerfilUsuario
            this.txtPerfilUsuario.Name = "txtPerfilUsuario";
            this.txtPerfilUsuario.Location = new System.Drawing.Point(100, 78);
            this.txtPerfilUsuario.Size = new System.Drawing.Size(325, 25);
            this.txtPerfilUsuario.TabIndex = 7;
            this.txtPerfilUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPerfilUsuario.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            this.txtPerfilUsuario.Text = "ana.silva";
            // lblPerfilEmail
            this.lblPerfilEmail.Name = "lblPerfilEmail";
            this.lblPerfilEmail.Location = new System.Drawing.Point(20, 110);
            this.lblPerfilEmail.Size = new System.Drawing.Size(75, 25);
            this.lblPerfilEmail.TabIndex = 8;
            this.lblPerfilEmail.Text = "E-mail";
            this.lblPerfilEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblPerfilEmail.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            // txtPerfilEmail
            this.txtPerfilEmail.Name = "txtPerfilEmail";
            this.txtPerfilEmail.Location = new System.Drawing.Point(100, 110);
            this.txtPerfilEmail.Size = new System.Drawing.Size(325, 25);
            this.txtPerfilEmail.TabIndex = 9;
            this.txtPerfilEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPerfilEmail.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            this.txtPerfilEmail.Text = "ana@example.com";
            // btnSalvarPerfil
            this.btnSalvarPerfil.Name = "btnSalvarPerfil";
            this.btnSalvarPerfil.Location = new System.Drawing.Point(255, 143);
            this.btnSalvarPerfil.Size = new System.Drawing.Size(170, 30);
            this.btnSalvarPerfil.TabIndex = 10;
            this.btnSalvarPerfil.Text = "Salvar alterações";
            this.btnSalvarPerfil.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnSalvarPerfil.ForeColor = System.Drawing.Color.White;
            this.btnSalvarPerfil.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalvarPerfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarPerfil.FlatAppearance.BorderSize = 0;
            this.btnSalvarPerfil.UseVisualStyleBackColor = false;
            this.btnSalvarPerfil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarPerfil.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            // panelPreferencias
            this.panelPreferencias.Name = "panelPreferencias";
            this.panelPreferencias.Location = new System.Drawing.Point(495, 115);
            this.panelPreferencias.Size = new System.Drawing.Size(445, 180);
            this.panelPreferencias.TabIndex = 11;
            this.panelPreferencias.BackColor = System.Drawing.Color.White;
            // lblPreferencias
            this.lblPreferencias.Name = "lblPreferencias";
            this.lblPreferencias.Location = new System.Drawing.Point(20, 10);
            this.lblPreferencias.Size = new System.Drawing.Size(390, 28);
            this.lblPreferencias.TabIndex = 12;
            this.lblPreferencias.Text = "Preferências";
            this.lblPreferencias.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPreferencias.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // chkAlertasEmail
            this.chkAlertasEmail.Name = "chkAlertasEmail";
            this.chkAlertasEmail.Location = new System.Drawing.Point(20, 53);
            this.chkAlertasEmail.Size = new System.Drawing.Size(400, 25);
            this.chkAlertasEmail.TabIndex = 13;
            this.chkAlertasEmail.Text = "Receber alertas por e-mail";
            // chkAvisosSistema
            this.chkAvisosSistema.Name = "chkAvisosSistema";
            this.chkAvisosSistema.Location = new System.Drawing.Point(20, 88);
            this.chkAvisosSistema.Size = new System.Drawing.Size(400, 25);
            this.chkAvisosSistema.TabIndex = 14;
            this.chkAvisosSistema.Text = "Receber avisos do sistema";
            this.chkAvisosSistema.Checked = true;
            // btnSalvarPreferencias
            this.btnSalvarPreferencias.Name = "btnSalvarPreferencias";
            this.btnSalvarPreferencias.Location = new System.Drawing.Point(245, 135);
            this.btnSalvarPreferencias.Size = new System.Drawing.Size(180, 38);
            this.btnSalvarPreferencias.TabIndex = 15;
            this.btnSalvarPreferencias.Text = "Salvar preferências";
            this.btnSalvarPreferencias.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnSalvarPreferencias.ForeColor = System.Drawing.Color.White;
            this.btnSalvarPreferencias.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalvarPreferencias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarPreferencias.FlatAppearance.BorderSize = 0;
            this.btnSalvarPreferencias.UseVisualStyleBackColor = false;
            this.btnSalvarPreferencias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarPreferencias.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            // panelSolicitacoesPendentes
            this.panelSolicitacoesPendentes.Name = "panelSolicitacoesPendentes";
            this.panelSolicitacoesPendentes.Location = new System.Drawing.Point(30, 315);
            this.panelSolicitacoesPendentes.Size = new System.Drawing.Size(910, 280);
            this.panelSolicitacoesPendentes.TabIndex = 16;
            this.panelSolicitacoesPendentes.BackColor = System.Drawing.Color.White;
            this.panelSolicitacoesPendentes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelSolicitacoesPendentes.MinimumSize = new System.Drawing.Size(910, 280);
            // lblSolicitacoesPendentes
            this.lblSolicitacoesPendentes.Name = "lblSolicitacoesPendentes";
            this.lblSolicitacoesPendentes.Location = new System.Drawing.Point(20, 15);
            this.lblSolicitacoesPendentes.Size = new System.Drawing.Size(500, 30);
            this.lblSolicitacoesPendentes.TabIndex = 17;
            this.lblSolicitacoesPendentes.Text = "Solicitações pendentes";
            this.lblSolicitacoesPendentes.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSolicitacoesPendentes.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            // dgvSolicitacoes
            this.dgvSolicitacoes.Name = "dgvSolicitacoes";
            this.dgvSolicitacoes.Location = new System.Drawing.Point(20, 55);
            this.dgvSolicitacoes.Size = new System.Drawing.Size(870, 165);
            this.dgvSolicitacoes.TabIndex = 18;
            this.dgvSolicitacoes.AllowUserToAddRows = false;
            this.dgvSolicitacoes.AllowUserToDeleteRows = false;
            this.dgvSolicitacoes.AllowUserToResizeRows = false;
            this.dgvSolicitacoes.ReadOnly = true;
            this.dgvSolicitacoes.RowHeadersVisible = false;
            this.dgvSolicitacoes.MultiSelect = false;
            this.dgvSolicitacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSolicitacoes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSolicitacoes.BackgroundColor = System.Drawing.Color.White;
            this.dgvSolicitacoes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSolicitacoes.EnableHeadersVisualStyles = false;
            this.dgvSolicitacoes.ColumnHeadersHeight = 42;
            this.dgvSolicitacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSolicitacoes.RowTemplate.Height = 48;
            this.dgvSolicitacoes.GridColor = System.Drawing.Color.FromArgb(230, 235, 242);
            this.dgvSolicitacoes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            estiloCabecalho.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            estiloCabecalho.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            estiloCabecalho.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            estiloCabecalho.SelectionBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            estiloCabecalho.SelectionForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            this.dgvSolicitacoes.ColumnHeadersDefaultCellStyle = estiloCabecalho;
            estiloCelulas.BackColor = System.Drawing.Color.White;
            estiloCelulas.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            estiloCelulas.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            estiloCelulas.SelectionForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            this.dgvSolicitacoes.DefaultCellStyle = estiloCelulas;
            // dgvSolicitacoesColNome
            this.dgvSolicitacoesColNome.Name = "dgvSolicitacoesColNome";
            this.dgvSolicitacoesColNome.HeaderText = "Nome";
            this.dgvSolicitacoesColNome.ReadOnly = true;
            this.dgvSolicitacoesColNome.FillWeight = 100F;
            // dgvSolicitacoesColUsuario
            this.dgvSolicitacoesColUsuario.Name = "dgvSolicitacoesColUsuario";
            this.dgvSolicitacoesColUsuario.HeaderText = "Usuário";
            this.dgvSolicitacoesColUsuario.ReadOnly = true;
            this.dgvSolicitacoesColUsuario.FillWeight = 100F;
            // dgvSolicitacoesColEmail
            this.dgvSolicitacoesColEmail.Name = "dgvSolicitacoesColEmail";
            this.dgvSolicitacoesColEmail.HeaderText = "E-mail";
            this.dgvSolicitacoesColEmail.ReadOnly = true;
            this.dgvSolicitacoesColEmail.FillWeight = 180F;
            // dgvSolicitacoesColData
            this.dgvSolicitacoesColData.Name = "dgvSolicitacoesColData";
            this.dgvSolicitacoesColData.HeaderText = "Data da solicitação";
            this.dgvSolicitacoesColData.ReadOnly = true;
            this.dgvSolicitacoesColData.FillWeight = 125F;
            // dgvSolicitacoesColStatus
            this.dgvSolicitacoesColStatus.Name = "dgvSolicitacoesColStatus";
            this.dgvSolicitacoesColStatus.HeaderText = "Status";
            this.dgvSolicitacoesColStatus.ReadOnly = true;
            this.dgvSolicitacoesColStatus.FillWeight = 100F;
            // btnAprovarSolicitacao
            this.btnAprovarSolicitacao.Name = "btnAprovarSolicitacao";
            this.btnAprovarSolicitacao.Location = new System.Drawing.Point(660, 230);
            this.btnAprovarSolicitacao.Size = new System.Drawing.Size(100, 30);
            this.btnAprovarSolicitacao.TabIndex = 24;
            this.btnAprovarSolicitacao.Text = "Aprovar";
            this.btnAprovarSolicitacao.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAprovarSolicitacao.ForeColor = System.Drawing.Color.White;
            this.btnAprovarSolicitacao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAprovarSolicitacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAprovarSolicitacao.FlatAppearance.BorderSize = 0;
            this.btnAprovarSolicitacao.UseVisualStyleBackColor = false;
            this.btnAprovarSolicitacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAprovarSolicitacao.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            // btnRejeitarSolicitacao
            this.btnRejeitarSolicitacao.Name = "btnRejeitarSolicitacao";
            this.btnRejeitarSolicitacao.Location = new System.Drawing.Point(775, 230);
            this.btnRejeitarSolicitacao.Size = new System.Drawing.Size(100, 30);
            this.btnRejeitarSolicitacao.TabIndex = 25;
            this.btnRejeitarSolicitacao.Text = "Rejeitar";
            this.btnRejeitarSolicitacao.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnRejeitarSolicitacao.ForeColor = System.Drawing.Color.FromArgb(18, 47, 80);
            this.btnRejeitarSolicitacao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRejeitarSolicitacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRejeitarSolicitacao.FlatAppearance.BorderSize = 0;
            this.btnRejeitarSolicitacao.UseVisualStyleBackColor = false;
            this.btnRejeitarSolicitacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRejeitarSolicitacao.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(this.lblTituloConfiguracoes);
            this.Controls.Add(this.lblSubtituloConfiguracoes);
            this.Controls.Add(this.panelPerfilUsuario);
            this.panelPerfilUsuario.Controls.Add(this.lblTituloPerfilUsuario);
            this.panelPerfilUsuario.Controls.Add(this.lblPerfilNome);
            this.panelPerfilUsuario.Controls.Add(this.txtPerfilNome);
            this.panelPerfilUsuario.Controls.Add(this.lblPerfilUsuario);
            this.panelPerfilUsuario.Controls.Add(this.txtPerfilUsuario);
            this.panelPerfilUsuario.Controls.Add(this.lblPerfilEmail);
            this.panelPerfilUsuario.Controls.Add(this.txtPerfilEmail);
            this.panelPerfilUsuario.Controls.Add(this.btnSalvarPerfil);
            this.Controls.Add(this.panelPreferencias);
            this.panelPreferencias.Controls.Add(this.lblPreferencias);
            this.panelPreferencias.Controls.Add(this.chkAlertasEmail);
            this.panelPreferencias.Controls.Add(this.chkAvisosSistema);
            this.panelPreferencias.Controls.Add(this.btnSalvarPreferencias);
            this.Controls.Add(this.panelSolicitacoesPendentes);
            this.panelSolicitacoesPendentes.Controls.Add(this.lblSolicitacoesPendentes);
            this.panelSolicitacoesPendentes.Controls.Add(this.dgvSolicitacoes);
            this.dgvSolicitacoes.Columns.Add(this.dgvSolicitacoesColNome);
            this.dgvSolicitacoes.Columns.Add(this.dgvSolicitacoesColUsuario);
            this.dgvSolicitacoes.Columns.Add(this.dgvSolicitacoesColEmail);
            this.dgvSolicitacoes.Columns.Add(this.dgvSolicitacoesColData);
            this.dgvSolicitacoes.Columns.Add(this.dgvSolicitacoesColStatus);
            this.panelSolicitacoesPendentes.Controls.Add(this.btnAprovarSolicitacao);
            this.panelSolicitacoesPendentes.Controls.Add(this.btnRejeitarSolicitacao);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor = System.Drawing.Color.FromArgb(243, 247, 252);
            this.Name = "ConfiguracoesControl";
            this.Size = new System.Drawing.Size(970, 680);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(970, 660);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitacoes)).EndInit();
            this.panelPerfilUsuario.ResumeLayout(false);
            this.panelPerfilUsuario.PerformLayout();
            this.panelPreferencias.ResumeLayout(false);
            this.panelPreferencias.PerformLayout();
            this.panelSolicitacoesPendentes.ResumeLayout(false);
            this.panelSolicitacoesPendentes.PerformLayout();
            // pictureIconeTela — Lucide
            this.pictureIconeTela.Name = "pictureIconeTela";
            this.pictureIconeTela.Image = global::CASE.Properties.Resources.LucideAzulConfiguracoes;
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

        private System.Windows.Forms.Label lblTituloConfiguracoes;
        private System.Windows.Forms.Label lblSubtituloConfiguracoes;
        private System.Windows.Forms.Panel panelPerfilUsuario;
        private System.Windows.Forms.Label lblTituloPerfilUsuario;
        private System.Windows.Forms.Label lblPerfilNome;
        private System.Windows.Forms.TextBox txtPerfilNome;
        private System.Windows.Forms.Label lblPerfilUsuario;
        private System.Windows.Forms.TextBox txtPerfilUsuario;
        private System.Windows.Forms.Label lblPerfilEmail;
        private System.Windows.Forms.TextBox txtPerfilEmail;
        private System.Windows.Forms.Button btnSalvarPerfil;
        private System.Windows.Forms.Panel panelPreferencias;
        private System.Windows.Forms.Label lblPreferencias;
        private System.Windows.Forms.CheckBox chkAlertasEmail;
        private System.Windows.Forms.CheckBox chkAvisosSistema;
        private System.Windows.Forms.Button btnSalvarPreferencias;
        private System.Windows.Forms.Panel panelSolicitacoesPendentes;
        private System.Windows.Forms.Label lblSolicitacoesPendentes;
        private System.Windows.Forms.DataGridView dgvSolicitacoes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSolicitacoesColNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSolicitacoesColUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSolicitacoesColEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSolicitacoesColData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSolicitacoesColStatus;
        private System.Windows.Forms.Button btnAprovarSolicitacao;
        private System.Windows.Forms.Button btnRejeitarSolicitacao;
        private System.Windows.Forms.PictureBox pictureIconeTela;
    }
}


namespace CASE.Quermesse
{
    partial class QuermesseForm
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
            this.panelFormulario = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureIconeFormulario = new System.Windows.Forms.PictureBox();
            this.labelAvisoFormulario = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalvarVisual = new System.Windows.Forms.Button();
            this.lblNomedoevento = new System.Windows.Forms.Label();
            this.txtNomeQuermesse = new System.Windows.Forms.TextBox();
            this.lblTipoEvento = new System.Windows.Forms.Label();
            this.cmbTipoEvento = new System.Windows.Forms.ComboBox();
            this.lblLocal = new System.Windows.Forms.Label();
            this.txtLocalQuermesse = new System.Windows.Forms.TextBox();
            this.lblDatainicial = new System.Windows.Forms.Label();
            this.dtpInicioQuermesse = new System.Windows.Forms.DateTimePicker();
            this.lblDatafinal = new System.Windows.Forms.Label();
            this.dtpFimQuermesse = new System.Windows.Forms.DateTimePicker();
            this.lblResponsavel = new System.Windows.Forms.Label();
            this.txtResponsavelQuermesse = new System.Windows.Forms.TextBox();
            this.lblSituacao = new System.Windows.Forms.Label();
            this.cmbSituacaoQuermesse = new System.Windows.Forms.ComboBox();
            this.lblObservacao = new System.Windows.Forms.Label();
            this.txtObservacaoQuermesse = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeFormulario)).BeginInit();
            // panelFormulario
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Location = new System.Drawing.Point(16, 16);
            this.panelFormulario.Size = new System.Drawing.Size(608, 593);
            this.panelFormulario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.panelFormulario.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.panelFormulario.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.panelFormulario.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.panelFormulario.TabIndex = 0;
            // lblTitulo
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Location = new System.Drawing.Point(28, 22);
            this.lblTitulo.Size = new System.Drawing.Size(485, 40);
            this.lblTitulo.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, (System.Drawing.FontStyle)1);
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(255, 16, 43, 73);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Novo evento";
            this.lblTitulo.TextAlign = (System.Drawing.ContentAlignment)1;
            // pictureIconeFormulario
            this.pictureIconeFormulario.Name = "pictureIconeFormulario";
            this.pictureIconeFormulario.Location = new System.Drawing.Point(538, 24);
            this.pictureIconeFormulario.Size = new System.Drawing.Size(36, 36);
            this.pictureIconeFormulario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.pictureIconeFormulario.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.pictureIconeFormulario.BackColor = System.Drawing.Color.FromArgb(255, 234, 242, 254);
            this.pictureIconeFormulario.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.pictureIconeFormulario.TabIndex = 1;
            this.pictureIconeFormulario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureIconeFormulario.Image = global::CASE.Properties.Resources.LucideAzulQuermesses;
            this.pictureIconeFormulario.TabStop = false;
            // labelAvisoFormulario
            this.labelAvisoFormulario.Name = "labelAvisoFormulario";
            this.labelAvisoFormulario.Location = new System.Drawing.Point(28, 499);
            this.labelAvisoFormulario.Size = new System.Drawing.Size(550, 36);
            this.labelAvisoFormulario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.labelAvisoFormulario.Font = new System.Drawing.Font("Segoe UI", 8.5F, (System.Drawing.FontStyle)0);
            this.labelAvisoFormulario.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.labelAvisoFormulario.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.labelAvisoFormulario.TabIndex = 2;
            this.labelAvisoFormulario.Text = "Prévia visual · alterações não são salvas";
            this.labelAvisoFormulario.TextAlign = (System.Drawing.ContentAlignment)1;
            // btnCancelar
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Location = new System.Drawing.Point(320, 545);
            this.btnCancelar.Size = new System.Drawing.Size(115, 35);
            this.btnCancelar.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)0);
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 1;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 228, 234, 242);
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = (System.Windows.Forms.DialogResult)2;
            // btnSalvarVisual
            this.btnSalvarVisual.Name = "btnSalvarVisual";
            this.btnSalvarVisual.Location = new System.Drawing.Point(450, 545);
            this.btnSalvarVisual.Size = new System.Drawing.Size(134, 35);
            this.btnSalvarVisual.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.btnSalvarVisual.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)1);
            this.btnSalvarVisual.BackColor = System.Drawing.Color.FromArgb(255, 23, 105, 210);
            this.btnSalvarVisual.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnSalvarVisual.TabIndex = 4;
            this.btnSalvarVisual.Text = "Salvar";
            this.btnSalvarVisual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarVisual.FlatAppearance.BorderSize = 0;
            this.btnSalvarVisual.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 228, 234, 242);
            this.btnSalvarVisual.UseVisualStyleBackColor = false;
            this.btnSalvarVisual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarVisual.DialogResult = (System.Windows.Forms.DialogResult)0;
            // lblNomedoevento
            this.lblNomedoevento.Name = "lblNomedoevento";
            this.lblNomedoevento.Location = new System.Drawing.Point(28, 72);
            this.lblNomedoevento.Size = new System.Drawing.Size(550, 22);
            this.lblNomedoevento.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblNomedoevento.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblNomedoevento.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblNomedoevento.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblNomedoevento.TabIndex = 5;
            this.lblNomedoevento.Text = "Nome do evento";
            this.lblNomedoevento.TextAlign = (System.Drawing.ContentAlignment)1;
            // txtNomeQuermesse
            this.txtNomeQuermesse.Name = "txtNomeQuermesse";
            this.txtNomeQuermesse.Location = new System.Drawing.Point(36, 98);
            this.txtNomeQuermesse.Size = new System.Drawing.Size(530, 25);
            this.txtNomeQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.txtNomeQuermesse.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.txtNomeQuermesse.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.txtNomeQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.txtNomeQuermesse.TabIndex = 6;
            this.txtNomeQuermesse.Text = "";
            this.txtNomeQuermesse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNomeQuermesse.Multiline = false;
            this.txtNomeQuermesse.MaxLength = 32767;
            // Tipo do evento
            this.lblTipoEvento.Name = "lblTipoEvento";
            this.lblTipoEvento.Location = new System.Drawing.Point(28, 123);
            this.lblTipoEvento.Size = new System.Drawing.Size(550, 22);
            this.lblTipoEvento.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoEvento.ForeColor = System.Drawing.Color.FromArgb(115, 132, 151);
            this.lblTipoEvento.Text = "Tipo de evento";
            this.lblTipoEvento.TabIndex = 7;
            this.cmbTipoEvento.Name = "cmbTipoEvento";
            this.cmbTipoEvento.Location = new System.Drawing.Point(28, 145);
            this.cmbTipoEvento.Size = new System.Drawing.Size(550, 29);
            this.cmbTipoEvento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoEvento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoEvento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTipoEvento.Items.AddRange(new object[] { "Quermesse", "Bazar", "Festa", "Outro" });
            this.cmbTipoEvento.SelectedIndex = 0;
            this.cmbTipoEvento.TabIndex = 8;
            // lblLocal
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Location = new System.Drawing.Point(28, 178);
            this.lblLocal.Size = new System.Drawing.Size(550, 22);
            this.lblLocal.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblLocal.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblLocal.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblLocal.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblLocal.TabIndex = 7;
            this.lblLocal.Text = "Local";
            this.lblLocal.TextAlign = (System.Drawing.ContentAlignment)1;
            // txtLocalQuermesse
            this.txtLocalQuermesse.Name = "txtLocalQuermesse";
            this.txtLocalQuermesse.Location = new System.Drawing.Point(36, 204);
            this.txtLocalQuermesse.Size = new System.Drawing.Size(530, 25);
            this.txtLocalQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.txtLocalQuermesse.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.txtLocalQuermesse.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.txtLocalQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.txtLocalQuermesse.TabIndex = 8;
            this.txtLocalQuermesse.Text = "";
            this.txtLocalQuermesse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocalQuermesse.Multiline = false;
            this.txtLocalQuermesse.MaxLength = 32767;
            // lblDatainicial
            this.lblDatainicial.Name = "lblDatainicial";
            this.lblDatainicial.Location = new System.Drawing.Point(28, 229);
            this.lblDatainicial.Size = new System.Drawing.Size(550, 22);
            this.lblDatainicial.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblDatainicial.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblDatainicial.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblDatainicial.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblDatainicial.TabIndex = 9;
            this.lblDatainicial.Text = "Data inicial";
            this.lblDatainicial.TextAlign = (System.Drawing.ContentAlignment)1;
            // dtpInicioQuermesse
            this.dtpInicioQuermesse.Name = "dtpInicioQuermesse";
            this.dtpInicioQuermesse.Location = new System.Drawing.Point(28, 251);
            this.dtpInicioQuermesse.Size = new System.Drawing.Size(550, 25);
            this.dtpInicioQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.dtpInicioQuermesse.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.dtpInicioQuermesse.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.dtpInicioQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0, 0);
            this.dtpInicioQuermesse.TabIndex = 10;
            this.dtpInicioQuermesse.Format = (System.Windows.Forms.DateTimePickerFormat)8;
            this.dtpInicioQuermesse.CustomFormat = "dd/MM/yyyy";
            this.dtpInicioQuermesse.ShowUpDown = false;
            this.dtpInicioQuermesse.Value = new System.DateTime(2026, 10, 22, 15, 49, 0);
            // lblDatafinal
            this.lblDatafinal.Name = "lblDatafinal";
            this.lblDatafinal.Location = new System.Drawing.Point(28, 280);
            this.lblDatafinal.Size = new System.Drawing.Size(550, 22);
            this.lblDatafinal.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblDatafinal.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblDatafinal.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblDatafinal.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblDatafinal.TabIndex = 11;
            this.lblDatafinal.Text = "Data final";
            this.lblDatafinal.TextAlign = (System.Drawing.ContentAlignment)1;
            // dtpFimQuermesse
            this.dtpFimQuermesse.Name = "dtpFimQuermesse";
            this.dtpFimQuermesse.Location = new System.Drawing.Point(28, 302);
            this.dtpFimQuermesse.Size = new System.Drawing.Size(550, 25);
            this.dtpFimQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.dtpFimQuermesse.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.dtpFimQuermesse.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.dtpFimQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0, 0);
            this.dtpFimQuermesse.TabIndex = 12;
            this.dtpFimQuermesse.Format = (System.Windows.Forms.DateTimePickerFormat)8;
            this.dtpFimQuermesse.CustomFormat = "dd/MM/yyyy";
            this.dtpFimQuermesse.ShowUpDown = false;
            this.dtpFimQuermesse.Value = new System.DateTime(2026, 10, 22, 15, 49, 0);
            // lblResponsavel
            this.lblResponsavel.Name = "lblResponsavel";
            this.lblResponsavel.Location = new System.Drawing.Point(28, 331);
            this.lblResponsavel.Size = new System.Drawing.Size(550, 22);
            this.lblResponsavel.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblResponsavel.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblResponsavel.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblResponsavel.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblResponsavel.TabIndex = 13;
            this.lblResponsavel.Text = "Responsável";
            this.lblResponsavel.TextAlign = (System.Drawing.ContentAlignment)1;
            // txtResponsavelQuermesse
            this.txtResponsavelQuermesse.Name = "txtResponsavelQuermesse";
            this.txtResponsavelQuermesse.Location = new System.Drawing.Point(36, 357);
            this.txtResponsavelQuermesse.Size = new System.Drawing.Size(530, 25);
            this.txtResponsavelQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.txtResponsavelQuermesse.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.txtResponsavelQuermesse.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.txtResponsavelQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.txtResponsavelQuermesse.TabIndex = 14;
            this.txtResponsavelQuermesse.Text = "";
            this.txtResponsavelQuermesse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResponsavelQuermesse.Multiline = false;
            this.txtResponsavelQuermesse.MaxLength = 32767;
            // lblSituacao
            this.lblSituacao.Name = "lblSituacao";
            this.lblSituacao.Location = new System.Drawing.Point(28, 382);
            this.lblSituacao.Size = new System.Drawing.Size(550, 22);
            this.lblSituacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSituacao.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblSituacao.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblSituacao.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSituacao.TabIndex = 15;
            this.lblSituacao.Text = "Situação";
            this.lblSituacao.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbSituacaoQuermesse
            this.cmbSituacaoQuermesse.Name = "cmbSituacaoQuermesse";
            this.cmbSituacaoQuermesse.Location = new System.Drawing.Point(28, 404);
            this.cmbSituacaoQuermesse.Size = new System.Drawing.Size(550, 29);
            this.cmbSituacaoQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbSituacaoQuermesse.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbSituacaoQuermesse.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbSituacaoQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbSituacaoQuermesse.TabIndex = 16;
            this.cmbSituacaoQuermesse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSituacaoQuermesse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSituacaoQuermesse.Items.AddRange(new object[] { "Planejado", "Em andamento", "Encerrado" });
            this.cmbSituacaoQuermesse.SelectedIndex = 0;
            // lblObservacao
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.Location = new System.Drawing.Point(28, 433);
            this.lblObservacao.Size = new System.Drawing.Size(550, 22);
            this.lblObservacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblObservacao.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblObservacao.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblObservacao.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblObservacao.TabIndex = 17;
            this.lblObservacao.Text = "Observação";
            this.lblObservacao.TextAlign = (System.Drawing.ContentAlignment)1;
            // txtObservacaoQuermesse
            this.txtObservacaoQuermesse.Name = "txtObservacaoQuermesse";
            this.txtObservacaoQuermesse.Location = new System.Drawing.Point(36, 459);
            this.txtObservacaoQuermesse.Size = new System.Drawing.Size(530, 25);
            this.txtObservacaoQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.txtObservacaoQuermesse.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.txtObservacaoQuermesse.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.txtObservacaoQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.txtObservacaoQuermesse.TabIndex = 18;
            this.txtObservacaoQuermesse.Text = "";
            this.txtObservacaoQuermesse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtObservacaoQuermesse.Multiline = false;
            this.txtObservacaoQuermesse.MaxLength = 32767;
            this.Controls.Add(this.panelFormulario);
            this.panelFormulario.Controls.Add(this.lblTitulo);
            this.panelFormulario.Controls.Add(this.pictureIconeFormulario);
            this.panelFormulario.Controls.Add(this.labelAvisoFormulario);
            this.panelFormulario.Controls.Add(this.btnCancelar);
            this.panelFormulario.Controls.Add(this.btnSalvarVisual);
            this.panelFormulario.Controls.Add(this.lblNomedoevento);
            this.panelFormulario.Controls.Add(this.txtNomeQuermesse);
            this.panelFormulario.Controls.Add(this.lblTipoEvento);
            this.panelFormulario.Controls.Add(this.cmbTipoEvento);
            this.panelFormulario.Controls.Add(this.lblLocal);
            this.panelFormulario.Controls.Add(this.txtLocalQuermesse);
            this.panelFormulario.Controls.Add(this.lblDatainicial);
            this.panelFormulario.Controls.Add(this.dtpInicioQuermesse);
            this.panelFormulario.Controls.Add(this.lblDatafinal);
            this.panelFormulario.Controls.Add(this.dtpFimQuermesse);
            this.panelFormulario.Controls.Add(this.lblResponsavel);
            this.panelFormulario.Controls.Add(this.txtResponsavelQuermesse);
            this.panelFormulario.Controls.Add(this.lblSituacao);
            this.panelFormulario.Controls.Add(this.cmbSituacaoQuermesse);
            this.panelFormulario.Controls.Add(this.lblObservacao);
            this.panelFormulario.Controls.Add(this.txtObservacaoQuermesse);
            this.Name = "QuermesseForm";
            this.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.BackColor = System.Drawing.Color.FromArgb(255, 246, 248, 252);
            this.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Text = "Novo evento";
            this.ClientSize = new System.Drawing.Size(640, 625);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AcceptButton = this.btnSalvarVisual;
            this.CancelButton = this.btnCancelar;
            this.panelFormulario.ResumeLayout(false);
            this.panelFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeFormulario)).EndInit();
            this.btnSalvarVisual.Click += new System.EventHandler(this.btnSalvarVisual_Click);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel panelFormulario;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureIconeFormulario;
        private System.Windows.Forms.Label labelAvisoFormulario;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvarVisual;
        private System.Windows.Forms.Label lblNomedoevento;
        private System.Windows.Forms.TextBox txtNomeQuermesse;
        private System.Windows.Forms.Label lblTipoEvento;
        private System.Windows.Forms.ComboBox cmbTipoEvento;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.TextBox txtLocalQuermesse;
        private System.Windows.Forms.Label lblDatainicial;
        private System.Windows.Forms.DateTimePicker dtpInicioQuermesse;
        private System.Windows.Forms.Label lblDatafinal;
        private System.Windows.Forms.DateTimePicker dtpFimQuermesse;
        private System.Windows.Forms.Label lblResponsavel;
        private System.Windows.Forms.TextBox txtResponsavelQuermesse;
        private System.Windows.Forms.Label lblSituacao;
        private System.Windows.Forms.ComboBox cmbSituacaoQuermesse;
        private System.Windows.Forms.Label lblObservacao;
        private System.Windows.Forms.TextBox txtObservacaoQuermesse;
    }
}
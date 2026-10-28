namespace CASE.Quermesse
{
    partial class EscalaForm
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
            this.lblQuermesse = new System.Windows.Forms.Label();
            this.cmbEventoEscala = new System.Windows.Forms.ComboBox();
            this.lblFuncionario = new System.Windows.Forms.Label();
            this.cmbPessoaEscala = new System.Windows.Forms.ComboBox();
            this.lblSetorbarraca = new System.Windows.Forms.Label();
            this.cmbSetorEscala = new System.Windows.Forms.ComboBox();
            this.lblData = new System.Windows.Forms.Label();
            this.dtpDiaEscala = new System.Windows.Forms.DateTimePicker();
            this.lblTurno = new System.Windows.Forms.Label();
            this.cmbTurnoEscala = new System.Windows.Forms.ComboBox();
            this.lblEntrada = new System.Windows.Forms.Label();
            this.dtpHoraInicioEscala = new System.Windows.Forms.DateTimePicker();
            this.lblSaida = new System.Windows.Forms.Label();
            this.dtpHoraFimEscala = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeFormulario)).BeginInit();
            // panelFormulario
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Location = new System.Drawing.Point(16, 16);
            this.panelFormulario.Size = new System.Drawing.Size(608, 538);
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
            this.lblTitulo.Text = "Alocação de funcionário";
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
            this.pictureIconeFormulario.Image = global::CASE.Properties.Resources.LucideAzulEscala;
            this.pictureIconeFormulario.TabStop = false;
            // labelAvisoFormulario
            this.labelAvisoFormulario.Name = "labelAvisoFormulario";
            this.labelAvisoFormulario.Location = new System.Drawing.Point(28, 444);
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
            this.btnCancelar.Location = new System.Drawing.Point(320, 490);
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
            this.btnSalvarVisual.Location = new System.Drawing.Point(450, 490);
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
            // lblQuermesse
            this.lblQuermesse.Name = "lblQuermesse";
            this.lblQuermesse.Location = new System.Drawing.Point(28, 72);
            this.lblQuermesse.Size = new System.Drawing.Size(550, 22);
            this.lblQuermesse.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblQuermesse.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblQuermesse.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblQuermesse.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblQuermesse.TabIndex = 5;
            this.lblQuermesse.Text = "Evento";
            this.lblQuermesse.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbEventoEscala
            this.cmbEventoEscala.Name = "cmbEventoEscala";
            this.cmbEventoEscala.Location = new System.Drawing.Point(28, 94);
            this.cmbEventoEscala.Size = new System.Drawing.Size(550, 29);
            this.cmbEventoEscala.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbEventoEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbEventoEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbEventoEscala.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbEventoEscala.TabIndex = 6;
            this.cmbEventoEscala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventoEscala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEventoEscala.Items.AddRange(new object[] { "Quermesse de São João 2026" });
            this.cmbEventoEscala.SelectedIndex = 0;
            // lblFuncionario
            this.lblFuncionario.Name = "lblFuncionario";
            this.lblFuncionario.Location = new System.Drawing.Point(28, 123);
            this.lblFuncionario.Size = new System.Drawing.Size(550, 22);
            this.lblFuncionario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblFuncionario.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblFuncionario.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblFuncionario.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblFuncionario.TabIndex = 7;
            this.lblFuncionario.Text = "Funcionário";
            this.lblFuncionario.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbPessoaEscala
            this.cmbPessoaEscala.Name = "cmbPessoaEscala";
            this.cmbPessoaEscala.Location = new System.Drawing.Point(28, 145);
            this.cmbPessoaEscala.Size = new System.Drawing.Size(550, 29);
            this.cmbPessoaEscala.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbPessoaEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbPessoaEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbPessoaEscala.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbPessoaEscala.TabIndex = 8;
            this.cmbPessoaEscala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPessoaEscala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPessoaEscala.Items.AddRange(new object[] { "Marina Souza", "Pedro Lima", "Julia Santos" });
            this.cmbPessoaEscala.SelectedIndex = 0;
            // lblSetorbarraca
            this.lblSetorbarraca.Name = "lblSetorbarraca";
            this.lblSetorbarraca.Location = new System.Drawing.Point(28, 174);
            this.lblSetorbarraca.Size = new System.Drawing.Size(550, 22);
            this.lblSetorbarraca.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSetorbarraca.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblSetorbarraca.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblSetorbarraca.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSetorbarraca.TabIndex = 9;
            this.lblSetorbarraca.Text = "Setor / barraca";
            this.lblSetorbarraca.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbSetorEscala
            this.cmbSetorEscala.Name = "cmbSetorEscala";
            this.cmbSetorEscala.Location = new System.Drawing.Point(28, 196);
            this.cmbSetorEscala.Size = new System.Drawing.Size(550, 29);
            this.cmbSetorEscala.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbSetorEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbSetorEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbSetorEscala.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbSetorEscala.TabIndex = 10;
            this.cmbSetorEscala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetorEscala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSetorEscala.Items.AddRange(new object[] { "Bebidas", "Alimentação", "Doces", "Brincadeiras" });
            this.cmbSetorEscala.SelectedIndex = 0;
            // lblData
            this.lblData.Name = "lblData";
            this.lblData.Location = new System.Drawing.Point(28, 225);
            this.lblData.Size = new System.Drawing.Size(550, 22);
            this.lblData.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblData.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblData.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblData.TabIndex = 11;
            this.lblData.Text = "Data";
            this.lblData.TextAlign = (System.Drawing.ContentAlignment)1;
            // dtpDiaEscala
            this.dtpDiaEscala.Name = "dtpDiaEscala";
            this.dtpDiaEscala.Location = new System.Drawing.Point(28, 247);
            this.dtpDiaEscala.Size = new System.Drawing.Size(550, 25);
            this.dtpDiaEscala.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.dtpDiaEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.dtpDiaEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.dtpDiaEscala.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0, 0);
            this.dtpDiaEscala.TabIndex = 12;
            this.dtpDiaEscala.Format = (System.Windows.Forms.DateTimePickerFormat)8;
            this.dtpDiaEscala.CustomFormat = "dd/MM/yyyy";
            this.dtpDiaEscala.ShowUpDown = false;
            this.dtpDiaEscala.Value = new System.DateTime(2026, 10, 22, 15, 49, 0);
            // lblTurno
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Location = new System.Drawing.Point(28, 276);
            this.lblTurno.Size = new System.Drawing.Size(550, 22);
            this.lblTurno.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblTurno.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblTurno.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblTurno.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblTurno.TabIndex = 13;
            this.lblTurno.Text = "Turno";
            this.lblTurno.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbTurnoEscala
            this.cmbTurnoEscala.Name = "cmbTurnoEscala";
            this.cmbTurnoEscala.Location = new System.Drawing.Point(28, 298);
            this.cmbTurnoEscala.Size = new System.Drawing.Size(550, 29);
            this.cmbTurnoEscala.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbTurnoEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbTurnoEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbTurnoEscala.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbTurnoEscala.TabIndex = 14;
            this.cmbTurnoEscala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTurnoEscala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTurnoEscala.Items.AddRange(new object[] { "Manhã", "Tarde", "Noite" });
            this.cmbTurnoEscala.SelectedIndex = 2;
            // lblEntrada
            this.lblEntrada.Name = "lblEntrada";
            this.lblEntrada.Location = new System.Drawing.Point(28, 327);
            this.lblEntrada.Size = new System.Drawing.Size(550, 22);
            this.lblEntrada.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblEntrada.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblEntrada.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblEntrada.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblEntrada.TabIndex = 15;
            this.lblEntrada.Text = "Entrada";
            this.lblEntrada.TextAlign = (System.Drawing.ContentAlignment)1;
            // dtpHoraInicioEscala
            this.dtpHoraInicioEscala.Name = "dtpHoraInicioEscala";
            this.dtpHoraInicioEscala.Location = new System.Drawing.Point(28, 349);
            this.dtpHoraInicioEscala.Size = new System.Drawing.Size(550, 25);
            this.dtpHoraInicioEscala.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.dtpHoraInicioEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.dtpHoraInicioEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.dtpHoraInicioEscala.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0, 0);
            this.dtpHoraInicioEscala.TabIndex = 16;
            this.dtpHoraInicioEscala.Format = (System.Windows.Forms.DateTimePickerFormat)8;
            this.dtpHoraInicioEscala.CustomFormat = "HH:mm";
            this.dtpHoraInicioEscala.ShowUpDown = true;
            this.dtpHoraInicioEscala.Value = new System.DateTime(2026, 10, 22, 18, 0, 0);
            // lblSaida
            this.lblSaida.Name = "lblSaida";
            this.lblSaida.Location = new System.Drawing.Point(28, 378);
            this.lblSaida.Size = new System.Drawing.Size(550, 22);
            this.lblSaida.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSaida.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblSaida.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblSaida.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSaida.TabIndex = 17;
            this.lblSaida.Text = "Saída";
            this.lblSaida.TextAlign = (System.Drawing.ContentAlignment)1;
            // dtpHoraFimEscala
            this.dtpHoraFimEscala.Name = "dtpHoraFimEscala";
            this.dtpHoraFimEscala.Location = new System.Drawing.Point(28, 400);
            this.dtpHoraFimEscala.Size = new System.Drawing.Size(550, 25);
            this.dtpHoraFimEscala.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.dtpHoraFimEscala.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.dtpHoraFimEscala.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.dtpHoraFimEscala.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0, 0);
            this.dtpHoraFimEscala.TabIndex = 18;
            this.dtpHoraFimEscala.Format = (System.Windows.Forms.DateTimePickerFormat)8;
            this.dtpHoraFimEscala.CustomFormat = "HH:mm";
            this.dtpHoraFimEscala.ShowUpDown = true;
            this.dtpHoraFimEscala.Value = new System.DateTime(2026, 10, 22, 23, 0, 0);
            this.Controls.Add(this.panelFormulario);
            this.panelFormulario.Controls.Add(this.lblTitulo);
            this.panelFormulario.Controls.Add(this.pictureIconeFormulario);
            this.panelFormulario.Controls.Add(this.labelAvisoFormulario);
            this.panelFormulario.Controls.Add(this.btnCancelar);
            this.panelFormulario.Controls.Add(this.btnSalvarVisual);
            this.panelFormulario.Controls.Add(this.lblQuermesse);
            this.panelFormulario.Controls.Add(this.cmbEventoEscala);
            this.panelFormulario.Controls.Add(this.lblFuncionario);
            this.panelFormulario.Controls.Add(this.cmbPessoaEscala);
            this.panelFormulario.Controls.Add(this.lblSetorbarraca);
            this.panelFormulario.Controls.Add(this.cmbSetorEscala);
            this.panelFormulario.Controls.Add(this.lblData);
            this.panelFormulario.Controls.Add(this.dtpDiaEscala);
            this.panelFormulario.Controls.Add(this.lblTurno);
            this.panelFormulario.Controls.Add(this.cmbTurnoEscala);
            this.panelFormulario.Controls.Add(this.lblEntrada);
            this.panelFormulario.Controls.Add(this.dtpHoraInicioEscala);
            this.panelFormulario.Controls.Add(this.lblSaida);
            this.panelFormulario.Controls.Add(this.dtpHoraFimEscala);
            this.Name = "EscalaForm";
            this.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.BackColor = System.Drawing.Color.FromArgb(255, 246, 248, 252);
            this.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Text = "Alocação de funcionário";
            this.ClientSize = new System.Drawing.Size(640, 570);
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
        private System.Windows.Forms.Label lblQuermesse;
        private System.Windows.Forms.ComboBox cmbEventoEscala;
        private System.Windows.Forms.Label lblFuncionario;
        private System.Windows.Forms.ComboBox cmbPessoaEscala;
        private System.Windows.Forms.Label lblSetorbarraca;
        private System.Windows.Forms.ComboBox cmbSetorEscala;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.DateTimePicker dtpDiaEscala;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.ComboBox cmbTurnoEscala;
        private System.Windows.Forms.Label lblEntrada;
        private System.Windows.Forms.DateTimePicker dtpHoraInicioEscala;
        private System.Windows.Forms.Label lblSaida;
        private System.Windows.Forms.DateTimePicker dtpHoraFimEscala;
    }
}
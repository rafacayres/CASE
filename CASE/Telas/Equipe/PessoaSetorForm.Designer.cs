namespace CASE.Quermesse
{
    partial class PessoaSetorForm
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
            this.btnSalvarFuncionario = new System.Windows.Forms.Button();
            this.lblNomecompleto = new System.Windows.Forms.Label();
            this.txtNomeFuncionario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtLoginFuncionario = new System.Windows.Forms.TextBox();
            this.lblPerfildeacesso = new System.Windows.Forms.Label();
            this.cmbPerfilFuncionario = new System.Windows.Forms.ComboBox();
            this.lblSetorbarraca = new System.Windows.Forms.Label();
            this.cmbSetorFuncionario = new System.Windows.Forms.ComboBox();
            this.lblTurno = new System.Windows.Forms.Label();
            this.cmbTurnoFuncionario = new System.Windows.Forms.ComboBox();
            this.chkFuncionarioAtivo = new System.Windows.Forms.CheckBox();
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
            this.lblTitulo.Text = "Novo funcionário";
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
            this.pictureIconeFormulario.Image = global::CASE.Properties.Resources.LucideAzulUsuario;
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
            // btnSalvarFuncionario
            this.btnSalvarFuncionario.Name = "btnSalvarFuncionario";
            this.btnSalvarFuncionario.Location = new System.Drawing.Point(450, 490);
            this.btnSalvarFuncionario.Size = new System.Drawing.Size(134, 35);
            this.btnSalvarFuncionario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.btnSalvarFuncionario.Font = new System.Drawing.Font("Segoe UI", 9.5F, (System.Drawing.FontStyle)1);
            this.btnSalvarFuncionario.BackColor = System.Drawing.Color.FromArgb(255, 23, 105, 210);
            this.btnSalvarFuncionario.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnSalvarFuncionario.TabIndex = 4;
            this.btnSalvarFuncionario.Text = "Salvar vínculo";
            this.btnSalvarFuncionario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarFuncionario.FlatAppearance.BorderSize = 0;
            this.btnSalvarFuncionario.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 228, 234, 242);
            this.btnSalvarFuncionario.UseVisualStyleBackColor = false;
            this.btnSalvarFuncionario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarFuncionario.DialogResult = (System.Windows.Forms.DialogResult)0;
            // lblNomecompleto
            this.lblNomecompleto.Name = "lblNomecompleto";
            this.lblNomecompleto.Location = new System.Drawing.Point(28, 72);
            this.lblNomecompleto.Size = new System.Drawing.Size(550, 22);
            this.lblNomecompleto.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblNomecompleto.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblNomecompleto.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblNomecompleto.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblNomecompleto.TabIndex = 5;
            this.lblNomecompleto.Text = "Nome completo";
            this.lblNomecompleto.TextAlign = (System.Drawing.ContentAlignment)1;
            // txtNomeFuncionario
            this.txtNomeFuncionario.Name = "txtNomeFuncionario";
            this.txtNomeFuncionario.Location = new System.Drawing.Point(36, 98);
            this.txtNomeFuncionario.Size = new System.Drawing.Size(530, 25);
            this.txtNomeFuncionario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.txtNomeFuncionario.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.txtNomeFuncionario.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.txtNomeFuncionario.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.txtNomeFuncionario.TabIndex = 6;
            this.txtNomeFuncionario.Text = "";
            this.txtNomeFuncionario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNomeFuncionario.Multiline = false;
            this.txtNomeFuncionario.MaxLength = 32767;
            // lblUsuario
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Location = new System.Drawing.Point(28, 123);
            this.lblUsuario.Size = new System.Drawing.Size(550, 22);
            this.lblUsuario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblUsuario.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblUsuario.TabIndex = 7;
            this.lblUsuario.Text = "Usuário";
            this.lblUsuario.TextAlign = (System.Drawing.ContentAlignment)1;
            // txtLoginFuncionario
            this.txtLoginFuncionario.Name = "txtLoginFuncionario";
            this.txtLoginFuncionario.Location = new System.Drawing.Point(36, 149);
            this.txtLoginFuncionario.Size = new System.Drawing.Size(530, 25);
            this.txtLoginFuncionario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.txtLoginFuncionario.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.txtLoginFuncionario.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.txtLoginFuncionario.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.txtLoginFuncionario.TabIndex = 8;
            this.txtLoginFuncionario.Text = "";
            this.txtLoginFuncionario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLoginFuncionario.Multiline = false;
            this.txtLoginFuncionario.MaxLength = 32767;
            // lblPerfildeacesso
            this.lblPerfildeacesso.Name = "lblPerfildeacesso";
            this.lblPerfildeacesso.Location = new System.Drawing.Point(28, 174);
            this.lblPerfildeacesso.Size = new System.Drawing.Size(550, 22);
            this.lblPerfildeacesso.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblPerfildeacesso.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblPerfildeacesso.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblPerfildeacesso.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblPerfildeacesso.TabIndex = 9;
            this.lblPerfildeacesso.Text = "Perfil de acesso";
            this.lblPerfildeacesso.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbPerfilFuncionario
            this.cmbPerfilFuncionario.Name = "cmbPerfilFuncionario";
            this.cmbPerfilFuncionario.Location = new System.Drawing.Point(28, 196);
            this.cmbPerfilFuncionario.Size = new System.Drawing.Size(550, 29);
            this.cmbPerfilFuncionario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbPerfilFuncionario.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbPerfilFuncionario.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbPerfilFuncionario.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbPerfilFuncionario.TabIndex = 10;
            this.cmbPerfilFuncionario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPerfilFuncionario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPerfilFuncionario.Items.AddRange(new object[] { "Vendedor", "Gerente", "Administrador" });
            this.cmbPerfilFuncionario.SelectedIndex = 0;
            // lblSetorbarraca
            this.lblSetorbarraca.Name = "lblSetorbarraca";
            this.lblSetorbarraca.Location = new System.Drawing.Point(28, 225);
            this.lblSetorbarraca.Size = new System.Drawing.Size(550, 22);
            this.lblSetorbarraca.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSetorbarraca.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblSetorbarraca.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblSetorbarraca.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSetorbarraca.TabIndex = 11;
            this.lblSetorbarraca.Text = "Setor / barraca";
            this.lblSetorbarraca.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbSetorFuncionario
            this.cmbSetorFuncionario.Name = "cmbSetorFuncionario";
            this.cmbSetorFuncionario.Location = new System.Drawing.Point(28, 247);
            this.cmbSetorFuncionario.Size = new System.Drawing.Size(550, 29);
            this.cmbSetorFuncionario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbSetorFuncionario.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbSetorFuncionario.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbSetorFuncionario.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbSetorFuncionario.TabIndex = 12;
            this.cmbSetorFuncionario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetorFuncionario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSetorFuncionario.Items.AddRange(new object[] { "Bebidas", "Alimentação", "Doces", "Brincadeiras" });
            this.cmbSetorFuncionario.SelectedIndex = 0;
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
            // cmbTurnoFuncionario
            this.cmbTurnoFuncionario.Name = "cmbTurnoFuncionario";
            this.cmbTurnoFuncionario.Location = new System.Drawing.Point(28, 298);
            this.cmbTurnoFuncionario.Size = new System.Drawing.Size(550, 29);
            this.cmbTurnoFuncionario.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbTurnoFuncionario.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbTurnoFuncionario.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbTurnoFuncionario.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbTurnoFuncionario.TabIndex = 14;
            this.cmbTurnoFuncionario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTurnoFuncionario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTurnoFuncionario.Items.AddRange(new object[] { "Manhã", "Tarde", "Noite" });
            this.cmbTurnoFuncionario.SelectedIndex = 2;
            // chkFuncionarioAtivo
            this.chkFuncionarioAtivo.Name = "chkFuncionarioAtivo";
            this.chkFuncionarioAtivo.Location = new System.Drawing.Point(28, 352);
            this.chkFuncionarioAtivo.Size = new System.Drawing.Size(260, 28);
            this.chkFuncionarioAtivo.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.chkFuncionarioAtivo.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.chkFuncionarioAtivo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.chkFuncionarioAtivo.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.chkFuncionarioAtivo.TabIndex = 15;
            this.chkFuncionarioAtivo.Text = "Funcionário ativo";
            this.chkFuncionarioAtivo.Checked = true;
            this.Controls.Add(this.panelFormulario);
            this.panelFormulario.Controls.Add(this.lblTitulo);
            this.panelFormulario.Controls.Add(this.pictureIconeFormulario);
            this.panelFormulario.Controls.Add(this.labelAvisoFormulario);
            this.panelFormulario.Controls.Add(this.btnCancelar);
            this.panelFormulario.Controls.Add(this.btnSalvarFuncionario);
            this.panelFormulario.Controls.Add(this.lblNomecompleto);
            this.panelFormulario.Controls.Add(this.txtNomeFuncionario);
            this.panelFormulario.Controls.Add(this.lblUsuario);
            this.panelFormulario.Controls.Add(this.txtLoginFuncionario);
            this.panelFormulario.Controls.Add(this.lblPerfildeacesso);
            this.panelFormulario.Controls.Add(this.cmbPerfilFuncionario);
            this.panelFormulario.Controls.Add(this.lblSetorbarraca);
            this.panelFormulario.Controls.Add(this.cmbSetorFuncionario);
            this.panelFormulario.Controls.Add(this.lblTurno);
            this.panelFormulario.Controls.Add(this.cmbTurnoFuncionario);
            this.panelFormulario.Controls.Add(this.chkFuncionarioAtivo);
            this.Name = "PessoaSetorForm";
            this.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.BackColor = System.Drawing.Color.FromArgb(255, 246, 248, 252);
            this.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Text = "Novo funcionário";
            this.ClientSize = new System.Drawing.Size(640, 570);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AcceptButton = this.btnSalvarFuncionario;
            this.CancelButton = this.btnCancelar;
            this.panelFormulario.ResumeLayout(false);
            this.panelFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeFormulario)).EndInit();
            this.btnSalvarFuncionario.Click += new System.EventHandler(this.btnSalvarFuncionario_Click);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel panelFormulario;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureIconeFormulario;
        private System.Windows.Forms.Label labelAvisoFormulario;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvarFuncionario;
        private System.Windows.Forms.Label lblNomecompleto;
        private System.Windows.Forms.TextBox txtNomeFuncionario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtLoginFuncionario;
        private System.Windows.Forms.Label lblPerfildeacesso;
        private System.Windows.Forms.ComboBox cmbPerfilFuncionario;
        private System.Windows.Forms.Label lblSetorbarraca;
        private System.Windows.Forms.ComboBox cmbSetorFuncionario;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.ComboBox cmbTurnoFuncionario;
        private System.Windows.Forms.CheckBox chkFuncionarioAtivo;
    }
}
namespace CASE
{
    partial class CadastroForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelEsquerdo = new System.Windows.Forms.Panel();
            this.lblCASE = new System.Windows.Forms.Label();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.panelDireito = new System.Windows.Forms.Panel();
            this.panelCadastro = new System.Windows.Forms.Panel();
            this.lblTituloCadastro = new System.Windows.Forms.Label();
            this.lblSubtituloCadastro = new System.Windows.Forms.Label();
            this.lblNomeCompleto = new System.Windows.Forms.Label();
            this.panelNomeCompleto = new System.Windows.Forms.Panel();
            this.txtNomeCompleto = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.panelUsuarioCadastro = new System.Windows.Forms.Panel();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.panelEmail = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            this.panelSenhaCadastro = new System.Windows.Forms.Panel();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblConfirmarSenha = new System.Windows.Forms.Label();
            this.panelConfirmarSenha = new System.Windows.Forms.Panel();
            this.txtConfirmarSenha = new System.Windows.Forms.TextBox();
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.linkVoltarLogin = new System.Windows.Forms.LinkLabel();
            this.panelEsquerdo.SuspendLayout();
            this.panelDireito.SuspendLayout();
            this.panelCadastro.SuspendLayout();
            this.panelNomeCompleto.SuspendLayout();
            this.panelUsuarioCadastro.SuspendLayout();
            this.panelEmail.SuspendLayout();
            this.panelSenhaCadastro.SuspendLayout();
            this.panelConfirmarSenha.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEsquerdo
            // 
            this.panelEsquerdo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.panelEsquerdo.Controls.Add(this.lblCASE);
            this.panelEsquerdo.Controls.Add(this.lblDescricao);
            this.panelEsquerdo.Location = new System.Drawing.Point(0, 0);
            this.panelEsquerdo.Name = "panelEsquerdo";
            this.panelEsquerdo.Size = new System.Drawing.Size(400, 550);
            this.panelEsquerdo.TabIndex = 0;
            // 
            // lblCASE
            // 
            this.lblCASE.AutoSize = true;
            this.lblCASE.Font = new System.Drawing.Font("Segoe UI", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCASE.ForeColor = System.Drawing.Color.White;
            this.lblCASE.Location = new System.Drawing.Point(125, 205);
            this.lblCASE.Name = "lblCASE";
            this.lblCASE.Size = new System.Drawing.Size(138, 62);
            this.lblCASE.TabIndex = 0;
            this.lblCASE.Text = "CASE";
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricao.ForeColor = System.Drawing.Color.White;
            this.lblDescricao.Location = new System.Drawing.Point(128, 278);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(150, 21);
            this.lblDescricao.TabIndex = 1;
            this.lblDescricao.Text = "Controle de Estoque";
            // 
            // panelDireito
            // 
            this.panelDireito.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.panelDireito.Controls.Add(this.panelCadastro);
            this.panelDireito.Location = new System.Drawing.Point(400, 0);
            this.panelDireito.Name = "panelDireito";
            this.panelDireito.Size = new System.Drawing.Size(500, 550);
            this.panelDireito.TabIndex = 1;
            // 
            // panelCadastro
            // 
            this.panelCadastro.BackColor = System.Drawing.Color.White;
            this.panelCadastro.Controls.Add(this.lblTituloCadastro);
            this.panelCadastro.Controls.Add(this.lblSubtituloCadastro);
            this.panelCadastro.Controls.Add(this.lblNomeCompleto);
            this.panelCadastro.Controls.Add(this.panelNomeCompleto);
            this.panelCadastro.Controls.Add(this.lblUsuario);
            this.panelCadastro.Controls.Add(this.panelUsuarioCadastro);
            this.panelCadastro.Controls.Add(this.lblEmail);
            this.panelCadastro.Controls.Add(this.panelEmail);
            this.panelCadastro.Controls.Add(this.lblSenha);
            this.panelCadastro.Controls.Add(this.panelSenhaCadastro);
            this.panelCadastro.Controls.Add(this.lblConfirmarSenha);
            this.panelCadastro.Controls.Add(this.panelConfirmarSenha);
            this.panelCadastro.Controls.Add(this.btnCadastrar);
            this.panelCadastro.Controls.Add(this.linkVoltarLogin);
            this.panelCadastro.Location = new System.Drawing.Point(60, 30);
            this.panelCadastro.Name = "panelCadastro";
            this.panelCadastro.Size = new System.Drawing.Size(380, 490);
            this.panelCadastro.TabIndex = 0;
            // 
            // lblTituloCadastro
            // 
            this.lblTituloCadastro.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloCadastro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.lblTituloCadastro.Location = new System.Drawing.Point(25, 18);
            this.lblTituloCadastro.Name = "lblTituloCadastro";
            this.lblTituloCadastro.Size = new System.Drawing.Size(330, 32);
            this.lblTituloCadastro.TabIndex = 0;
            this.lblTituloCadastro.Text = "Criar conta";
            this.lblTituloCadastro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubtituloCadastro
            // 
            this.lblSubtituloCadastro.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtituloCadastro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtituloCadastro.Location = new System.Drawing.Point(25, 53);
            this.lblSubtituloCadastro.Name = "lblSubtituloCadastro";
            this.lblSubtituloCadastro.Size = new System.Drawing.Size(330, 25);
            this.lblSubtituloCadastro.TabIndex = 1;
            this.lblSubtituloCadastro.Text = "Preencha os dados para solicitar acesso";
            this.lblSubtituloCadastro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNomeCompleto
            // 
            this.lblNomeCompleto.BackColor = System.Drawing.Color.Transparent;
            this.lblNomeCompleto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNomeCompleto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.lblNomeCompleto.Location = new System.Drawing.Point(25, 88);
            this.lblNomeCompleto.Name = "lblNomeCompleto";
            this.lblNomeCompleto.Size = new System.Drawing.Size(330, 18);
            this.lblNomeCompleto.TabIndex = 2;
            this.lblNomeCompleto.Text = "Nome completo";
            this.lblNomeCompleto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelNomeCompleto
            // 
            this.panelNomeCompleto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelNomeCompleto.Controls.Add(this.txtNomeCompleto);
            this.panelNomeCompleto.Location = new System.Drawing.Point(25, 108);
            this.panelNomeCompleto.Name = "panelNomeCompleto";
            this.panelNomeCompleto.Size = new System.Drawing.Size(330, 30);
            this.panelNomeCompleto.TabIndex = 3;
            // 
            // txtNomeCompleto
            // 
            this.txtNomeCompleto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtNomeCompleto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNomeCompleto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNomeCompleto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.txtNomeCompleto.Location = new System.Drawing.Point(10, 5);
            this.txtNomeCompleto.Name = "txtNomeCompleto";
            this.txtNomeCompleto.Size = new System.Drawing.Size(310, 18);
            this.txtNomeCompleto.TabIndex = 0;
            // 
            // lblUsuario
            // 
            this.lblUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.lblUsuario.Location = new System.Drawing.Point(25, 146);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(330, 18);
            this.lblUsuario.TabIndex = 4;
            this.lblUsuario.Text = "Nome de usuário";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelUsuarioCadastro
            // 
            this.panelUsuarioCadastro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelUsuarioCadastro.Controls.Add(this.txtUsuario);
            this.panelUsuarioCadastro.Location = new System.Drawing.Point(25, 166);
            this.panelUsuarioCadastro.Name = "panelUsuarioCadastro";
            this.panelUsuarioCadastro.Size = new System.Drawing.Size(330, 30);
            this.panelUsuarioCadastro.TabIndex = 5;
            // 
            // txtUsuario
            // 
            this.txtUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.txtUsuario.Location = new System.Drawing.Point(10, 5);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(310, 18);
            this.txtUsuario.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.lblEmail.Location = new System.Drawing.Point(25, 204);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(330, 18);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "E-mail";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelEmail
            // 
            this.panelEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelEmail.Controls.Add(this.txtEmail);
            this.panelEmail.Location = new System.Drawing.Point(25, 224);
            this.panelEmail.Name = "panelEmail";
            this.panelEmail.Size = new System.Drawing.Size(330, 30);
            this.panelEmail.TabIndex = 7;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.txtEmail.Location = new System.Drawing.Point(10, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(310, 18);
            this.txtEmail.TabIndex = 2;
            // 
            // lblSenha
            // 
            this.lblSenha.BackColor = System.Drawing.Color.Transparent;
            this.lblSenha.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.lblSenha.Location = new System.Drawing.Point(25, 262);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(330, 18);
            this.lblSenha.TabIndex = 8;
            this.lblSenha.Text = "Senha";
            this.lblSenha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelSenhaCadastro
            // 
            this.panelSenhaCadastro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelSenhaCadastro.Controls.Add(this.txtSenha);
            this.panelSenhaCadastro.Location = new System.Drawing.Point(25, 282);
            this.panelSenhaCadastro.Name = "panelSenhaCadastro";
            this.panelSenhaCadastro.Size = new System.Drawing.Size(330, 30);
            this.panelSenhaCadastro.TabIndex = 9;
            // 
            // txtSenha
            // 
            this.txtSenha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSenha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.txtSenha.Location = new System.Drawing.Point(10, 5);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(310, 18);
            this.txtSenha.TabIndex = 3;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // lblConfirmarSenha
            // 
            this.lblConfirmarSenha.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirmarSenha.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConfirmarSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.lblConfirmarSenha.Location = new System.Drawing.Point(25, 320);
            this.lblConfirmarSenha.Name = "lblConfirmarSenha";
            this.lblConfirmarSenha.Size = new System.Drawing.Size(330, 18);
            this.lblConfirmarSenha.TabIndex = 10;
            this.lblConfirmarSenha.Text = "Confirmar senha";
            this.lblConfirmarSenha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelConfirmarSenha
            // 
            this.panelConfirmarSenha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelConfirmarSenha.Controls.Add(this.txtConfirmarSenha);
            this.panelConfirmarSenha.Location = new System.Drawing.Point(25, 340);
            this.panelConfirmarSenha.Name = "panelConfirmarSenha";
            this.panelConfirmarSenha.Size = new System.Drawing.Size(330, 30);
            this.panelConfirmarSenha.TabIndex = 11;
            // 
            // txtConfirmarSenha
            // 
            this.txtConfirmarSenha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtConfirmarSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConfirmarSenha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmarSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.txtConfirmarSenha.Location = new System.Drawing.Point(10, 5);
            this.txtConfirmarSenha.Name = "txtConfirmarSenha";
            this.txtConfirmarSenha.Size = new System.Drawing.Size(310, 18);
            this.txtConfirmarSenha.TabIndex = 4;
            this.txtConfirmarSenha.UseSystemPasswordChar = true;
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(105)))), ((int)(((byte)(210)))));
            this.btnCadastrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastrar.FlatAppearance.BorderSize = 0;
            this.btnCadastrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCadastrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastrar.ForeColor = System.Drawing.Color.White;
            this.btnCadastrar.Location = new System.Drawing.Point(25, 387);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(330, 40);
            this.btnCadastrar.TabIndex = 12;
            this.btnCadastrar.Text = "Solicitar cadastro";
            this.btnCadastrar.UseVisualStyleBackColor = false;
            // 
            // linkVoltarLogin
            // 
            this.linkVoltarLogin.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.linkVoltarLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linkVoltarLogin.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(105)))), ((int)(((byte)(210)))));
            this.linkVoltarLogin.Location = new System.Drawing.Point(25, 445);
            this.linkVoltarLogin.Name = "linkVoltarLogin";
            this.linkVoltarLogin.Size = new System.Drawing.Size(330, 22);
            this.linkVoltarLogin.TabIndex = 13;
            this.linkVoltarLogin.TabStop = true;
            this.linkVoltarLogin.Text = "Já tem uma conta? Voltar ao login";
            this.linkVoltarLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkVoltarLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkVoltarLogin_LinkClicked);
            // 
            // CadastroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.panelDireito);
            this.Controls.Add(this.panelEsquerdo);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CadastroForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CASE - Criar Conta";
            this.panelEsquerdo.ResumeLayout(false);
            this.panelEsquerdo.PerformLayout();
            this.panelDireito.ResumeLayout(false);
            this.panelCadastro.ResumeLayout(false);
            this.panelNomeCompleto.ResumeLayout(false);
            this.panelNomeCompleto.PerformLayout();
            this.panelUsuarioCadastro.ResumeLayout(false);
            this.panelUsuarioCadastro.PerformLayout();
            this.panelEmail.ResumeLayout(false);
            this.panelEmail.PerformLayout();
            this.panelSenhaCadastro.ResumeLayout(false);
            this.panelSenhaCadastro.PerformLayout();
            this.panelConfirmarSenha.ResumeLayout(false);
            this.panelConfirmarSenha.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEsquerdo;
        private System.Windows.Forms.Label lblCASE;
        private System.Windows.Forms.Label lblDescricao;

        private System.Windows.Forms.Panel panelDireito;
        private System.Windows.Forms.Panel panelCadastro;

        private System.Windows.Forms.Label lblTituloCadastro;
        private System.Windows.Forms.Label lblSubtituloCadastro;

        private System.Windows.Forms.Label lblNomeCompleto;
        private System.Windows.Forms.Panel panelNomeCompleto;
        private System.Windows.Forms.TextBox txtNomeCompleto;

        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Panel panelUsuarioCadastro;
        private System.Windows.Forms.TextBox txtUsuario;

        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Panel panelEmail;
        private System.Windows.Forms.TextBox txtEmail;

        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.Panel panelSenhaCadastro;
        private System.Windows.Forms.TextBox txtSenha;

        private System.Windows.Forms.Label lblConfirmarSenha;
        private System.Windows.Forms.Panel panelConfirmarSenha;
        private System.Windows.Forms.TextBox txtConfirmarSenha;

        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.LinkLabel linkVoltarLogin;
    }
}
namespace CASE
{
    partial class FornecedoresControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.panelBusca = new System.Windows.Forms.Panel();
            this.lblBusca = new System.Windows.Forms.Label();
            this.txtBusca = new System.Windows.Forms.TextBox();
            this.btnNovo = new System.Windows.Forms.Button();
            this.panelLista = new System.Windows.Forms.Panel();
            this.dgvFornecedores = new System.Windows.Forms.DataGridView();
            this.panelEdicao = new System.Windows.Forms.Panel();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.pictureIconeTela = new System.Windows.Forms.PictureBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBusca.SuspendLayout();
            this.panelLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFornecedores)).BeginInit();
            this.panelEdicao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.lblTitulo.Location = new System.Drawing.Point(88, 24);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(690, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Fornecedores";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(90, 70);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(926, 30);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Gerencie fornecedores e seus contatos";
            // 
            // panelBusca
            // 
            this.panelBusca.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBusca.BackColor = System.Drawing.Color.White;
            this.panelBusca.Controls.Add(this.lblBusca);
            this.panelBusca.Controls.Add(this.txtBusca);
            this.panelBusca.Controls.Add(this.btnNovo);
            this.panelBusca.Location = new System.Drawing.Point(24, 112);
            this.panelBusca.Name = "panelBusca";
            this.panelBusca.Size = new System.Drawing.Size(1002, 88);
            this.panelBusca.TabIndex = 2;
            // 
            // lblBusca
            // 
            this.lblBusca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBusca.Location = new System.Drawing.Point(24, 10);
            this.lblBusca.Name = "lblBusca";
            this.lblBusca.Size = new System.Drawing.Size(680, 24);
            this.lblBusca.TabIndex = 3;
            this.lblBusca.Text = "Buscar por nome ou documento";
            // 
            // txtBusca
            // 
            this.txtBusca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusca.Location = new System.Drawing.Point(24, 40);
            this.txtBusca.Name = "txtBusca";
            this.txtBusca.Size = new System.Drawing.Size(680, 23);
            this.txtBusca.TabIndex = 4;
            // 
            // btnNovo
            // 
            this.btnNovo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(105)))), ((int)(((byte)(210)))));
            this.btnNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.Location = new System.Drawing.Point(790, 28);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(184, 40);
            this.btnNovo.TabIndex = 5;
            this.btnNovo.Text = "Novo fornecedor";
            this.btnNovo.UseVisualStyleBackColor = false;
            // 
            // panelLista
            // 
            this.panelLista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLista.BackColor = System.Drawing.Color.White;
            this.panelLista.Controls.Add(this.dgvFornecedores);
            this.panelLista.Location = new System.Drawing.Point(24, 216);
            this.panelLista.Name = "panelLista";
            this.panelLista.Size = new System.Drawing.Size(1002, 224);
            this.panelLista.TabIndex = 6;
            // 
            // dgvFornecedores
            // 
            this.dgvFornecedores.AllowUserToAddRows = false;
            this.dgvFornecedores.AllowUserToDeleteRows = false;
            this.dgvFornecedores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFornecedores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFornecedores.BackgroundColor = System.Drawing.Color.White;
            this.dgvFornecedores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvFornecedores.EnableHeadersVisualStyles = false;
            this.dgvFornecedores.Location = new System.Drawing.Point(24, 20);
            this.dgvFornecedores.MultiSelect = false;
            this.dgvFornecedores.Name = "dgvFornecedores";
            this.dgvFornecedores.ReadOnly = true;
            this.dgvFornecedores.RowHeadersVisible = false;
            this.dgvFornecedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFornecedores.Size = new System.Drawing.Size(954, 184);
            this.dgvFornecedores.TabIndex = 7;
            // 
            // panelEdicao
            // 
            this.panelEdicao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEdicao.BackColor = System.Drawing.Color.White;
            this.panelEdicao.Controls.Add(this.lblNome);
            this.panelEdicao.Controls.Add(this.txtNome);
            this.panelEdicao.Controls.Add(this.lblDocumento);
            this.panelEdicao.Controls.Add(this.txtDocumento);
            this.panelEdicao.Controls.Add(this.lblEmail);
            this.panelEdicao.Controls.Add(this.txtEmail);
            this.panelEdicao.Controls.Add(this.lblTelefone);
            this.panelEdicao.Controls.Add(this.txtTelefone);
            this.panelEdicao.Controls.Add(this.btnSalvar);
            this.panelEdicao.Location = new System.Drawing.Point(24, 456);
            this.panelEdicao.Name = "panelEdicao";
            this.panelEdicao.Size = new System.Drawing.Size(1002, 208);
            this.panelEdicao.TabIndex = 8;
            // 
            // lblNome
            // 
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNome.Location = new System.Drawing.Point(24, 20);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(450, 24);
            this.lblNome.TabIndex = 9;
            this.lblNome.Text = "Nome *";
            // 
            // txtNome
            // 
            this.txtNome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNome.Location = new System.Drawing.Point(24, 50);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(450, 23);
            this.txtNome.TabIndex = 10;
            // 
            // lblDocumento
            // 
            this.lblDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDocumento.Location = new System.Drawing.Point(510, 20);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(450, 24);
            this.lblDocumento.TabIndex = 11;
            this.lblDocumento.Text = "CPF / CNPJ (opcional)";
            // 
            // txtDocumento
            // 
            this.txtDocumento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocumento.Location = new System.Drawing.Point(510, 50);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(450, 23);
            this.txtDocumento.TabIndex = 12;
            // 
            // lblEmail
            // 
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblEmail.Location = new System.Drawing.Point(24, 100);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(300, 24);
            this.lblEmail.TabIndex = 13;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Location = new System.Drawing.Point(24, 130);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 23);
            this.txtEmail.TabIndex = 14;
            // 
            // lblTelefone
            // 
            this.lblTelefone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTelefone.Location = new System.Drawing.Point(350, 100);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(250, 24);
            this.lblTelefone.TabIndex = 15;
            this.lblTelefone.Text = "Telefone";
            // 
            // txtTelefone
            // 
            this.txtTelefone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefone.Location = new System.Drawing.Point(350, 130);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(250, 23);
            this.txtTelefone.TabIndex = 16;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(105)))), ((int)(((byte)(210)))));
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(730, 132);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(230, 40);
            this.btnSalvar.TabIndex = 17;
            this.btnSalvar.Text = "Salvar fornecedor";
            this.btnSalvar.UseVisualStyleBackColor = false;
            // 
            // lblMensagem
            // 
            this.lblMensagem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensagem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMensagem.Location = new System.Drawing.Point(30, 680);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(990, 24);
            this.lblMensagem.TabIndex = 18;
            this.lblMensagem.Text = "Selecione um fornecedor para editar ou crie um novo.";
            // 
            // pictureIconeTela
            // 
            this.pictureIconeTela.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.pictureIconeTela.Image = global::CASE.Properties.Resources.LucideAzulFornecedores;
            this.pictureIconeTela.Location = new System.Drawing.Point(30, 28);
            this.pictureIconeTela.Name = "pictureIconeTela";
            this.pictureIconeTela.Padding = new System.Windows.Forms.Padding(6);
            this.pictureIconeTela.Size = new System.Drawing.Size(44, 44);
            this.pictureIconeTela.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureIconeTela.TabIndex = 19;
            this.pictureIconeTela.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Nome";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Documento";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Email";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Telefone";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // FornecedoresControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1050, 720);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.panelBusca);
            this.Controls.Add(this.panelLista);
            this.Controls.Add(this.panelEdicao);
            this.Controls.Add(this.lblMensagem);
            this.Controls.Add(this.pictureIconeTela);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FornecedoresControl";
            this.Size = new System.Drawing.Size(1050, 720);
            this.panelBusca.ResumeLayout(false);
            this.panelBusca.PerformLayout();
            this.panelLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFornecedores)).EndInit();
            this.panelEdicao.ResumeLayout(false);
            this.panelEdicao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.PictureBox pictureIconeTela;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelBusca;
        private System.Windows.Forms.Label lblBusca;
        private System.Windows.Forms.TextBox txtBusca;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Panel panelLista;
        private System.Windows.Forms.DataGridView dgvFornecedores;
        private System.Windows.Forms.Panel panelEdicao;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}

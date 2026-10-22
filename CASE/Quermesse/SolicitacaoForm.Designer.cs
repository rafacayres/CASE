namespace CASE.Quermesse
{
    partial class SolicitacaoForm
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
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipoSolicitacao = new System.Windows.Forms.ComboBox();
            this.lblSetor = new System.Windows.Forms.Label();
            this.cmbSetorSolicitacao = new System.Windows.Forms.ComboBox();
            this.lblSolicitante = new System.Windows.Forms.Label();
            this.txtSolicitante = new System.Windows.Forms.TextBox();
            this.lblProduto = new System.Windows.Forms.Label();
            this.txtProdutoSolicitacao = new System.Windows.Forms.TextBox();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.txtQuantidadeSolicitacao = new System.Windows.Forms.TextBox();
            this.lblObservacao = new System.Windows.Forms.Label();
            this.txtObservacaoSolicitacao = new System.Windows.Forms.TextBox();
            this.lblSituacao = new System.Windows.Forms.Label();
            this.cmbStatusSolicitacao = new System.Windows.Forms.ComboBox();
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
            this.lblTitulo.Text = "Nova solicitação";
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
            this.pictureIconeFormulario.Image = global::CASE.Properties.Resources.LucideAzulSolicitacoes;
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
            // lblTipo
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Location = new System.Drawing.Point(28, 72);
            this.lblTipo.Size = new System.Drawing.Size(550, 22);
            this.lblTipo.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblTipo.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblTipo.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblTipo.TabIndex = 5;
            this.lblTipo.Text = "Tipo";
            this.lblTipo.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbTipoSolicitacao
            this.cmbTipoSolicitacao.Name = "cmbTipoSolicitacao";
            this.cmbTipoSolicitacao.Location = new System.Drawing.Point(28, 94);
            this.cmbTipoSolicitacao.Size = new System.Drawing.Size(550, 29);
            this.cmbTipoSolicitacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbTipoSolicitacao.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbTipoSolicitacao.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbTipoSolicitacao.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbTipoSolicitacao.TabIndex = 6;
            this.cmbTipoSolicitacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoSolicitacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTipoSolicitacao.Items.AddRange(new object[] { "Reposição", "Ocorrência", "Ajuste de estoque" });
            this.cmbTipoSolicitacao.SelectedIndex = 0;
            // lblSetor
            this.lblSetor.Name = "lblSetor";
            this.lblSetor.Location = new System.Drawing.Point(28, 123);
            this.lblSetor.Size = new System.Drawing.Size(550, 22);
            this.lblSetor.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSetor.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblSetor.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblSetor.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSetor.TabIndex = 7;
            this.lblSetor.Text = "Setor";
            this.lblSetor.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbSetorSolicitacao
            this.cmbSetorSolicitacao.Name = "cmbSetorSolicitacao";
            this.cmbSetorSolicitacao.Location = new System.Drawing.Point(28, 145);
            this.cmbSetorSolicitacao.Size = new System.Drawing.Size(550, 29);
            this.cmbSetorSolicitacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbSetorSolicitacao.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbSetorSolicitacao.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbSetorSolicitacao.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbSetorSolicitacao.TabIndex = 8;
            this.cmbSetorSolicitacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetorSolicitacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSetorSolicitacao.Items.AddRange(new object[] { "Bebidas", "Alimentação", "Doces", "Brincadeiras" });
            this.cmbSetorSolicitacao.SelectedIndex = 0;
            // lblSolicitante
            this.lblSolicitante.Name = "lblSolicitante";
            this.lblSolicitante.Location = new System.Drawing.Point(28, 174);
            this.lblSolicitante.Size = new System.Drawing.Size(550, 22);
            this.lblSolicitante.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSolicitante.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblSolicitante.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblSolicitante.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSolicitante.TabIndex = 9;
            this.lblSolicitante.Text = "Solicitante";
            this.lblSolicitante.TextAlign = (System.Drawing.ContentAlignment)1;
            // txtSolicitante
            this.txtSolicitante.Name = "txtSolicitante";
            this.txtSolicitante.Location = new System.Drawing.Point(36, 200);
            this.txtSolicitante.Size = new System.Drawing.Size(530, 25);
            this.txtSolicitante.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.txtSolicitante.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.txtSolicitante.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.txtSolicitante.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.txtSolicitante.TabIndex = 10;
            this.txtSolicitante.Text = "";
            this.txtSolicitante.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSolicitante.Multiline = false;
            this.txtSolicitante.MaxLength = 32767;
            // lblProduto
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Location = new System.Drawing.Point(28, 225);
            this.lblProduto.Size = new System.Drawing.Size(550, 22);
            this.lblProduto.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblProduto.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblProduto.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblProduto.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblProduto.TabIndex = 11;
            this.lblProduto.Text = "Produto";
            this.lblProduto.TextAlign = (System.Drawing.ContentAlignment)1;
            // txtProdutoSolicitacao
            this.txtProdutoSolicitacao.Name = "txtProdutoSolicitacao";
            this.txtProdutoSolicitacao.Location = new System.Drawing.Point(36, 251);
            this.txtProdutoSolicitacao.Size = new System.Drawing.Size(530, 25);
            this.txtProdutoSolicitacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.txtProdutoSolicitacao.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.txtProdutoSolicitacao.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.txtProdutoSolicitacao.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.txtProdutoSolicitacao.TabIndex = 12;
            this.txtProdutoSolicitacao.Text = "";
            this.txtProdutoSolicitacao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProdutoSolicitacao.Multiline = false;
            this.txtProdutoSolicitacao.MaxLength = 32767;
            // lblQuantidade
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Location = new System.Drawing.Point(28, 276);
            this.lblQuantidade.Size = new System.Drawing.Size(550, 22);
            this.lblQuantidade.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblQuantidade.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblQuantidade.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblQuantidade.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblQuantidade.TabIndex = 13;
            this.lblQuantidade.Text = "Quantidade";
            this.lblQuantidade.TextAlign = (System.Drawing.ContentAlignment)1;
            // txtQuantidadeSolicitacao
            this.txtQuantidadeSolicitacao.Name = "txtQuantidadeSolicitacao";
            this.txtQuantidadeSolicitacao.Location = new System.Drawing.Point(36, 302);
            this.txtQuantidadeSolicitacao.Size = new System.Drawing.Size(530, 25);
            this.txtQuantidadeSolicitacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.txtQuantidadeSolicitacao.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.txtQuantidadeSolicitacao.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.txtQuantidadeSolicitacao.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.txtQuantidadeSolicitacao.TabIndex = 14;
            this.txtQuantidadeSolicitacao.Text = "";
            this.txtQuantidadeSolicitacao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuantidadeSolicitacao.Multiline = false;
            this.txtQuantidadeSolicitacao.MaxLength = 32767;
            // lblObservacao
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.Location = new System.Drawing.Point(28, 327);
            this.lblObservacao.Size = new System.Drawing.Size(550, 22);
            this.lblObservacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblObservacao.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblObservacao.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblObservacao.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblObservacao.TabIndex = 15;
            this.lblObservacao.Text = "Observação";
            this.lblObservacao.TextAlign = (System.Drawing.ContentAlignment)1;
            // txtObservacaoSolicitacao
            this.txtObservacaoSolicitacao.Name = "txtObservacaoSolicitacao";
            this.txtObservacaoSolicitacao.Location = new System.Drawing.Point(36, 353);
            this.txtObservacaoSolicitacao.Size = new System.Drawing.Size(530, 25);
            this.txtObservacaoSolicitacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.txtObservacaoSolicitacao.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.txtObservacaoSolicitacao.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.txtObservacaoSolicitacao.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.txtObservacaoSolicitacao.TabIndex = 16;
            this.txtObservacaoSolicitacao.Text = "";
            this.txtObservacaoSolicitacao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtObservacaoSolicitacao.Multiline = false;
            this.txtObservacaoSolicitacao.MaxLength = 32767;
            // lblSituacao
            this.lblSituacao.Name = "lblSituacao";
            this.lblSituacao.Location = new System.Drawing.Point(28, 378);
            this.lblSituacao.Size = new System.Drawing.Size(550, 22);
            this.lblSituacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.lblSituacao.Font = new System.Drawing.Font("Segoe UI", 9F, (System.Drawing.FontStyle)0);
            this.lblSituacao.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            this.lblSituacao.ForeColor = System.Drawing.Color.FromArgb(255, 115, 132, 151);
            this.lblSituacao.TabIndex = 17;
            this.lblSituacao.Text = "Situação";
            this.lblSituacao.TextAlign = (System.Drawing.ContentAlignment)1;
            // cmbStatusSolicitacao
            this.cmbStatusSolicitacao.Name = "cmbStatusSolicitacao";
            this.cmbStatusSolicitacao.Location = new System.Drawing.Point(28, 400);
            this.cmbStatusSolicitacao.Size = new System.Drawing.Size(550, 29);
            this.cmbStatusSolicitacao.Anchor = (System.Windows.Forms.AnchorStyles)5;
            this.cmbStatusSolicitacao.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.cmbStatusSolicitacao.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.cmbStatusSolicitacao.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.cmbStatusSolicitacao.TabIndex = 18;
            this.cmbStatusSolicitacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusSolicitacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatusSolicitacao.Items.AddRange(new object[] { "Pendente", "Em atendimento", "Concluída" });
            this.cmbStatusSolicitacao.SelectedIndex = 0;
            this.Controls.Add(this.panelFormulario);
            this.panelFormulario.Controls.Add(this.lblTitulo);
            this.panelFormulario.Controls.Add(this.pictureIconeFormulario);
            this.panelFormulario.Controls.Add(this.labelAvisoFormulario);
            this.panelFormulario.Controls.Add(this.btnCancelar);
            this.panelFormulario.Controls.Add(this.btnSalvarVisual);
            this.panelFormulario.Controls.Add(this.lblTipo);
            this.panelFormulario.Controls.Add(this.cmbTipoSolicitacao);
            this.panelFormulario.Controls.Add(this.lblSetor);
            this.panelFormulario.Controls.Add(this.cmbSetorSolicitacao);
            this.panelFormulario.Controls.Add(this.lblSolicitante);
            this.panelFormulario.Controls.Add(this.txtSolicitante);
            this.panelFormulario.Controls.Add(this.lblProduto);
            this.panelFormulario.Controls.Add(this.txtProdutoSolicitacao);
            this.panelFormulario.Controls.Add(this.lblQuantidade);
            this.panelFormulario.Controls.Add(this.txtQuantidadeSolicitacao);
            this.panelFormulario.Controls.Add(this.lblObservacao);
            this.panelFormulario.Controls.Add(this.txtObservacaoSolicitacao);
            this.panelFormulario.Controls.Add(this.lblSituacao);
            this.panelFormulario.Controls.Add(this.cmbStatusSolicitacao);
            this.Name = "SolicitacaoForm";
            this.Font = new System.Drawing.Font("Segoe UI", 10F, (System.Drawing.FontStyle)0);
            this.BackColor = System.Drawing.Color.FromArgb(255, 246, 248, 252);
            this.ForeColor = System.Drawing.Color.FromArgb(255, 30, 51, 75);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Text = "Nova solicitação";
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
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipoSolicitacao;
        private System.Windows.Forms.Label lblSetor;
        private System.Windows.Forms.ComboBox cmbSetorSolicitacao;
        private System.Windows.Forms.Label lblSolicitante;
        private System.Windows.Forms.TextBox txtSolicitante;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.TextBox txtProdutoSolicitacao;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.TextBox txtQuantidadeSolicitacao;
        private System.Windows.Forms.Label lblObservacao;
        private System.Windows.Forms.TextBox txtObservacaoSolicitacao;
        private System.Windows.Forms.Label lblSituacao;
        private System.Windows.Forms.ComboBox cmbStatusSolicitacao;
    }
}
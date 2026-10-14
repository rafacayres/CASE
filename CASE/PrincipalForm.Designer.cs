namespace CASE
{
    partial class PrincipalForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelCabecalho = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.panelAreaPrincipal = new System.Windows.Forms.Panel();
            this.panelRodapeMenu = new System.Windows.Forms.Panel();
            this.buttonSair = new System.Windows.Forms.Button();
            this.buttonConfiguracoes = new System.Windows.Forms.Button();
            this.buttonRelatorios = new System.Windows.Forms.Button();
            this.buttonMovimentacoes = new System.Windows.Forms.Button();
            this.buttonEstoque = new System.Windows.Forms.Button();
            this.buttonProdutos = new System.Windows.Forms.Button();
            this.buttonDashboard = new System.Windows.Forms.Button();
            this.pictureLogoMenu = new System.Windows.Forms.PictureBox();
            this.panelMenu.SuspendLayout();
            this.panelAreaPrincipal.SuspendLayout();
            this.panelRodapeMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogoMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCabecalho
            // 
            this.panelCabecalho.BackColor = System.Drawing.Color.White;
            this.panelCabecalho.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCabecalho.Location = new System.Drawing.Point(0, 0);
            this.panelCabecalho.Name = "panelCabecalho";
            this.panelCabecalho.Size = new System.Drawing.Size(954, 70);
            this.panelCabecalho.TabIndex = 0;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.panelMenu.Controls.Add(this.panelRodapeMenu);
            this.panelMenu.Controls.Add(this.buttonRelatorios);
            this.panelMenu.Controls.Add(this.buttonMovimentacoes);
            this.panelMenu.Controls.Add(this.buttonEstoque);
            this.panelMenu.Controls.Add(this.buttonProdutos);
            this.panelMenu.Controls.Add(this.buttonDashboard);
            this.panelMenu.Controls.Add(this.pictureLogoMenu);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(230, 711);
            this.panelMenu.TabIndex = 1;
            // 
            // panelConteudo
            // 
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 70);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Size = new System.Drawing.Size(954, 641);
            this.panelConteudo.TabIndex = 2;
            // 
            // panelAreaPrincipal
            // 
            this.panelAreaPrincipal.Controls.Add(this.panelConteudo);
            this.panelAreaPrincipal.Controls.Add(this.panelCabecalho);
            this.panelAreaPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAreaPrincipal.Location = new System.Drawing.Point(230, 0);
            this.panelAreaPrincipal.Name = "panelAreaPrincipal";
            this.panelAreaPrincipal.Size = new System.Drawing.Size(954, 711);
            this.panelAreaPrincipal.TabIndex = 0;
            // 
            // panelRodapeMenu
            // 
            this.panelRodapeMenu.Controls.Add(this.buttonSair);
            this.panelRodapeMenu.Controls.Add(this.buttonConfiguracoes);
            this.panelRodapeMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRodapeMenu.Location = new System.Drawing.Point(0, 596);
            this.panelRodapeMenu.Name = "panelRodapeMenu";
            this.panelRodapeMenu.Size = new System.Drawing.Size(230, 115);
            this.panelRodapeMenu.TabIndex = 7;
            // 
            // buttonSair
            // 
            this.buttonSair.FlatAppearance.BorderSize = 0;
            this.buttonSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSair.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonSair.ForeColor = System.Drawing.Color.White;
            this.buttonSair.Image = global::CASE.Properties.Resources.log_out;
            this.buttonSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSair.Location = new System.Drawing.Point(12, 60);
            this.buttonSair.Name = "buttonSair";
            this.buttonSair.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.buttonSair.Size = new System.Drawing.Size(206, 42);
            this.buttonSair.TabIndex = 1;
            this.buttonSair.Text = "Sair";
            this.buttonSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSair.UseVisualStyleBackColor = true;
            // 
            // buttonConfiguracoes
            // 
            this.buttonConfiguracoes.FlatAppearance.BorderSize = 0;
            this.buttonConfiguracoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConfiguracoes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonConfiguracoes.ForeColor = System.Drawing.Color.White;
            this.buttonConfiguracoes.Image = global::CASE.Properties.Resources.settings;
            this.buttonConfiguracoes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonConfiguracoes.Location = new System.Drawing.Point(12, 10);
            this.buttonConfiguracoes.Name = "buttonConfiguracoes";
            this.buttonConfiguracoes.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.buttonConfiguracoes.Size = new System.Drawing.Size(206, 42);
            this.buttonConfiguracoes.TabIndex = 0;
            this.buttonConfiguracoes.Text = "Configurações";
            this.buttonConfiguracoes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonConfiguracoes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonConfiguracoes.UseVisualStyleBackColor = true;
            // 
            // buttonRelatorios
            // 
            this.buttonRelatorios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.buttonRelatorios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRelatorios.FlatAppearance.BorderSize = 0;
            this.buttonRelatorios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRelatorios.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonRelatorios.ForeColor = System.Drawing.Color.White;
            this.buttonRelatorios.Image = global::CASE.Properties.Resources.chart_no_axes_column_increasing;
            this.buttonRelatorios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRelatorios.Location = new System.Drawing.Point(12, 315);
            this.buttonRelatorios.Name = "buttonRelatorios";
            this.buttonRelatorios.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.buttonRelatorios.Size = new System.Drawing.Size(206, 42);
            this.buttonRelatorios.TabIndex = 6;
            this.buttonRelatorios.Text = "Relatórios";
            this.buttonRelatorios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRelatorios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRelatorios.UseVisualStyleBackColor = false;
            // 
            // buttonMovimentacoes
            // 
            this.buttonMovimentacoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.buttonMovimentacoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMovimentacoes.FlatAppearance.BorderSize = 0;
            this.buttonMovimentacoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMovimentacoes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonMovimentacoes.ForeColor = System.Drawing.Color.White;
            this.buttonMovimentacoes.Image = global::CASE.Properties.Resources.arrow_left_right;
            this.buttonMovimentacoes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMovimentacoes.Location = new System.Drawing.Point(12, 265);
            this.buttonMovimentacoes.Name = "buttonMovimentacoes";
            this.buttonMovimentacoes.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.buttonMovimentacoes.Size = new System.Drawing.Size(206, 42);
            this.buttonMovimentacoes.TabIndex = 5;
            this.buttonMovimentacoes.Text = "Movimentações";
            this.buttonMovimentacoes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMovimentacoes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonMovimentacoes.UseVisualStyleBackColor = false;
            // 
            // buttonEstoque
            // 
            this.buttonEstoque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.buttonEstoque.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEstoque.FlatAppearance.BorderSize = 0;
            this.buttonEstoque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEstoque.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonEstoque.ForeColor = System.Drawing.Color.White;
            this.buttonEstoque.Image = global::CASE.Properties.Resources.warehouse;
            this.buttonEstoque.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEstoque.Location = new System.Drawing.Point(12, 215);
            this.buttonEstoque.Name = "buttonEstoque";
            this.buttonEstoque.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.buttonEstoque.Size = new System.Drawing.Size(206, 42);
            this.buttonEstoque.TabIndex = 4;
            this.buttonEstoque.Text = "Estoque";
            this.buttonEstoque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEstoque.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonEstoque.UseVisualStyleBackColor = false;
            // 
            // buttonProdutos
            // 
            this.buttonProdutos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.buttonProdutos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonProdutos.FlatAppearance.BorderSize = 0;
            this.buttonProdutos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProdutos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonProdutos.ForeColor = System.Drawing.Color.White;
            this.buttonProdutos.Image = global::CASE.Properties.Resources.package;
            this.buttonProdutos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonProdutos.Location = new System.Drawing.Point(12, 165);
            this.buttonProdutos.Name = "buttonProdutos";
            this.buttonProdutos.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.buttonProdutos.Size = new System.Drawing.Size(206, 42);
            this.buttonProdutos.TabIndex = 2;
            this.buttonProdutos.Text = "Produtos";
            this.buttonProdutos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonProdutos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonProdutos.UseVisualStyleBackColor = false;
            // 
            // buttonDashboard
            // 
            this.buttonDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(94)))), ((int)(((byte)(166)))));
            this.buttonDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDashboard.FlatAppearance.BorderSize = 0;
            this.buttonDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDashboard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonDashboard.ForeColor = System.Drawing.Color.White;
            this.buttonDashboard.Image = global::CASE.Properties.Resources.layout_dashboard_1_;
            this.buttonDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDashboard.Location = new System.Drawing.Point(12, 115);
            this.buttonDashboard.Name = "buttonDashboard";
            this.buttonDashboard.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.buttonDashboard.Size = new System.Drawing.Size(206, 42);
            this.buttonDashboard.TabIndex = 1;
            this.buttonDashboard.Text = "Dashboard";
            this.buttonDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonDashboard.UseVisualStyleBackColor = false;
            // 
            // pictureLogoMenu
            // 
            this.pictureLogoMenu.Image = global::CASE.Properties.Resources.Sem_título3;
            this.pictureLogoMenu.Location = new System.Drawing.Point(15, 15);
            this.pictureLogoMenu.Name = "pictureLogoMenu";
            this.pictureLogoMenu.Size = new System.Drawing.Size(200, 75);
            this.pictureLogoMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureLogoMenu.TabIndex = 0;
            this.pictureLogoMenu.TabStop = false;
            // 
            // PrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1184, 711);
            this.Controls.Add(this.panelAreaPrincipal);
            this.Controls.Add(this.panelMenu);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "PrincipalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CASE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelMenu.ResumeLayout(false);
            this.panelAreaPrincipal.ResumeLayout(false);
            this.panelRodapeMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogoMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCabecalho;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelConteudo;
        private System.Windows.Forms.Panel panelAreaPrincipal;
        private System.Windows.Forms.PictureBox pictureLogoMenu;
        private System.Windows.Forms.Button buttonDashboard;
        private System.Windows.Forms.Button buttonRelatorios;
        private System.Windows.Forms.Button buttonMovimentacoes;
        private System.Windows.Forms.Button buttonEstoque;
        private System.Windows.Forms.Button buttonProdutos;
        private System.Windows.Forms.Panel panelRodapeMenu;
        private System.Windows.Forms.Button buttonConfiguracoes;
        private System.Windows.Forms.Button buttonSair;
    }
}


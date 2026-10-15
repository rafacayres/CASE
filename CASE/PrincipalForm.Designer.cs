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
            this.panelPerfil = new System.Windows.Forms.Panel();
            this.pictureNotificacoes = new System.Windows.Forms.PictureBox();
            this.labelTipoUsuario = new System.Windows.Forms.Label();
            this.labelNomeUsuario = new System.Windows.Forms.Label();
            this.pictureUsuario = new System.Windows.Forms.PictureBox();
            this.panelPesquisa = new System.Windows.Forms.Panel();
            this.picturePesquisa = new System.Windows.Forms.PictureBox();
            this.textPesquisa = new System.Windows.Forms.TextBox();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelRodapeMenu = new System.Windows.Forms.Panel();
            this.buttonSair = new System.Windows.Forms.Button();
            this.buttonConfiguracoes = new System.Windows.Forms.Button();
            this.buttonRelatorios = new System.Windows.Forms.Button();
            this.buttonMovimentacoes = new System.Windows.Forms.Button();
            this.buttonEstoque = new System.Windows.Forms.Button();
            this.buttonProdutos = new System.Windows.Forms.Button();
            this.buttonDashboard = new System.Windows.Forms.Button();
            this.pictureLogoMenu = new System.Windows.Forms.PictureBox();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.panelTotalProdutos = new System.Windows.Forms.Panel();
            this.panelIconeProdutos = new System.Windows.Forms.Panel();
            this.picIconeProdutos = new System.Windows.Forms.PictureBox();
            this.lblTotalProdutosValor = new System.Windows.Forms.Label();
            this.lblTotalProdutosTitulo = new System.Windows.Forms.Label();
            this.lblSubtituloDashboard = new System.Windows.Forms.Label();
            this.lblTituloDashboard = new System.Windows.Forms.Label();
            this.dashboardControl1 = new CASE.DashboardControl();
            this.panelAreaPrincipal = new System.Windows.Forms.Panel();
            this.panelCabecalho.SuspendLayout();
            this.panelPerfil.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNotificacoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuario)).BeginInit();
            this.panelPesquisa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturePesquisa)).BeginInit();
            this.panelMenu.SuspendLayout();
            this.panelRodapeMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogoMenu)).BeginInit();
            this.panelConteudo.SuspendLayout();
            this.panelTotalProdutos.SuspendLayout();
            this.panelIconeProdutos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIconeProdutos)).BeginInit();
            this.panelAreaPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCabecalho
            // 
            this.panelCabecalho.BackColor = System.Drawing.Color.White;
            this.panelCabecalho.Controls.Add(this.panelPerfil);
            this.panelCabecalho.Controls.Add(this.panelPesquisa);
            this.panelCabecalho.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCabecalho.Location = new System.Drawing.Point(0, 0);
            this.panelCabecalho.Name = "panelCabecalho";
            this.panelCabecalho.Size = new System.Drawing.Size(954, 70);
            this.panelCabecalho.TabIndex = 0;
            // 
            // panelPerfil
            // 
            this.panelPerfil.Controls.Add(this.pictureNotificacoes);
            this.panelPerfil.Controls.Add(this.labelTipoUsuario);
            this.panelPerfil.Controls.Add(this.labelNomeUsuario);
            this.panelPerfil.Controls.Add(this.pictureUsuario);
            this.panelPerfil.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelPerfil.Location = new System.Drawing.Point(724, 0);
            this.panelPerfil.Name = "panelPerfil";
            this.panelPerfil.Size = new System.Drawing.Size(230, 70);
            this.panelPerfil.TabIndex = 1;
            // 
            // pictureNotificacoes
            // 
            this.pictureNotificacoes.Image = global::CASE.Properties.Resources.bell_1_;
            this.pictureNotificacoes.Location = new System.Drawing.Point(35, 23);
            this.pictureNotificacoes.Name = "pictureNotificacoes";
            this.pictureNotificacoes.Size = new System.Drawing.Size(24, 24);
            this.pictureNotificacoes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureNotificacoes.TabIndex = 3;
            this.pictureNotificacoes.TabStop = false;
            // 
            // labelTipoUsuario
            // 
            this.labelTipoUsuario.AutoSize = true;
            this.labelTipoUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTipoUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelTipoUsuario.Location = new System.Drawing.Point(126, 36);
            this.labelTipoUsuario.Name = "labelTipoUsuario";
            this.labelTipoUsuario.Size = new System.Drawing.Size(49, 15);
            this.labelTipoUsuario.TabIndex = 2;
            this.labelTipoUsuario.Text = "Usuário";
            // 
            // labelNomeUsuario
            // 
            this.labelNomeUsuario.AutoSize = true;
            this.labelNomeUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelNomeUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.labelNomeUsuario.Location = new System.Drawing.Point(126, 17);
            this.labelNomeUsuario.Name = "labelNomeUsuario";
            this.labelNomeUsuario.Size = new System.Drawing.Size(83, 15);
            this.labelNomeUsuario.TabIndex = 1;
            this.labelNomeUsuario.Text = "Administrador";
            // 
            // pictureUsuario
            // 
            this.pictureUsuario.Image = global::CASE.Properties.Resources.Sem_título4;
            this.pictureUsuario.Location = new System.Drawing.Point(82, 17);
            this.pictureUsuario.Name = "pictureUsuario";
            this.pictureUsuario.Size = new System.Drawing.Size(36, 36);
            this.pictureUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureUsuario.TabIndex = 0;
            this.pictureUsuario.TabStop = false;
            // 
            // panelPesquisa
            // 
            this.panelPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelPesquisa.Controls.Add(this.picturePesquisa);
            this.panelPesquisa.Controls.Add(this.textPesquisa);
            this.panelPesquisa.Location = new System.Drawing.Point(20, 15);
            this.panelPesquisa.Name = "panelPesquisa";
            this.panelPesquisa.Size = new System.Drawing.Size(260, 40);
            this.panelPesquisa.TabIndex = 0;
            // 
            // picturePesquisa
            // 
            this.picturePesquisa.Image = global::CASE.Properties.Resources.search;
            this.picturePesquisa.Location = new System.Drawing.Point(12, 10);
            this.picturePesquisa.Name = "picturePesquisa";
            this.picturePesquisa.Size = new System.Drawing.Size(20, 20);
            this.picturePesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picturePesquisa.TabIndex = 1;
            this.picturePesquisa.TabStop = false;
            // 
            // textPesquisa
            // 
            this.textPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.textPesquisa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textPesquisa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textPesquisa.Location = new System.Drawing.Point(40, 10);
            this.textPesquisa.Name = "textPesquisa";
            this.textPesquisa.Size = new System.Drawing.Size(205, 18);
            this.textPesquisa.TabIndex = 0;
            this.textPesquisa.Enter += new System.EventHandler(this.textPesquisa_Enter);
            this.textPesquisa.Leave += new System.EventHandler(this.textPesquisa_Leave);
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
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.panelTotalProdutos);
            this.panelConteudo.Controls.Add(this.lblSubtituloDashboard);
            this.panelConteudo.Controls.Add(this.lblTituloDashboard);
            this.panelConteudo.Controls.Add(this.dashboardControl1);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 70);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Size = new System.Drawing.Size(954, 641);
            this.panelConteudo.TabIndex = 2;
            // 
            // panelTotalProdutos
            // 
            this.panelTotalProdutos.BackColor = System.Drawing.Color.White;
            this.panelTotalProdutos.Controls.Add(this.panelIconeProdutos);
            this.panelTotalProdutos.Controls.Add(this.lblTotalProdutosValor);
            this.panelTotalProdutos.Controls.Add(this.lblTotalProdutosTitulo);
            this.panelTotalProdutos.Location = new System.Drawing.Point(30, 115);
            this.panelTotalProdutos.Name = "panelTotalProdutos";
            this.panelTotalProdutos.Size = new System.Drawing.Size(230, 120);
            this.panelTotalProdutos.TabIndex = 3;
            // 
            // panelIconeProdutos
            // 
            this.panelIconeProdutos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
            this.panelIconeProdutos.Controls.Add(this.picIconeProdutos);
            this.panelIconeProdutos.Location = new System.Drawing.Point(177, 12);
            this.panelIconeProdutos.Name = "panelIconeProdutos";
            this.panelIconeProdutos.Size = new System.Drawing.Size(40, 40);
            this.panelIconeProdutos.TabIndex = 2;
            // 
            // picIconeProdutos
            // 
            this.picIconeProdutos.Image = global::CASE.Properties.Resources.package_2_;
            this.picIconeProdutos.Location = new System.Drawing.Point(8, 8);
            this.picIconeProdutos.Name = "picIconeProdutos";
            this.picIconeProdutos.Size = new System.Drawing.Size(24, 24);
            this.picIconeProdutos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIconeProdutos.TabIndex = 0;
            this.picIconeProdutos.TabStop = false;
            // 
            // lblTotalProdutosValor
            // 
            this.lblTotalProdutosValor.AutoSize = true;
            this.lblTotalProdutosValor.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProdutosValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblTotalProdutosValor.Location = new System.Drawing.Point(15, 52);
            this.lblTotalProdutosValor.Name = "lblTotalProdutosValor";
            this.lblTotalProdutosValor.Size = new System.Drawing.Size(74, 45);
            this.lblTotalProdutosValor.TabIndex = 1;
            this.lblTotalProdutosValor.Text = "128";
            // 
            // lblTotalProdutosTitulo
            // 
            this.lblTotalProdutosTitulo.AutoSize = true;
            this.lblTotalProdutosTitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalProdutosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTotalProdutosTitulo.Location = new System.Drawing.Point(15, 18);
            this.lblTotalProdutosTitulo.Name = "lblTotalProdutosTitulo";
            this.lblTotalProdutosTitulo.Size = new System.Drawing.Size(117, 19);
            this.lblTotalProdutosTitulo.TabIndex = 0;
            this.lblTotalProdutosTitulo.Text = "Total de produtos";
            // 
            // lblSubtituloDashboard
            // 
            this.lblSubtituloDashboard.AutoSize = true;
            this.lblSubtituloDashboard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtituloDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtituloDashboard.Location = new System.Drawing.Point(32, 65);
            this.lblSubtituloDashboard.Name = "lblSubtituloDashboard";
            this.lblSubtituloDashboard.Size = new System.Drawing.Size(174, 19);
            this.lblSubtituloDashboard.TabIndex = 2;
            this.lblSubtituloDashboard.Text = "Visão geral do seu estoque";
            // 
            // lblTituloDashboard
            // 
            this.lblTituloDashboard.AutoSize = true;
            this.lblTituloDashboard.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblTituloDashboard.Location = new System.Drawing.Point(30, 25);
            this.lblTituloDashboard.Name = "lblTituloDashboard";
            this.lblTituloDashboard.Size = new System.Drawing.Size(138, 32);
            this.lblTituloDashboard.TabIndex = 1;
            this.lblTituloDashboard.Text = "Dashboard";
            // 
            // dashboardControl1
            // 
            this.dashboardControl1.AutoScroll = true;
            this.dashboardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardControl1.Location = new System.Drawing.Point(0, 0);
            this.dashboardControl1.Name = "dashboardControl1";
            this.dashboardControl1.Size = new System.Drawing.Size(954, 641);
            this.dashboardControl1.TabIndex = 0;
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
            this.Shown += new System.EventHandler(this.PrincipalForm_Shown);
            this.panelCabecalho.ResumeLayout(false);
            this.panelPerfil.ResumeLayout(false);
            this.panelPerfil.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNotificacoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuario)).EndInit();
            this.panelPesquisa.ResumeLayout(false);
            this.panelPesquisa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturePesquisa)).EndInit();
            this.panelMenu.ResumeLayout(false);
            this.panelRodapeMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogoMenu)).EndInit();
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            this.panelTotalProdutos.ResumeLayout(false);
            this.panelTotalProdutos.PerformLayout();
            this.panelIconeProdutos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIconeProdutos)).EndInit();
            this.panelAreaPrincipal.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panelPesquisa;
        private System.Windows.Forms.TextBox textPesquisa;
        private System.Windows.Forms.PictureBox picturePesquisa;
        private System.Windows.Forms.Panel panelPerfil;
        private System.Windows.Forms.PictureBox pictureUsuario;
        private System.Windows.Forms.Label labelTipoUsuario;
        private System.Windows.Forms.Label labelNomeUsuario;
        private System.Windows.Forms.PictureBox pictureNotificacoes;
        private DashboardControl dashboardControl1;
        private System.Windows.Forms.Label lblTituloDashboard;
        private System.Windows.Forms.Label lblSubtituloDashboard;
        private System.Windows.Forms.Panel panelTotalProdutos;
        private System.Windows.Forms.Label lblTotalProdutosValor;
        private System.Windows.Forms.Label lblTotalProdutosTitulo;
        private System.Windows.Forms.Panel panelIconeProdutos;
        private System.Windows.Forms.PictureBox picIconeProdutos;
    }
}


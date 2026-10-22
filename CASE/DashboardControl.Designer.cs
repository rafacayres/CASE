namespace CASE
{
    partial class DashboardControl
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

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureIconeTela = new System.Windows.Forms.PictureBox();
            this.panelEntradasMes = new System.Windows.Forms.Panel();
            this.panelIconeEntradas = new System.Windows.Forms.Panel();
            this.lblEntradasMesTitulo = new System.Windows.Forms.Label();
            this.lblEntradasMesValor = new System.Windows.Forms.Label();
            this.panelSaidasMes = new System.Windows.Forms.Panel();
            this.panelIconeSaidas = new System.Windows.Forms.Panel();
            this.lblSaidasMesTitulo = new System.Windows.Forms.Label();
            this.lblSaidasMesValor = new System.Windows.Forms.Label();
            this.panelGraficoMovimentacoes = new System.Windows.Forms.Panel();
            this.lblTituloGrafico = new System.Windows.Forms.Label();
            this.panelMovimentacoesRecentes = new System.Windows.Forms.Panel();
            this.lblTituloMovimentacoes = new System.Windows.Forms.Label();
            this.lblTituloDashboard = new System.Windows.Forms.Label();
            this.lblSubtituloDashboard = new System.Windows.Forms.Label();
            this.panelTotalProdutos = new System.Windows.Forms.Panel();
            this.panelIconeProdutos = new System.Windows.Forms.Panel();
            this.lblTotalProdutosTitulo = new System.Windows.Forms.Label();
            this.lblTotalProdutosValor = new System.Windows.Forms.Label();
            this.panelEstoqueBaixo = new System.Windows.Forms.Panel();
            this.panelIconeEstoqueBaixo = new System.Windows.Forms.Panel();
            this.lblEstoqueBaixoTitulo = new System.Windows.Forms.Label();
            this.lblEstoqueBaixoValor = new System.Windows.Forms.Label();
            this.picIconeEntradas = new System.Windows.Forms.PictureBox();
            this.picIconeSaidas = new System.Windows.Forms.PictureBox();
            this.picIconeProdutos = new System.Windows.Forms.PictureBox();
            this.picIconeEstoqueBaixo = new System.Windows.Forms.PictureBox();
            this.panelEntradasMes.SuspendLayout();
            this.panelIconeEntradas.SuspendLayout();
            this.panelSaidasMes.SuspendLayout();
            this.panelIconeSaidas.SuspendLayout();
            this.panelGraficoMovimentacoes.SuspendLayout();
            this.panelMovimentacoesRecentes.SuspendLayout();
            this.panelTotalProdutos.SuspendLayout();
            this.panelIconeProdutos.SuspendLayout();
            this.panelEstoqueBaixo.SuspendLayout();
            this.panelIconeEstoqueBaixo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIconeEntradas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIconeSaidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIconeProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIconeEstoqueBaixo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconeTela)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEntradasMes
            // 
            this.panelEntradasMes.BackColor = System.Drawing.Color.White;
            this.panelEntradasMes.Controls.Add(this.panelIconeEntradas);
            this.panelEntradasMes.Controls.Add(this.lblEntradasMesTitulo);
            this.panelEntradasMes.Controls.Add(this.lblEntradasMesValor);
            this.panelEntradasMes.Location = new System.Drawing.Point(520, 115);
            this.panelEntradasMes.Name = "panelEntradasMes";
            this.panelEntradasMes.Size = new System.Drawing.Size(230, 120);
            this.panelEntradasMes.TabIndex = 0;
            // 
            // panelIconeEntradas
            // 
            this.panelIconeEntradas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
            this.panelIconeEntradas.Controls.Add(this.picIconeEntradas);
            this.panelIconeEntradas.Location = new System.Drawing.Point(177, 12);
            this.panelIconeEntradas.Name = "panelIconeEntradas";
            this.panelIconeEntradas.Size = new System.Drawing.Size(40, 40);
            this.panelIconeEntradas.TabIndex = 2;
            // 
            // lblEntradasMesTitulo
            // 
            this.lblEntradasMesTitulo.AutoSize = true;
            this.lblEntradasMesTitulo.BackColor = System.Drawing.Color.White;
            this.lblEntradasMesTitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEntradasMesTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblEntradasMesTitulo.Location = new System.Drawing.Point(15, 18);
            this.lblEntradasMesTitulo.Name = "lblEntradasMesTitulo";
            this.lblEntradasMesTitulo.Size = new System.Drawing.Size(111, 19);
            this.lblEntradasMesTitulo.TabIndex = 0;
            this.lblEntradasMesTitulo.Text = "Entradas no mês";
            // 
            // lblEntradasMesValor
            // 
            this.lblEntradasMesValor.AutoSize = true;
            this.lblEntradasMesValor.BackColor = System.Drawing.Color.White;
            this.lblEntradasMesValor.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblEntradasMesValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblEntradasMesValor.Location = new System.Drawing.Point(15, 52);
            this.lblEntradasMesValor.Name = "lblEntradasMesValor";
            this.lblEntradasMesValor.Size = new System.Drawing.Size(56, 45);
            this.lblEntradasMesValor.TabIndex = 1;
            this.lblEntradasMesValor.Text = "32";
            // 
            // panelSaidasMes
            // 
            this.panelSaidasMes.BackColor = System.Drawing.Color.White;
            this.panelSaidasMes.Controls.Add(this.panelIconeSaidas);
            this.panelSaidasMes.Controls.Add(this.lblSaidasMesTitulo);
            this.panelSaidasMes.Controls.Add(this.lblSaidasMesValor);
            this.panelSaidasMes.Location = new System.Drawing.Point(765, 115);
            this.panelSaidasMes.Name = "panelSaidasMes";
            this.panelSaidasMes.Size = new System.Drawing.Size(230, 120);
            this.panelSaidasMes.TabIndex = 1;
            // 
            // panelIconeSaidas
            // 
            this.panelIconeSaidas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(241)))), ((int)(((byte)(242)))));
            this.panelIconeSaidas.Controls.Add(this.picIconeSaidas);
            this.panelIconeSaidas.Location = new System.Drawing.Point(177, 12);
            this.panelIconeSaidas.Name = "panelIconeSaidas";
            this.panelIconeSaidas.Size = new System.Drawing.Size(40, 40);
            this.panelIconeSaidas.TabIndex = 2;
            // 
            // lblSaidasMesTitulo
            // 
            this.lblSaidasMesTitulo.AutoSize = true;
            this.lblSaidasMesTitulo.BackColor = System.Drawing.Color.White;
            this.lblSaidasMesTitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSaidasMesTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSaidasMesTitulo.Location = new System.Drawing.Point(15, 18);
            this.lblSaidasMesTitulo.Name = "lblSaidasMesTitulo";
            this.lblSaidasMesTitulo.Size = new System.Drawing.Size(96, 19);
            this.lblSaidasMesTitulo.TabIndex = 0;
            this.lblSaidasMesTitulo.Text = "Saídas no mês";
            // 
            // lblSaidasMesValor
            // 
            this.lblSaidasMesValor.AutoSize = true;
            this.lblSaidasMesValor.BackColor = System.Drawing.Color.White;
            this.lblSaidasMesValor.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblSaidasMesValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblSaidasMesValor.Location = new System.Drawing.Point(15, 52);
            this.lblSaidasMesValor.Name = "lblSaidasMesValor";
            this.lblSaidasMesValor.Size = new System.Drawing.Size(56, 45);
            this.lblSaidasMesValor.TabIndex = 1;
            this.lblSaidasMesValor.Text = "18";
            // 
            // panelGraficoMovimentacoes
            // 
            this.panelGraficoMovimentacoes.BackColor = System.Drawing.Color.White;
            this.panelGraficoMovimentacoes.Controls.Add(this.lblTituloGrafico);
            this.panelGraficoMovimentacoes.Location = new System.Drawing.Point(30, 265);
            this.panelGraficoMovimentacoes.Name = "panelGraficoMovimentacoes";
            this.panelGraficoMovimentacoes.Size = new System.Drawing.Size(650, 300);
            this.panelGraficoMovimentacoes.TabIndex = 2;
            // 
            // lblTituloGrafico
            // 
            this.lblTituloGrafico.AutoSize = true;
            this.lblTituloGrafico.BackColor = System.Drawing.Color.White;
            this.lblTituloGrafico.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloGrafico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblTituloGrafico.Location = new System.Drawing.Point(20, 18);
            this.lblTituloGrafico.Name = "lblTituloGrafico";
            this.lblTituloGrafico.Size = new System.Drawing.Size(221, 21);
            this.lblTituloGrafico.TabIndex = 0;
            this.lblTituloGrafico.Text = "Movimentações do estoque";
            // 
            // panelMovimentacoesRecentes
            // 
            this.panelMovimentacoesRecentes.BackColor = System.Drawing.Color.White;
            this.panelMovimentacoesRecentes.Controls.Add(this.lblTituloMovimentacoes);
            this.panelMovimentacoesRecentes.Location = new System.Drawing.Point(695, 265);
            this.panelMovimentacoesRecentes.Name = "panelMovimentacoesRecentes";
            this.panelMovimentacoesRecentes.Size = new System.Drawing.Size(300, 300);
            this.panelMovimentacoesRecentes.TabIndex = 3;
            // 
            // lblTituloMovimentacoes
            // 
            this.lblTituloMovimentacoes.AutoSize = true;
            this.lblTituloMovimentacoes.BackColor = System.Drawing.Color.White;
            this.lblTituloMovimentacoes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloMovimentacoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblTituloMovimentacoes.Location = new System.Drawing.Point(20, 18);
            this.lblTituloMovimentacoes.Name = "lblTituloMovimentacoes";
            this.lblTituloMovimentacoes.Size = new System.Drawing.Size(200, 21);
            this.lblTituloMovimentacoes.TabIndex = 0;
            this.lblTituloMovimentacoes.Text = "Movimentações recentes";
            // 
            // lblTituloDashboard
            // 
            this.lblTituloDashboard.AutoSize = true;
            this.lblTituloDashboard.BackColor = System.Drawing.Color.White;
            this.lblTituloDashboard.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTituloDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblTituloDashboard.Location = new System.Drawing.Point(80, 27);
            this.lblTituloDashboard.Name = "lblTituloDashboard";
            this.lblTituloDashboard.Size = new System.Drawing.Size(109, 25);
            this.lblTituloDashboard.TabIndex = 0;
            this.lblTituloDashboard.Text = "Dashboard";
            // 
            // lblSubtituloDashboard
            // 
            this.lblSubtituloDashboard.AutoSize = true;
            this.lblSubtituloDashboard.BackColor = System.Drawing.Color.White;
            this.lblSubtituloDashboard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtituloDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtituloDashboard.Location = new System.Drawing.Point(82, 62);
            this.lblSubtituloDashboard.Name = "lblSubtituloDashboard";
            this.lblSubtituloDashboard.Size = new System.Drawing.Size(92, 19);
            this.lblSubtituloDashboard.TabIndex = 1;
            this.lblSubtituloDashboard.Text = "Resumo geral";
            // 
            // panelTotalProdutos
            // 
            this.panelTotalProdutos.BackColor = System.Drawing.Color.White;
            this.panelTotalProdutos.Controls.Add(this.panelIconeProdutos);
            this.panelTotalProdutos.Controls.Add(this.lblTotalProdutosTitulo);
            this.panelTotalProdutos.Controls.Add(this.lblTotalProdutosValor);
            this.panelTotalProdutos.Location = new System.Drawing.Point(30, 115);
            this.panelTotalProdutos.Name = "panelTotalProdutos";
            this.panelTotalProdutos.Size = new System.Drawing.Size(230, 120);
            this.panelTotalProdutos.TabIndex = 2;
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
            // lblTotalProdutosTitulo
            // 
            this.lblTotalProdutosTitulo.AutoSize = true;
            this.lblTotalProdutosTitulo.BackColor = System.Drawing.Color.White;
            this.lblTotalProdutosTitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalProdutosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTotalProdutosTitulo.Location = new System.Drawing.Point(15, 18);
            this.lblTotalProdutosTitulo.Name = "lblTotalProdutosTitulo";
            this.lblTotalProdutosTitulo.Size = new System.Drawing.Size(117, 19);
            this.lblTotalProdutosTitulo.TabIndex = 0;
            this.lblTotalProdutosTitulo.Text = "Total de produtos";
            // 
            // lblTotalProdutosValor
            // 
            this.lblTotalProdutosValor.AutoSize = true;
            this.lblTotalProdutosValor.BackColor = System.Drawing.Color.White;
            this.lblTotalProdutosValor.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalProdutosValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblTotalProdutosValor.Location = new System.Drawing.Point(15, 52);
            this.lblTotalProdutosValor.Name = "lblTotalProdutosValor";
            this.lblTotalProdutosValor.Size = new System.Drawing.Size(74, 45);
            this.lblTotalProdutosValor.TabIndex = 1;
            this.lblTotalProdutosValor.Text = "150";
            // 
            // panelEstoqueBaixo
            // 
            this.panelEstoqueBaixo.BackColor = System.Drawing.Color.White;
            this.panelEstoqueBaixo.Controls.Add(this.panelIconeEstoqueBaixo);
            this.panelEstoqueBaixo.Controls.Add(this.lblEstoqueBaixoTitulo);
            this.panelEstoqueBaixo.Controls.Add(this.lblEstoqueBaixoValor);
            this.panelEstoqueBaixo.Location = new System.Drawing.Point(276, 115);
            this.panelEstoqueBaixo.Name = "panelEstoqueBaixo";
            this.panelEstoqueBaixo.Size = new System.Drawing.Size(230, 120);
            this.panelEstoqueBaixo.TabIndex = 2;
            // 
            // panelIconeEstoqueBaixo
            // 
            this.panelIconeEstoqueBaixo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(241)))), ((int)(((byte)(242)))));
            this.panelIconeEstoqueBaixo.Controls.Add(this.picIconeEstoqueBaixo);
            this.panelIconeEstoqueBaixo.Location = new System.Drawing.Point(177, 12);
            this.panelIconeEstoqueBaixo.Name = "panelIconeEstoqueBaixo";
            this.panelIconeEstoqueBaixo.Size = new System.Drawing.Size(40, 40);
            this.panelIconeEstoqueBaixo.TabIndex = 2;
            // 
            // lblEstoqueBaixoTitulo
            // 
            this.lblEstoqueBaixoTitulo.AutoSize = true;
            this.lblEstoqueBaixoTitulo.BackColor = System.Drawing.Color.White;
            this.lblEstoqueBaixoTitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEstoqueBaixoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblEstoqueBaixoTitulo.Location = new System.Drawing.Point(15, 18);
            this.lblEstoqueBaixoTitulo.Name = "lblEstoqueBaixoTitulo";
            this.lblEstoqueBaixoTitulo.Size = new System.Drawing.Size(106, 19);
            this.lblEstoqueBaixoTitulo.TabIndex = 0;
            this.lblEstoqueBaixoTitulo.Text = "Estoques baixos";
            // 
            // lblEstoqueBaixoValor
            // 
            this.lblEstoqueBaixoValor.AutoSize = true;
            this.lblEstoqueBaixoValor.BackColor = System.Drawing.Color.White;
            this.lblEstoqueBaixoValor.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblEstoqueBaixoValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(80)))));
            this.lblEstoqueBaixoValor.Location = new System.Drawing.Point(15, 52);
            this.lblEstoqueBaixoValor.Name = "lblEstoqueBaixoValor";
            this.lblEstoqueBaixoValor.Size = new System.Drawing.Size(38, 45);
            this.lblEstoqueBaixoValor.TabIndex = 1;
            this.lblEstoqueBaixoValor.Text = "5";
            // 
            // picIconeEntradas
            // 
            this.picIconeEntradas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
            this.picIconeEntradas.Image = global::CASE.Properties.Resources.LucideAzulEntradas;
            this.picIconeEntradas.Location = new System.Drawing.Point(8, 8);
            this.picIconeEntradas.Name = "picIconeEntradas";
            this.picIconeEntradas.Size = new System.Drawing.Size(24, 24);
            this.picIconeEntradas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIconeEntradas.TabIndex = 0;
            this.picIconeEntradas.TabStop = false;
            // 
            // picIconeSaidas
            // 
            this.picIconeSaidas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(241)))), ((int)(((byte)(242)))));
            this.picIconeSaidas.Image = global::CASE.Properties.Resources.LucideAzulSaidas;
            this.picIconeSaidas.Location = new System.Drawing.Point(8, 8);
            this.picIconeSaidas.Name = "picIconeSaidas";
            this.picIconeSaidas.Size = new System.Drawing.Size(24, 24);
            this.picIconeSaidas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIconeSaidas.TabIndex = 0;
            this.picIconeSaidas.TabStop = false;
            // 
            // picIconeProdutos
            // 
            this.picIconeProdutos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
            this.picIconeProdutos.Image = global::CASE.Properties.Resources.LucideAzulProdutos;
            this.picIconeProdutos.Location = new System.Drawing.Point(8, 8);
            this.picIconeProdutos.Name = "picIconeProdutos";
            this.picIconeProdutos.Size = new System.Drawing.Size(24, 24);
            this.picIconeProdutos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIconeProdutos.TabIndex = 0;
            this.picIconeProdutos.TabStop = false;
            // 
            // picIconeEstoqueBaixo
            // 
            this.picIconeEstoqueBaixo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(241)))), ((int)(((byte)(242)))));
            this.picIconeEstoqueBaixo.Image = global::CASE.Properties.Resources.LucideAzulAlerta;
            this.picIconeEstoqueBaixo.Location = new System.Drawing.Point(8, 8);
            this.picIconeEstoqueBaixo.Name = "picIconeEstoqueBaixo";
            this.picIconeEstoqueBaixo.Size = new System.Drawing.Size(24, 24);
            this.picIconeEstoqueBaixo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIconeEstoqueBaixo.TabIndex = 0;
            this.picIconeEstoqueBaixo.TabStop = false;
            // 
            // DashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.panelEntradasMes);
            this.Controls.Add(this.panelSaidasMes);
            this.Controls.Add(this.panelGraficoMovimentacoes);
            this.Controls.Add(this.panelMovimentacoesRecentes);
            this.Controls.Add(this.lblTituloDashboard);
            this.Controls.Add(this.lblSubtituloDashboard);
            this.Controls.Add(this.panelTotalProdutos);
            this.Controls.Add(this.panelEstoqueBaixo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(954, 641);
            this.panelEntradasMes.ResumeLayout(false);
            this.panelEntradasMes.PerformLayout();
            this.panelIconeEntradas.ResumeLayout(false);
            this.panelSaidasMes.ResumeLayout(false);
            this.panelSaidasMes.PerformLayout();
            this.panelIconeSaidas.ResumeLayout(false);
            this.panelGraficoMovimentacoes.ResumeLayout(false);
            this.panelGraficoMovimentacoes.PerformLayout();
            this.panelMovimentacoesRecentes.ResumeLayout(false);
            this.panelMovimentacoesRecentes.PerformLayout();
            this.panelTotalProdutos.ResumeLayout(false);
            this.panelTotalProdutos.PerformLayout();
            this.panelIconeProdutos.ResumeLayout(false);
            this.panelEstoqueBaixo.ResumeLayout(false);
            this.panelEstoqueBaixo.PerformLayout();
            this.panelIconeEstoqueBaixo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIconeEntradas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIconeSaidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIconeProdutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIconeEstoqueBaixo)).EndInit();
            // pictureIconeTela — Lucide
            this.pictureIconeTela.Name = "pictureIconeTela";
            this.pictureIconeTela.Image = global::CASE.Properties.Resources.LucideAzulDashboard;
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelEntradasMes;
        private System.Windows.Forms.Label lblEntradasMesTitulo;
        private System.Windows.Forms.Label lblEntradasMesValor;
        private System.Windows.Forms.Panel panelIconeEntradas;
        private System.Windows.Forms.PictureBox picIconeEntradas;
        private System.Windows.Forms.Panel panelSaidasMes;
        private System.Windows.Forms.Label lblSaidasMesTitulo;
        private System.Windows.Forms.Label lblSaidasMesValor;
        private System.Windows.Forms.Panel panelIconeSaidas;
        private System.Windows.Forms.PictureBox picIconeSaidas;
        private System.Windows.Forms.Panel panelGraficoMovimentacoes;
        private System.Windows.Forms.Label lblTituloGrafico;
        private System.Windows.Forms.Panel panelMovimentacoesRecentes;
        private System.Windows.Forms.Label lblTituloMovimentacoes;
        private System.Windows.Forms.Label lblTituloDashboard;
        private System.Windows.Forms.Label lblSubtituloDashboard;
        private System.Windows.Forms.Panel panelTotalProdutos;
        private System.Windows.Forms.Panel panelIconeProdutos;
        private System.Windows.Forms.Label lblTotalProdutosValor;
        private System.Windows.Forms.Label lblTotalProdutosTitulo;
        private System.Windows.Forms.PictureBox picIconeProdutos;
        private System.Windows.Forms.Panel panelEstoqueBaixo;
        private System.Windows.Forms.Panel panelIconeEstoqueBaixo;
        private System.Windows.Forms.Label lblEstoqueBaixoValor;
        private System.Windows.Forms.Label lblEstoqueBaixoTitulo;
        private System.Windows.Forms.PictureBox picIconeEstoqueBaixo;
        private System.Windows.Forms.PictureBox pictureIconeTela;
    }
}

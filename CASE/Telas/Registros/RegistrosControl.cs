using System;
using System.Linq;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class RegistrosControl : UserControl
    {
        private readonly QuermessePreview exemplos;

        public RegistrosControl() : this(new QuermessePreview()) { }

        public RegistrosControl(QuermessePreview exemplos)
        {
            this.exemplos = exemplos;
            InitializeComponent();
            ConfigurarApresentacao();
            FrontendTheme.Aplicar(this);
            AtualizarExemplos();
        }

        private void ConfigurarApresentacao()
        {
            bool vendedor = exemplos.Atual.Perfil == PerfilUsuario.Vendedor;
            lblTitulo.Text = vendedor ? "Meus registros" : "Acompanhamento da equipe";
            lblSubtitulo.Text = vendedor ? "Registre vendas, perdas e pedidos de reposição do seu setor" : "Veja quem registrou, em qual setor e em qual turno";
            panelNovoRegistro.Visible = vendedor;
            panelListaRegistros.SetBounds(0, vendedor ? 307 : 105, panelCorpo.Width, vendedor ? 223 : 425);
            cmbSetorRegistro.SelectedIndex = 0;
            cmbPessoaRegistro.Items.Clear();
            cmbPessoaRegistro.Items.Add("Todos os funcionários");
            cmbPessoaRegistro.Items.AddRange(exemplos.Pessoas.Where(p => p.Perfil == PerfilUsuario.Vendedor).Cast<object>().ToArray());
            cmbPessoaRegistro.SelectedIndex = 0;
            cmbSetorRegistro.Enabled = cmbPessoaRegistro.Enabled = !vendedor;
            if (vendedor) { cmbSetorRegistro.Text = exemplos.Atual.Setor; cmbPessoaRegistro.SelectedItem = exemplos.Atual; }
            cmbProdutoRegistro.Items.Clear();
            cmbProdutoRegistro.Items.AddRange(exemplos.Produtos.Where(p => p.Setor == exemplos.Atual.Setor).Cast<object>().ToArray());
            if (cmbProdutoRegistro.Items.Count > 0) cmbProdutoRegistro.SelectedIndex = 0;
            lblContextoRegistro.Text = "Novo registro · " + exemplos.Atual.Setor + " · turno " + exemplos.Atual.Turno;
        }

        private void Filtros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (exemplos != null) AtualizarExemplos();
        }

        private void AtualizarExemplos()
        {
            dgvRegistrosEquipe.Rows.Clear();
            var registros = exemplos.Registros.AsEnumerable();
            if (cmbSetorRegistro.SelectedIndex > 0) registros = registros.Where(r => r.Setor == cmbSetorRegistro.Text);
            var pessoa = cmbPessoaRegistro.SelectedItem as PessoaPreview;
            if (pessoa != null) registros = registros.Where(r => r.PessoaId == pessoa.Id);
            foreach (var r in registros.OrderByDescending(r => r.Data))
                dgvRegistrosEquipe.Rows.Add(r.Data.ToString("dd/MM HH:mm"), r.Pessoa, r.Setor, r.Turno, r.Tipo, r.Produto, r.Quantidade, r.Observacao);
            dgvRegistrosEquipe.ClearSelection();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            labelAviso.Text = "Prévia do formulário: nenhum registro foi salvo.";
        }
    }
}

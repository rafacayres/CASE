using System;
using System.Linq;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class QuermesseDashboardControl : UserControl
    {
        private readonly QuermessePreview exemplos;

        public QuermesseDashboardControl() : this(new QuermessePreview()) { }

        public QuermesseDashboardControl(QuermessePreview exemplos)
        {
            this.exemplos = exemplos;
            InitializeComponent();
            FrontendTheme.Aplicar(this);
            CarregarExemplos();
            AjustarIndicadores();
        }

        private void CarregarExemplos()
        {
            lblSubtitulo.Text = exemplos.Evento + " · " + exemplos.Atual.Setor + " · turno " + exemplos.Atual.Turno;
            lblPerfilPreview.Text = "Seu espaço · " + exemplos.Atual.Perfil;
            lblDescricaoPerfil.Text = exemplos.Atual.Perfil == PerfilUsuario.Administrador
                ? "Organize equipes e setores e acompanhe os registros de todos os funcionários."
                : exemplos.Atual.Perfil == PerfilUsuario.Gerente
                    ? "Acompanhe a operação, registre custos e confira a apuração por setor e turno."
                    : "Adicione registros do seu setor e acompanhe suas próprias vendas e solicitações.";
            var registros = exemplos.Registros.AsEnumerable();
            if (exemplos.Atual.Perfil == PerfilUsuario.Vendedor) registros = registros.Where(r => r.PessoaId == exemplos.Atual.Id);
            foreach (var registro in registros.OrderByDescending(r => r.Data))
                dgvAtividade.Rows.Add(registro.Pessoa, registro.Setor, registro.Turno, registro.Tipo, registro.Produto, registro.Quantidade);
            dgvAtividade.ClearSelection();
        }

        private void panelCorpo_SizeChanged(object sender, EventArgs e) { AjustarIndicadores(); }

        private void AjustarIndicadores()
        {
            var indicadores = new[] { panelIndicador0, panelIndicador1, panelIndicador2, panelIndicador3 };
            for (int i = 0; i < indicadores.Length; i++)
                indicadores[i].SetBounds(i * (panelCorpo.Width + 10) / 4, 0, (panelCorpo.Width - 30) / 4, 105);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CASE
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
            // Arredondar novos componentes conforme especificado
            CASE.Utils.VisualUtils.Arredondar(panelEntradasMes, 12);
            CASE.Utils.VisualUtils.Arredondar(panelSaidasMes, 12);
            CASE.Utils.VisualUtils.Arredondar(panelIconeEntradas, 10);
            CASE.Utils.VisualUtils.Arredondar(panelIconeSaidas, 10);
            CASE.Utils.VisualUtils.Arredondar(panelGraficoMovimentacoes, 12);
            CASE.Utils.VisualUtils.Arredondar(panelMovimentacoesRecentes, 12);
        }
    }
}

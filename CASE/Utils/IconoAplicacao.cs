using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CASE.Utils
{
    internal static class IconoAplicacao
    {
        private static Icon iconePrincipal;

        public static void Aplicar(Form formulario)
        {
            if (formulario == null)
            {
                throw new ArgumentNullException(nameof(formulario));
            }

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            if (iconePrincipal == null)
            {
                string caminhoIcone = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Resources",
                    "CASE.ico");

                iconePrincipal = new Icon(caminhoIcone);
            }

            formulario.Icon = (Icon)iconePrincipal.Clone();
        }
    }
}

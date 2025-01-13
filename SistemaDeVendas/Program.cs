using SistemaDeVendas.UI.Usuario;
using System;
using System.Windows.Forms;

namespace SistemaDeVendas
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (frmLogin loginForm = new frmLogin())
            {
                Application.Run(loginForm);


                if (loginForm.LoginSuccesso)
                {
                    // Exibe a tela principal
                    Application.Run(new frmPrincipal());
                }
                else {
                    // Fecha o aplicativo se o login falhar ou for cancelado
                    Application.Exit();
                }
            }
        }
    }
}
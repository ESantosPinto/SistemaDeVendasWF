using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeVendas
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }
        private void clienteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form cadastroDeCliente = new frmCadastroDeCliente();
            AddFormToToolStripContainer(toolStripContainer1, cadastroDeCliente);
        }
       
        private void AddFormToToolStripContainer(ToolStripContainer toolStripContainer, Form form)
        {
            // Remover qualquer formulário previamente adicionado.
            foreach (Control ctrl in toolStripContainer.ContentPanel.Controls)
            {
                ctrl.Dispose(); // Libera os recursos do formulário antigo.
            }
            // Remove a borda e transforma o Form em um controle.
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // Adiciona o Form ao ContentPanel.
            toolStripContainer.ContentPanel.Controls.Add(form);

            // Exibe o Form embutido no ToolStripContainer.
            form.Show();
        }

        
    }
}

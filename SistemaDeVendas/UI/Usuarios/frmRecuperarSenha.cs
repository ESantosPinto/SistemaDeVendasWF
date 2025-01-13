using SistemaDeVendas.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeVendas.UI.Usuarios
{
    public partial class frmRecuperarSenha : Form
    {
        BLL_Login bLL_Login = new BLL_Login();

        public frmRecuperarSenha()
        {
            InitializeComponent();
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }       

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Por favor, insira o e-mail.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Simula o envio de um e-mail para redefinição
                bool emailEnviado = bLL_Login.EnviarEmailRecuperacao(email);

                if (emailEnviado)
                {
                    MessageBox.Show("Instruções para redefinição de senha foram enviadas para o e-mail informado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("O e-mail informado não está cadastrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar a recuperação de senha: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using SistemaDeVendas.BLL;
using SistemaDeVendas.BLL.Usuarios;
using SistemaDeVendas.Models;
using SistemaDeVendas.Ui.Usuarios;
using SistemaDeVendas.UI.Usuarios;
using SistemaDeVendas.Util;
using System;
using System.Windows.Forms;

namespace SistemaDeVendas.UI.Usuario
{
    public partial class frmLogin : Form
    {
        BLL_Login bLL_Login = new BLL_Login();
        public bool LoginSuccesso { get; private set; }

        public frmLogin()
        {
            InitializeComponent();
            LoginSuccesso = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {       
            ResultadoValidacao resultadoValidacao = bLL_Login.ValidarUsuario(txtUsuario.Text, txtSenha.Text);

            if (!string.IsNullOrEmpty(resultadoValidacao.MensagemErro))
            {
                MessageBox.Show(resultadoValidacao.MensagemErro, "Erro de Validação");

                // Colocar o foco no campo inválido
                switch (resultadoValidacao.CampoInvalido)
                {
                    case "Usuario":
                        txtUsuario.Focus();
                        break;

                    case "Senha":
                        txtSenha.Focus();
                        break;
                }
            }
            else
            {
                try
                {

                    bool isValid = bLL_Login.ValidarLogin(txtUsuario.Text, txtSenha.Text);

                    if (isValid)
                    {

                        LoginSuccesso = true; // Marca o login como bem-sucedido
                        this.Close(); // Fecha o formulário de login
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha inválidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

               
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LoginSuccesso = false; // Marca o login como não realizado
            this.Close(); // Fecha o formulário de login
        }

        private void linkCadastro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCadastroUsuario cadastroForm = new frmCadastroUsuario();
            cadastroForm.ShowDialog(); // Usando ShowDialog para abrir de forma modal
        }

        private string GerarHashSenha(string senha)
        {
            return UtilitarioHash.HashSenha(senha);
        }

        private void linkEsqueceuSenha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (frmRecuperarSenha recuperarSenhaForm = new frmRecuperarSenha())
                {
                    recuperarSenhaForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar a solicitação: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

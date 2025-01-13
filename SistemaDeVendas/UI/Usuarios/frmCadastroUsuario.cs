using SistemaDeVendas.BLL.Usuarios;
using SistemaDeVendas.Models;
using SistemaDeVendas.Models.Usuarios;
using SistemaDeVendas.Util;
using System;
using System.Windows.Forms;

namespace SistemaDeVendas.Ui.Usuarios
{
    public partial class frmCadastroUsuario : Form
    {
        BLL_Usuario bLL_Usuario = new BLL_Usuario();

        public frmCadastroUsuario()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Captura os dados do formulário
                string nome = txtNome.Text.Trim();
                string email = txtEmail.Text.Trim();
                string usuarioLogin = txtUsuarioLogin.Text.Trim();
                string senha = txtSenha.Text.Trim();
                string confirmarSenha = txtConfirmarSenha.Text.Trim();


                // Criação do hash da senha (simulação)
                string senhaHash = GerarHashSenha(senha);
                string confirmarSenhaHash = GerarHashSenha(senha);

                Usuario novoUsuario = new Usuario
                {
                    Nome = nome,
                    Email = email,
                    UsuarioLogin = usuarioLogin,
                    SenhaHash = senhaHash

                };


                ResultadoValidacao resultadoValidacao = bLL_Usuario.ValidarUsuario(novoUsuario, senha, confirmarSenha);

                if (!string.IsNullOrEmpty(resultadoValidacao.MensagemErro))
                {
                    MessageBox.Show(resultadoValidacao.MensagemErro, "Erro de Validação");

                    // Colocar o foco no campo inválido
                    switch (resultadoValidacao.CampoInvalido)
                    {
                        case "Nome":
                            txtNome.Focus();
                            break;

                        case "Email":
                            txtEmail.Focus();
                            break;

                        case "usuarioLogin":
                            txtUsuarioLogin.Focus();
                            break;
                        case "Senha":
                            txtSenha.Focus();
                            break;
                        case "ConfirmarSenha":
                            txtConfirmarSenha.Focus();
                            break;
                    }
                }
                else
                {
                    string retornoCadastro = bLL_Usuario.CadastrarUsuario(novoUsuario);

                    if (!string.IsNullOrEmpty(retornoCadastro))
                    {
                        MessageBox.Show(retornoCadastro, "Resultado do Cadastro");
                    }
                    else
                    {
                        MessageBox.Show("O cadastro foi realizado, mas nenhuma mensagem foi retornada.", "Atenção");
                    }
                }                              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GerarHashSenha(string senha)
        {
            return UtilitarioHash.HashSenha(senha);
        }       

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close(); // Fecha o formulário de login
        }
    }
}

using SistemaDeVendas.DAL.Login;
using SistemaDeVendas.Models;
using SistemaDeVendas.Models.Usuarios;
using System;
using System.Windows.Forms;

namespace SistemaDeVendas.BLL
{
    public class BLL_Login
    {
        DAL_Login _dALLogin = new DAL_Login();
        public bool ValidarLogin(string usuarioLogin, string senha)
        {
            var result = _dALLogin.ValidarLogin(usuarioLogin, senha);
            return result;
        }

        public bool EnviarEmailRecuperacao(string email)
        {
            // Verifica se o e-mail existe no banco
            Usuario usuario = _dALLogin.ObterPorEmail(email); // Método no repositório

            if (usuario == null)
            {
                return false; // E-mail não encontrado
            }

            // Simulado no envio de um e-mail
            string mensagem = $"Olá, {usuario.Nome}. Clique no link abaixo para redefinir sua senha:\n\n" +
                              "https://sistemadevendas.com/redefinir-senha?token=UNIQUE_TOKEN";

            // Aqui você implementaria o envio de e-mail real
            Console.WriteLine($"Enviando e-mail para {email}:\n{mensagem}");

            return true; // E-mail enviado
        }

        public  ResultadoValidacao ValidarUsuario(string usuario, string senha)
        {
            if (usuario == null)
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "Por favor, insira o nome de usuário.",
                    CampoInvalido = "Usuario"
                };
            }

            if (usuario == null)
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "Por favor, insira a senha.",
                    CampoInvalido = "Senha"
                };
            }
            else
            {
                // Dados válidos
                return new ResultadoValidacao
                {
                    MensagemErro = string.Empty,
                    CampoInvalido = string.Empty
                };
            }
        }
    }
}

using SistemaDeVendas.DAL.Usuarios;
using SistemaDeVendas.Models;
using SistemaDeVendas.Models.Usuarios;
using SistemaDeVendas.Util;
using SistemaDeVendas.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendas.BLL.Usuarios
{
    public class BLL_Usuario
    {
        public DAL_Usuario dALUsuario = new DAL_Usuario();
        Logger logger = new Logger();

        public ResultadoValidacao ValidarUsuario(Usuario usuario, string senha, string confirmarSenha)
        {
            // Validações básicas
            if (usuario == null)
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "Usuário não pode ser nulo.",
                    CampoInvalido = string.Empty
                };
            }
            else if (string.IsNullOrWhiteSpace(usuario.Nome))
                return new ResultadoValidacao
                {
                    MensagemErro = "O campo Nome é obrigatório.",
                    CampoInvalido = "Nome"
                };
            else if(string.IsNullOrWhiteSpace(usuario.Email))
                return new ResultadoValidacao
                {
                    MensagemErro = "O campo Email é obrigatório.",
                    CampoInvalido = "Email"
                };
            else if(string.IsNullOrWhiteSpace(usuario.UsuarioLogin))
                return new ResultadoValidacao
                {
                    MensagemErro = "O campo Login é obrigatório.",
                    CampoInvalido = "usuarioLogin"
                };
            else if(string.IsNullOrWhiteSpace(senha) || senha.Length < 6 || senha.Length > 10)
                return new ResultadoValidacao
                {
                    MensagemErro = "O campo Senha é obrigatório e deve ter entre 6 e 10 caracteres.",
                    CampoInvalido = "Senha"
                };
            else if(senha != confirmarSenha)
                return new ResultadoValidacao
                {
                    MensagemErro = "As senhas não coincidem.",
                    CampoInvalido = "ConfirmarSenha"
                };
            else if (!ValidadorDeEmail.ValidarEmail(usuario.Email))
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "O E-mail é inválido.",
                    CampoInvalido = "Email"
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

        public string CadastrarUsuario(Usuario novoUsuario)
        {
            try
            {
                return dALUsuario.CadastrarUsuario(novoUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar usuário", ex);
            }
        }

    }
}

using SistemaDeVendas.Models.Usuario;

namespace SistemaDeVendas.Models.Usuarios
{
    public class Usuario : Identity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string UsuarioLogin { get; set; }
        public string SenhaHash { get; set; }

        public Usuario() { }

        public Usuario(string nome, string email, string usuarioLogin, string senhaHash)
        {
            Nome = nome;
            Email = email;
            UsuarioLogin = usuarioLogin;
            SenhaHash = senhaHash;
        }
    }
}

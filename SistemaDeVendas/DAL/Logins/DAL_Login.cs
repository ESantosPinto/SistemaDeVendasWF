using SistemaDeVendas.Models.Usuario;
using SistemaDeVendas.Models.Usuarios;
using SistemaDeVendas.Util;
using System.Data.SqlClient;

namespace SistemaDeVendas.DAL.Login
{
    public class DAL_Login
    {
        DAL_Connection _dalConnection = new DAL_Connection();



        public bool ValidarLogin(string usuarioLogin, string senhaHash)
        {
            string hashedSenha = UtilitarioHash.HashSenha(senhaHash);
            string connectionString = _dalConnection.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(1) FROM Usuarios WHERE usuarioLogin = @UsuarioLogin AND senhaHash = @SenhaHash";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UsuarioLogin", usuarioLogin);
                    command.Parameters.AddWithValue("@SenhaHash", hashedSenha);

                    int result = (int)command.ExecuteScalar();
                    return result > 0;
                }
            }
        }

        public Usuario ObterPorEmail(string email)
        {
            string connectionString = _dalConnection.GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Usuarios WHERE Email = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                UsuarioLogin = reader.GetString(reader.GetOrdinal("UsuarioLogin")),
                                SenhaHash = reader.GetString(reader.GetOrdinal("SenhaHash"))
                            };
                        }
                    }
                }
            }

            return null;
        }


    }
}

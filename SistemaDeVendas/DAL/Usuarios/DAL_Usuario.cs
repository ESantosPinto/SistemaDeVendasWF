using SistemaDeVendas.Models.Usuario;
using SistemaDeVendas.Models.Usuarios;
using SistemaDeVendas.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaDeVendas.DAL.Usuarios
{
    public class DAL_Usuario
    {

        DAL_Connection _dalConnection = new DAL_Connection();
        Logger logger = new Logger();


        public string CadastrarUsuario(Usuario usuario)
        {
            string mensagemRetorno = string.Empty;

            try
            {
                string connectionString = _dalConnection.GetConnectionString();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("Pr_CadastrarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar os parâmetros
                        command.Parameters.AddWithValue("@Nome", usuario.Nome);
                        command.Parameters.AddWithValue("@Email", usuario.Email);
                        command.Parameters.AddWithValue("@UsuarioLogin", usuario.UsuarioLogin);
                        command.Parameters.AddWithValue("@Senha", usuario.SenhaHash);

                        var mensagemParam = new SqlParameter("@Mensagem", SqlDbType.NVarChar, 1000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(mensagemParam);

                        // Executar o comando
                        command.ExecuteNonQuery();

                        // Obter a mensagem de retorno
                        mensagemRetorno = mensagemParam.Value?.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Capturar erros relacionados ao SQL Server
                logger.Log($"Erro ao acessar o banco de dados: {ex.Message}", "ERROR", nameof(DAL_Usuario));
                mensagemRetorno = $"Erro ao acessar o banco de dados: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Capturar outros tipos de exceções
                logger.Log($"Erro inesperado ao cadastrar usuário: {ex.Message}", "ERROR", nameof(DAL_Usuario));
                mensagemRetorno = $"Ocorreu um erro: {ex.Message}";
            }

            return mensagemRetorno;
        }
    }
}



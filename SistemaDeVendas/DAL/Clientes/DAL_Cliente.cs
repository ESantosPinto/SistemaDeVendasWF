using SistemaDeVendas.Models;
using SistemaDeVendas.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaDeVendas.DAL
{
    public class DAL_Cliente
    {
        DAL_Connection _dalConnection = new DAL_Connection();

        public string CadastrarCliente(Cliente cliente)
        {
            string mensagemRetorno = string.Empty;

            try
            {
                string connectionString = _dalConnection.GetConnectionString();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("Pr_CadastrarCliente", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar os parâmetros
                        command.Parameters.AddWithValue("@Nome", cliente.Nome);
                        command.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                        command.Parameters.AddWithValue("@Email", cliente.Email);
                        command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                        command.Parameters.AddWithValue("@Cep", cliente.Cep);
                        command.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                        command.Parameters.AddWithValue("@Logradouro", cliente.Logradouro);
                        command.Parameters.AddWithValue("@Numero", cliente.Numero);
                        command.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                        command.Parameters.AddWithValue("@Estado", cliente.UF);

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
                mensagemRetorno = $"Erro ao acessar o banco de dados: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Capturar outros tipos de exceções
                mensagemRetorno = $"Ocorreu um erro: {ex.Message}";
            }

            return mensagemRetorno;
        }

        public List<Cliente> ConsultarCliente()
        {
            var clientes = new List<Cliente>();  // Lista para armazenar os resultados.

            try
            {
                string connectionString = _dalConnection.GetConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT IdCliente, Nome, Email, Cpf, Telefone, Cep, Bairro, Logradouro, Numero, Cidade, Estado FROM Clientes"; 
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    // Usando 'using' para garantir que o SqlDataReader seja fechado corretamente
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Processar os dados e adicionar à lista de clientes
                            var cliente = new Cliente
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Cpf = reader.GetString(reader.GetOrdinal("Cpf")),
                                Telefone = reader.GetString(reader.GetOrdinal("Telefone")),
                                Cep = reader.GetString(reader.GetOrdinal("Cep")),
                                Bairro = reader.GetString(reader.GetOrdinal("Bairro")),
                                Logradouro = reader.GetString(reader.GetOrdinal("Logradouro")),
                                Numero = reader.GetString(reader.GetOrdinal("Numero")),
                                Cidade = reader.GetString(reader.GetOrdinal("Cidade")),
                                UF = reader.GetString(reader.GetOrdinal("Estado"))
                            };
                            clientes.Add(cliente);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Logar ou tratar erros específicos de SQL
                // Exemplo: Log.Error("Erro na consulta ao banco de dados", sqlEx);
                throw new ApplicationException("Erro ao consultar os clientes no banco de dados.", sqlEx);
            }
            catch (Exception ex)
            {
                // Logar ou tratar outros tipos de erros
                // Exemplo: Log.Error("Erro inesperado", ex);
                throw new ApplicationException("Erro inesperado ao consultar os clientes.", ex);
            }

           
            return clientes;
        }

        public string ExcluirCliente(int clienteId)
        {
            string mensagemRetorno = string.Empty;

            try
            {
                string connectionString = _dalConnection.GetConnectionString();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Clientes WHERE IdCliente = @IdCliente";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdCliente", clienteId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            mensagemRetorno = "Cliente excluído com sucesso.";
                        }
                        else
                        {
                            mensagemRetorno = "Cliente não encontrado.";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                mensagemRetorno = $"Erro ao excluir cliente: {ex.Message}";
            }
            catch (Exception ex)
            {
                mensagemRetorno = $"Erro inesperado: {ex.Message}";
            }

            return mensagemRetorno;
        }


    }
}
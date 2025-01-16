using SistemaDeVendas.Models;
using SistemaDeVendas.Models.Produto;
using SistemaDeVendas.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaDeVendas.DAL.Produtos
{
    public class DAL_Produto
    {
        private readonly DAL_Connection _connection;

        public DAL_Produto()
        {
            _connection = new DAL_Connection();
        }

        public string InserirProduto(Produto produto)
        {
            string mensagemRetorno = string.Empty;

            try
            {
                string connectionString = _connection.GetConnectionString();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("Pr_CadastrarProduto", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar os parâmetros
                        command.Parameters.AddWithValue("@Codigo", produto.Codigo);
                        command.Parameters.AddWithValue("@Nome", produto.Nome);
                        command.Parameters.AddWithValue("@Preco", produto.Preco);
                        command.Parameters.AddWithValue("@Descricao", produto.Descricao);
                        command.Parameters.AddWithValue("@Quantidade", produto.Quantidade);

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

        public DataTable BuscarProdutos()
        {
            string query = "SELECT IdProduto, Codigo, Nome, Preco, Descricao, Estoque FROM Produtos";

            using (SqlConnection conn = _connection.OpenConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable produtos = new DataTable();
                    adapter.Fill(produtos);
                    return produtos;
                }
            }
        }

        public string AtualizarProduto(Produto produto)
        {
            string mensagemRetorno = string.Empty;

            try
            {
                string connectionString = _connection.GetConnectionString();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("Pr_AtualizarProduto", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdProduto", produto.Id);
                        command.Parameters.AddWithValue("@Nome", produto.Nome);
                        command.Parameters.AddWithValue("@Codigo", produto.Codigo);
                        command.Parameters.AddWithValue("@Descricao", produto.Descricao);
                        command.Parameters.AddWithValue("@Preco", produto.Preco);
                        command.Parameters.AddWithValue("@Estoque",produto.Quantidade);

                        var mensagemParam = new SqlParameter("@Mensagem", SqlDbType.NVarChar, 1000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(mensagemParam);

                        int rowsAffected = command.ExecuteNonQuery();
                        mensagemRetorno = mensagemParam.Value?.ToString() ?? "Cliente não encontrado.";
                    }
                }
            }
            catch (SqlException ex)
            {
                mensagemRetorno = $"Erro ao atualizar produto: {ex.Message}";
            }
            catch (Exception ex)
            {
                mensagemRetorno = $"Erro inesperado: {ex.Message}";
            }

            return mensagemRetorno;
        }

        public string ExcluirProduto(int produtoId)
        {
            string mensagemRetorno = string.Empty;

            try
            {
                string connectionString = _connection.GetConnectionString();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Produtos WHERE IdProduto = @IdProduto";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdProduto", produtoId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            mensagemRetorno = "Produto excluído com sucesso.";
                        }
                        else
                        {
                            mensagemRetorno = "Produto não encontrado.";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                mensagemRetorno = $"Erro ao excluir produto: {ex.Message}";
            }
            catch (Exception ex)
            {
                mensagemRetorno = $"Erro inesperado: {ex.Message}";
            }

            return mensagemRetorno;
        }

    }
}

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SistemaDeVendas.Util
{
    public class DAL_Connection
    {
        private readonly string _connectionString;

        public DAL_Connection()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"]?.ConnectionString;
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("A string de conexão não foi configurada.");
            }
        }

        public DAL_Connection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        // Método para abrir conexão
        public SqlConnection OpenConnection()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir conexão com o banco de dados: " + ex.Message);
            }
        }

        // Método para fechar conexão
        public void CloseConnection(SqlConnection connection)
        {
            try
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao fechar conexão com o banco de dados: " + ex.Message);
            }
        }

        // Método para executar comandos SQL
        public DataTable ExecuteQuery(SqlCommand query)
        {
            try
            {
                SqlConnection connection = OpenConnection();
                query.Connection = connection; 
                SqlDataAdapter adapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao executar consulta SQL: " + ex.Message);
            }
            finally
            {
                if (query.Connection != null)
                {
                    query.Connection.Close();
                }
            }
        }

        // Método para executar comandos não-query (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query)
        {
            try
            {
                SqlConnection connection = OpenConnection();
                SqlCommand command = new SqlCommand(query, connection);
                int result = command.ExecuteNonQuery();
                connection.Close(); // Fechando a conexão explicitamente
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao executar comando SQL: " + ex.Message);
            }
        }

    }
}

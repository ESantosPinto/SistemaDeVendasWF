using SistemaDeVendas.Registro;
using System;
using System.Data.SqlClient;

namespace SistemaDeVendas.Util
{
    public class Logger
    {
        DAL_Connection _dalConnection = new DAL_Connection();

        public void Log(string message, string level, string source)
        {
            // Cria uma entrada de log
            var entradaLogs = new RegistroDeLog
            {
                Mensagem = message,
                Nivel = level,
                Fonte = source,
                DataHora = DateTime.Now  // Adiciona o timestamp
            };

            SalvarLogNoBanco(entradaLogs);
        }

        private void SalvarLogNoBanco(RegistroDeLog log)
        {
            try
            {
                using (var connection = new SqlConnection(_dalConnection.GetConnectionString()))
                {
                    connection.Open();

                    // Comando SQL para inserir o log no banco de dados
                    using (var command = new SqlCommand("INSERT INTO Logs (Message, Level, Source, Timestamp) VALUES (@Message, @Level, @Source, @Timestamp)", connection))
                    {
                        // Adiciona os parâmetros de forma correta
                        command.Parameters.AddWithValue("@Message", log.Mensagem);
                        command.Parameters.AddWithValue("@Level", log.Nivel);
                        command.Parameters.AddWithValue("@Source", log.Fonte);
                        command.Parameters.AddWithValue("@Timestamp", log.DataHora);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Se falhar ao salvar no banco, exibe um erro no console
                Console.WriteLine($"Erro ao salvar log no banco: {ex.Message}");
            }
        }
    }
}

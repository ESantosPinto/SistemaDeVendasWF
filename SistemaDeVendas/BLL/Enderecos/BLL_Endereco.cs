using Newtonsoft.Json;
using SistemaDeVendas.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SistemaDeVendas.BLL
{
   public class BLL_Endereco
    {
        Endereco endereco = new Endereco();
        private readonly string baseUrl;

        public BLL_Endereco()
        {
            // Carrega a URL do app.config
            baseUrl = ConfigurationManager.AppSettings["ViaCepBaseUrl"];
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new Exception("A URL do serviço ViaCEP não está configurada.");
            }
        }
        public async Task<Endereco> BuscarEndereco(string cep)
        {
            string url = $"{baseUrl}{cep}/json/";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Alteração: Usar JsonConvert em vez de JsonSerializer
                        var resultadoEndereco = JsonConvert.DeserializeObject<Endereco>(jsonResponse);

                                if (resultadoEndereco != null && string.IsNullOrEmpty(resultadoEndereco.Erro))
                        {
                            endereco.Logradouro = resultadoEndereco.Logradouro;
                            endereco.Bairro = resultadoEndereco.Bairro;
                            endereco.Cidade = resultadoEndereco.Cidade;
                            endereco.UF = resultadoEndereco.UF;
                            endereco.Cep = resultadoEndereco.Cep;

                        }
                        else
                        {
                            MessageBox.Show("CEP não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro ao buscar o CEP.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return endereco;
        }
    }


}

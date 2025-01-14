using Newtonsoft.Json;
using SistemaDeVendas.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SistemaDeVendas.Util
{
    public class ViaCepService
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public async Task<Endereco> ObterEnderecoPorCepAsync(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8 || !int.TryParse(cep, out _))
            {
                throw new ArgumentException("O CEP informado é inválido.");
            }

            string url = $"https://viacep.com.br/ws/{cep}/json/";

            try
            {
                // Faz a chamada HTTP
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Erro ao acessar o serviço ViaCEP. Código: {response.StatusCode}");
                }

                // Lê a resposta como string
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Desserializa o JSON para o objeto Endereco
                var endereco = JsonConvert.DeserializeObject<Endereco>(jsonResponse);

                // Verifica se o CEP foi encontrado (ViaCEP retorna uma chave "erro" no JSON)
                if (endereco == null || !string.IsNullOrEmpty(endereco.Erro))
                {
                    return null; // CEP não encontrado
                }

                return endereco;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar o endereço no ViaCEP.", ex);
            }
        }
    }

    
}

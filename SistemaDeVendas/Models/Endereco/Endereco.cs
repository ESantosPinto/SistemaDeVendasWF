using Newtonsoft.Json;
using SistemaDeVendas.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendas.Models
{
    public class Endereco : Identity
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        [JsonProperty("localidade")]
        public string Cidade { get; set; }
        public string UF { get; set; }
     
        [JsonProperty("cep")]
        public string Cep { get; set; }
        public string Numero { get; set; }
        [JsonProperty("erro")]
        public string Erro { get; set; }
    }
}

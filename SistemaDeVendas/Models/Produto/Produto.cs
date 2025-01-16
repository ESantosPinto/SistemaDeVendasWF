using SistemaDeVendas.Models.Usuario;

namespace SistemaDeVendas.Models.Produto
{
    public class Produto : Identity
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
    }
}

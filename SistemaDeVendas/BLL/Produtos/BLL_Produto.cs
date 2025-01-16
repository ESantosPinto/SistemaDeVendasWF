using SistemaDeVendas.DAL;
using SistemaDeVendas.DAL.Produtos;
using SistemaDeVendas.Models;
using SistemaDeVendas.Models.Produto;
using System;
using System.Data;

namespace SistemaDeVendas.BLL
{
    public class BLL_Produto
    {
        DAL_Produto _dalProduto = new DAL_Produto();

        public ResultadoValidacao ValidarProduto(Produto produto)
        {
            if (string.IsNullOrEmpty(produto.Codigo))
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "O código do produto não pode estar vazio.",
                    CampoInvalido = "Codigo"
                };
            }

            else if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "O nome do produto é obrigatório.",
                    CampoInvalido = "Nome"
                };
            }
            else if (produto.Preco <= 0)
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "O preço do produto deve ser maior que zero.",
                    CampoInvalido = "Preco"
                };
            }

            else if (produto.Quantidade < 0)
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "A quantidade do produto não pode ser negativa.",
                    CampoInvalido = "Quantidade"
                };
            }

            else if (produto.Descricao.Length > 255)
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "A descrição do produto deve ter no máximo 255 caracteres.",
                    CampoInvalido = "Descricao"
                };
            }
            else
            {
                // Dados válidos
                return new ResultadoValidacao
                {
                    MensagemErro = string.Empty,
                    CampoInvalido = string.Empty
                };
            }
        }

        public string CadastrarProduto(Produto produto)
        {
                return _dalProduto.InserirProduto(produto);    
        }

        public DataTable BuscarProdutos()
        {

            return _dalProduto.BuscarProdutos();
        }

        public string AtualizarProduto(Produto produto)
        {
            if (produto == null || produto.Id <= 0)
            {
                return "Dados inválidos para atualização.";
            }

            return _dalProduto.AtualizarProduto(produto);
        }

        public string ExcluirProduto(int produtoId)
        {
            if (produtoId <= 0)
            {
                return "ID do produto inválido.";
            }

            return _dalProduto.ExcluirProduto(produtoId);
        }
    }

   

}

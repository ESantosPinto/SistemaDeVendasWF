using SistemaDeVendas.DAL;
using SistemaDeVendas.Models;
using SistemaDeVendas.Utils;
using System;
using System.Data;

namespace SistemaDeVendas.BLL
{
    public class BLL_Cliente
    {
        DAL_Cliente _dalCliente = new DAL_Cliente();        

        public BLL_Cliente()
        {
        }

        public ResultadoValidacao ValidarDadosCliente(Cliente cliente) {

            if (cliente == null)
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "Cliente não pode ser nulo.",
                    CampoInvalido = string.Empty
                };
            }

           else if (string.IsNullOrEmpty(cliente.Nome))
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "O campo Nome é obrigatório.",
                    CampoInvalido = "Nome"
                };
            }

            else if (string.IsNullOrEmpty(cliente.Cpf))
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "O campo CPF é obrigatório.",
                    CampoInvalido = "Cpf"
                };
            }

            else if (string.IsNullOrEmpty(cliente.Telefone))
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "O campo Telefone é obrigatório.",
                    CampoInvalido = "Telefone"
                };
            }

            else if (!ValidadorDeEmail.ValidarEmail(cliente.Email))
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "O E-mail é inválido.",
                    CampoInvalido = "Email"
                };
            }

            else if (string.IsNullOrEmpty(cliente.Cep))
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "O campo Cep é obrigatório.",
                    CampoInvalido = "Cep"
                };
            }

            else if (string.IsNullOrEmpty(cliente.Numero))
            {
                return new ResultadoValidacao
                {
                    MensagemErro = "O campo Numero é obrigatório.",
                    CampoInvalido = "Numero"
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

        public string CadastrarCliente(Cliente cliente)
        {
           return _dalCliente.CadastrarCliente(cliente);
        }

        internal object ConsultarCliente()
        {
             var cliente = _dalCliente.ConsultarCliente();
            return cliente;
        }

        public string ExcluirCliente(int clienteId)
        {
            if (clienteId <= 0)
            {
                return "ID do cliente inválido.";
            }

            return _dalCliente.ExcluirCliente(clienteId);
        }

    }
}
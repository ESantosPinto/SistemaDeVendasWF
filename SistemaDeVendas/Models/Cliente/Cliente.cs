﻿namespace SistemaDeVendas.Models
{
    public class Cliente : Endereco
    {        
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }      
    }
}

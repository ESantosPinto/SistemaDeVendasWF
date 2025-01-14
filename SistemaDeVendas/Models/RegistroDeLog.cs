using System;

namespace SistemaDeVendas.Registro
{
    public class RegistroDeLog
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public string Nivel { get; set; } 
        public string Fonte { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;

       
    }
}

using System;

namespace SistemaDeVendas.Registro
{
    public class RegistroDeLog
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public string Nivel { get; set; } // Ex.: "INFO", "ERRO", "DEPURAÇÃO"
        public string Fonte { get; set; } // Origem do log (classe ou método)
        public DateTime DataHora { get; set; }

        public RegistroDeLog()
        {
            DataHora = DateTime.Now;
        }
    }
}

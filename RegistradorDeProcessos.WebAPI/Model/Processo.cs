using System.Collections.Generic;

namespace RegistradorDeProcessos.WebAPI.Model
{
    public class Processo
    {
        public string Numero { get; set; }

        public string Classe { get; set; }

        public string Area { get; set; }

        public string Assunto { get; set; }

        public string Origem { get; set; }

        public string Distribuicao { get; set; }

        public string Relator { get; set; }

        public virtual ICollection<Movimentacao> Movimentacoes { get; set; }
    }
}

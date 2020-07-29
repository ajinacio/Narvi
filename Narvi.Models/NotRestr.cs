using System;

namespace Narvi.Models
{
    public class NotRestr
    {
        public int NotificacaoId { get; set; }
        public int RestricaoId { get; set; }
        public string Complemento { get; set; }
        public string Formatacao { get; set; }
        public Double Valor { get; set; }
    }
}

using System;

namespace Narvi.Models
{
    public class Notificacao
    {
        public int NotificacaoId { get; set; }
        public string Numero { get; set; }
        public string Assunto { get; set; }
        public int PessoaId { get; set; }
        public int ProcessoId { get; set; }
        public int AgenteId { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Recebimento { get; set; }
    }
}

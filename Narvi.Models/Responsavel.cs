namespace Narvi.Models
{
    public class Responsavel
    {
        public int RespId { get; set; }
        public int ProcessoId { get; set; }
        public int PessoaId { get; set; }
        public string Convenente { get; set; }
        public int PatronoId { get; set; }
        public int Situacao { get; set; }
        public string Obs { get; set; }
        public string TipoPatrono { get; set; }
    }
}

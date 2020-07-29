namespace Narvi.Models
{
    public class Processo
    {
        public int ProcessoId { get; set; }
        public int Numero { get; set; }
        public int Ano { get; set; }
        public int Obra { get; set; }
        public int ApensoId { get; set; }
        public int NaturezaId { get; set; }
        public int EspecieId { get; set; }
        public int ProcuradorId { get; set; }
        public string Objeto { get; set; }
        public int Volume { get; set; }
        public string Ajuste { get; set; }
        public double ValorGlobal { get; set; }
        public int SetorId { get; set; }
        public int ArmarioId { get; set; }
        public int ConcedenteId { get; set; }
        public int ConvPublId { get; set; }
        public int ConvPrivId { get; set; }
        public string Parcela { get; set; }
        public int RelatorId { get; set; }
        public string Obs { get; set; }
        public int Situacao { get; set; }
        public int AgenteId { get; set; }
        public string Inventariado { get; set; }
        public string Inventariante { get; set; }
    }
}

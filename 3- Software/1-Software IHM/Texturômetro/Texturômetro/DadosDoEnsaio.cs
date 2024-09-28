using ClassesSuporteTexturometro;
using ProbeTexturometro;
using System;



namespace DadosDeEnsaio
{
    public class DataTest {
        public string Nome { get; set; }
        public TipoDeTeste Tipo { get; set; }
        public Probe PontaDeTeste { get; set; }
        public double VelPreTeste { get; set; }
        public double VelTeste { get; set; }
        public double VelPosTeste { get; set; }
        public TipoTarget TipoLimite { get; set; }
        public double ValorLimite { get; set; }
        public double Tempo { get; set; }
        public TipoTrigger TipoDeteccao { get; set; }
        public double ValorDeteccao { get; set; }
        public TipoTara TipoTara { get; set; }
        public double PosInicial { get; set; }
        public DateTime DataHora { get; set; }
    }
}

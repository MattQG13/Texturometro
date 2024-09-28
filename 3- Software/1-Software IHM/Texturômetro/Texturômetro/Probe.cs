using ClassesSuporteTexturometro;
using System;

namespace ProbeTexturometro {
    public abstract class Probe {
        public TipoProbe Tipo { get; set; }
        public Probe() {
        }

        public abstract String getDimin();
    }

    public class ProbeCircular : Probe {
        public double Diametro { get; set; }
        public double Area { get; set; }
        public ProbeCircular (double Diametro){
            Tipo = TipoProbe.Circular;
            this.Diametro = Diametro;
            Area = (Math.PI*Diametro*Diametro)/4;
        }

        public override String getDimin() {
            return $"⌀ {Diametro} mm";
        }
    }

    public class ProbeRetangular : Probe
    { 
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Area { get; set; }
        public ProbeRetangular(double Largura, double Comprimento)
        {
            Tipo = TipoProbe.Retangular;
            this.Largura = Largura;
            this.Comprimento = Comprimento;
            Area = Largura*Comprimento;
        }


        public override String getDimin() {
            return $"{Largura} mm × {Comprimento} mm";
        }
    }

    public class ProbeCisalhamento : Probe
    {
       public ProbeCisalhamento()
       {
            Tipo = TipoProbe.Cisalhamento;
        }
        public override String getDimin() {
            return String.Empty;
        }

        }

        public sealed class ProbeFactoryMethod{
        
        public static Probe ProbeCreate(double Diametro) {
            return new ProbeCircular(Diametro);
        }
        public static Probe ProbeCreate (double Largura, double Comprimento)
        {
            return new ProbeRetangular(Largura, Comprimento);
        }
        public static Probe ProbeCreate() {
            return new ProbeCisalhamento();
        }
    }
}

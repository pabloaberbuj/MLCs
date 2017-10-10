using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_Independiente_IMRT
{
    public class Subcampo
    {
        public string nombre { get; set; }
        public double indice { get; set; }
        public double[] posicionesA { get; set; }
        public double[] posicionesB { get; set; }
    }
}

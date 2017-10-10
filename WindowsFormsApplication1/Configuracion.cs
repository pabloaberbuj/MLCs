using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_Independiente_IMRT
{
    public static class Configuracion
    {
        public static double tamMatriz { get; set; } //tamaño matriz en cm
        public static double numPuntos { get; set; } //número de puntos en cada linea
        public static double factorTransmision { get; set; } //factor de transmisión de una lámina
    }
}

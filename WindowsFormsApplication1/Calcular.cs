using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_Independiente_IMRT
{
    public class Calcular
    {
        public static bool estaLibreSubCampo(int indice, int lamina, Subcampo sC, double tamMatriz, double numPuntos)
        {
            return (posicionIndice(indice, tamMatriz, numPuntos) > -sC.posicionesB[lamina - 1] && posicionIndice(indice, tamMatriz, numPuntos) < sC.posicionesA[lamina - 1]);
        }

        public static double posicionIndice(int indice,double tamMatriz, double numPuntos)
        {
            return indice * tamMatriz / numPuntos - tamMatriz / 2;
        }
        
        public static double fluenciaCoordLamina(int lamina, Campo campo, double tamMatriz, double numPuntos, double factorTransmision)
        {
            
        }
    }
}

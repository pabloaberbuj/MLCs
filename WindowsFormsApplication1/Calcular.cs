using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Calculo_Independiente_IMRT
{
    public class Calcular
    {

        public static int distanciaEntrePixeles(int indiceX1, int indiceX2, int )

        public static double posicionIndice(int indice, double tamMatriz, int numPuntos)
        {
            return indice * tamMatriz / numPuntos - tamMatriz / 2;
        }

        public static int laminaIndice(int indice, double tamMatriz, int numPuntos, double[] MLCarray)
        {
            double posicion = posicionIndice(indice, tamMatriz, numPuntos);
            int lamina = Array.FindIndex(MLCarray, x => x >= posicion); //busca la lámina que empieza después de ese punto (-1 porque quiero la anterior +1 porque la lámina 1 tiene index = 0)
            if (lamina == -1)
            {
                lamina = MLCarray.Count()-1;
            }
            return lamina;
        }

        public static bool estaLibrePC(int indice, int lamina, PuntoDeControl pC, double tamMatriz, int numPuntos)
        {
            return (posicionIndice(indice, tamMatriz, numPuntos) > -pC.posicionesB[lamina] && posicionIndice(indice, tamMatriz, numPuntos) < pC.posicionesA[lamina]);
        }
        
        public static double fluenciaIndiceLaminaPC(int indice, int lamina, PuntoDeControl pC, double tamMatriz, int numPuntos, double factorTransmision)
        {
            if (estaLibrePC(indice,lamina,pC,tamMatriz,numPuntos))
            {
                return 1;
            }
            else
            {
                return factorTransmision;
            }
        }
        public static double fluenciaIndiceLaminaCampo(int indice, int lamina, Campo campo, double tamMatriz, int numPuntos, double factorTransmision)
        {
            double fluenciaSinNormaliz = 0;
            foreach (PuntoDeControl pC in campo.puntosDeControl)
            {
                fluenciaSinNormaliz += fluenciaIndiceLaminaPC(indice, lamina, pC, tamMatriz, numPuntos, factorTransmision);
            }
            return fluenciaSinNormaliz /campo.numPC;
        }

        public static double fluenciaPunto(int indiceX, int indiceY, Campo campo, double tamMatriz, int numPuntos, double factorTransmision, double[] MLCarray)
        {
            int lamina = laminaIndice(indiceY, tamMatriz, numPuntos, MLCarray);
            return fluenciaIndiceLaminaCampo(indiceX, lamina, campo, tamMatriz, numPuntos, factorTransmision);
        }

        public static double[,] fluenciaCampo (Campo campo, double tamMatriz, int numPuntos, double factorTransmision, double[] MLCarray)
        {
            double[,] fluencia = new double[numPuntos, numPuntos];
            for (int i=0;i<numPuntos;i++)
            {
                for (int j=0;j<numPuntos;j++)
                {
                    fluencia[i, j] = fluenciaPunto(i, j, campo, tamMatriz, numPuntos, factorTransmision, MLCarray);
                }
            }
            return fluencia;
        }
    }
}

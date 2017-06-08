using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Statistics;

namespace WindowsFormsApplication1
{
    static class Metodos
    {
        public static double extraerlinea(string[] fid, int linea)
        {
            string aux = fid[linea]; string[] aux2 = aux.Split('='); double salida = Convert.ToDouble(aux2[1]);
            return salida;
        }
        public static string extraerstring(string[] fid, int linea)
        {
            string aux = fid[linea]; string[] aux2 = aux.Split('='); string salida = aux2[1];
            return salida;
        }
        public static Matrix<double> extraermatriz(string[] fid, int lineaI, int lineaF, int Tam1, int Tam2)
        {
            double[,] M = new double[Tam1, Tam2];
            for (int i = 0; i < Tam2; i++)
            {
                double[] aux1 = Array.ConvertAll(fid[lineaI + i].Split('\t'), new Converter<string, double>(Double.Parse));
                for (int j = 0; j < Tam1; j++)
                {
                    M[j, i] = aux1[j];
                }
            }
            Matrix<double> m = Matrix<double>.Build.DenseOfArray(M); //convierto a matriz de Mathdotnet
            return m;
        }
        public static Vector<double> eje(double tam, int resol)
        {
            Vector<double> Eje = Vector<double>.Build.Dense(resol+1,i=>-tam+2*tam/resol*i);
            return Eje;
            
        }
        public static Matrix<double> posicAfluencia(Vector<double> vMA, Vector<double> vMB, int resol,double tam)
        {
            Matrix<double> FlI = Matrix<double>.Build.Dense(60,resol/2+1);
            Matrix<double> FlD = Matrix<double>.Build.Dense(60, resol/2);
            Matrix<double> Fl = Matrix<double>.Build.Dense(60, resol+1);
        for (int j = 0; j < 60; j++)
            {
                for (int i = 0; i < resol/2+1; i++)
                {
                    if (vMB[j] > -(-tam + 2 * tam / resol * i) & -vMA[j] < -(-tam + 2 * tam / resol * i))
                    {
                        FlI[j, i] = 1;
                    }
                    else
                    {
                        FlI[j, i] = 0;
                    }
                }
                for (int i = 0; i < resol /2; i++)
                {
                    if (-vMB[j] < (2 * tam / resol * (i+1)) & vMA[j] > (2 * tam / resol * (i+1)))
                    {
                        FlD[j, i] = 1;
                    }
                    else
                    {
                        FlD[j, i] = 0;
                    }
                }
            }
           Fl = FlI.Append(FlD);
           return Fl;
        }
       
        public static Matrix<double> CorreccionMLC(Matrix<double> Fl, int resol)
        {
            Matrix<double> Fluencia = Matrix<double>.Build.Dense(resol+1,1); //la inicializo con una fila vacía
            Vector<double> linea = Vector<double>.Build.Dense(resol + 1,1);
            for (int i=0;i<60;i++)
                
                if (i<10)
                {
                    Fluencia=Fluencia.InsertColumn(2 * i, Fl.Row(i));
                    Fluencia = Fluencia.InsertColumn(2 * i + 1, Fl.Row(i));
                }
                else if (i<50)
                {
                    Fluencia = Fluencia.InsertColumn(10+i, Fl.Row(i));
                }
                else
                {
                    Fluencia = Fluencia.InsertColumn(60 + 2 * (i - 50), Fl.Row(i));
                    Fluencia = Fluencia.InsertColumn(60 + 2 * (i - 50) + 1, Fl.Row(i));
                }
            Fluencia.RemoveColumn(80);//remuevo la fila de la inicialización
            return Fluencia;
        }
    }
}

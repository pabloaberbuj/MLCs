using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_Independiente_IMRT
{
    public class Extraer
    {
        public static string[] cargar(string archivo)
        {
            try
            {
                return File.ReadAllLines(archivo);
            }
            catch (Exception e)
            {
                MessageBox.Show("No se ha podido abrir. Posiblemente el archivo esté en uso por otra aplicación\n\n" + e.ToString(), "Error abriendo el archivo");
                throw;
            }
        }

        public static string extraerString(string[] fid, int linea, char sep = '=')
        {
            string aux = fid[linea]; string[] aux2 = aux.Split(sep); string salida = aux2[1];
            return salida;
        }
        public static double extraerDouble(string[] fid, int linea)
        {
            return Convert.ToDouble(extraerString(fid, linea));
        }

        public static int buscarSubStringEnFid(string[] fid, string SubString, int Inicio = 0)
        {
            int indice = 0;
            for (int i = Inicio; i < fid.Length; i++)
            {
                if (fid[i].Contains(SubString))
                {
                    indice = i;
                    break;
                }
            }
            return indice;
        }

        public static string buscarYExtraerString(string[] fid, string buscar, char sep = '=')
        {
            int linea = buscarSubStringEnFid(fid, buscar);
            return extraerString(fid, linea, sep);
        }

        public static string apellido(string[] fid)
        {
            return buscarYExtraerString(fid, "Last Name");
        }

        public static string nombre(string[] fid)
        {
            return buscarYExtraerString(fid, "First Name");
        }

        public static string hc(string[] fid)
        {
            return buscarYExtraerString(fid, "Patient ID");
        }

        public static int numSubCampos(string[] fid)
        {
            return Convert.ToInt32(buscarYExtraerString(fid, "Number of Fields"));
        }

        public static Subcampo extraerSubcampo(string[] fid, int indice)
        {
            int inicio = buscarSubStringEnFid(fid,"Field =",indice);
            Subcampo sC = new Subcampo()
            {
                nombre = extraerString(fid, inicio),
                indice = extraerDouble(fid, inicio + 1),
                posicionesA = new double[60],
                posicionesB = new double[60],
            };
            for (int i=0; i<60; i++)
            {
                sC.posicionesA[i] = extraerDouble(fid, inicio + i + 5);
                sC.posicionesB[i] = extraerDouble(fid, inicio + i + 65);
            }
            return sC;
        }

        public static Campo extraerCampo(string[] fid)
        {
            int indice = 0;
            Campo campo = new Campo()
            {
                numSubCampos = numSubCampos(fid),
            };
            campo.subCampos = new List<Subcampo>();
            for (int i=0;i<campo.numSubCampos;i++)
            {
                campo.subCampos.Add(extraerSubcampo(fid, indice));
                indice += 60;
            }
            return campo;
        }

    }
}

using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Statistics;

namespace Calculo_Independiente_IMRT
{
    public class Variables
    {
        public double tam = 40; //max(x1,x2)
        public int resol = 80; //resolucion en x. Tiene que ser un número par
        public string[] fid;
        public string Apellido, Nombre, ID;
        public int NC;
        public Matrix<double> MA, MB, Fl, Fluencia;
        public Vector<double> EjeX;
    }
}

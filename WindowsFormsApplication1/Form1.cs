﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Data.Text;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Variables vr = new Variables();
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void extraer(string[] fid)
        {
           vr.Apellido = Metodos.extraerstring(vr.fid, 2);
           vr.Nombre = Metodos.extraerstring(vr.fid, 3);
           vr.ID = Metodos.extraerstring(vr.fid, 4);
           vr.NC = Convert.ToInt32(Metodos.extraerlinea(vr.fid, 5));
           
            vr.MA = Matrix<double>.Build.Dense(60, vr.NC);
            vr.MB = Matrix<double>.Build.Dense(60, vr.NC);
            vr.Fl = Matrix<double>.Build.Dense(60, vr.resol + 1);

            for (int k=0; k<vr.NC; k++)
            {
                for (int j = 0; j < 60; j++)
                {
                    vr.MA[j, k] = Metodos.extraerlinea(fid, 14 + j + k * 129);
                    vr.MB[j, k] = Metodos.extraerlinea(fid, 14+ 60 + j + k * 129);

                }
                Matrix<double> aux = Matrix<double>.Build.Dense(60, vr.resol + 1);
                aux = Metodos.posicAfluencia(vr.MA.Column(k), vr.MB.Column(k), vr.resol, vr.tam);
                vr.Fl += aux;
            }
            vr.Fluencia = Metodos.CorreccionMLC(vr.Fl,vr.resol);
            MessageBox.Show(Convert.ToString(vr.Fluencia.Enumerate().Sum()));

        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos mlc(.mlc)|*.mlc|All Files (*.*)|*.*";
            openFileDialog1.ShowDialog();
            vr.fid = File.ReadAllLines(openFileDialog1.FileName);
            extraer(vr.fid);
        }
    }
}

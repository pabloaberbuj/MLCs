using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_Independiente_IMRT
{
    public class Campo
    {
        public int numSubCampos { get; set; }
        public List<Subcampo> subCampos {get;set;}
        public double [,] mapaFluencia { get; set; }
    }
}

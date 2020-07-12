using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Data
{
    public class F_FormularioDiagInicial : Base
    {
        public int NumeroSeccion { get; set; }

        public string NombreSeccion { get; set; }

        public string Enunciado { get; set; }

        public string Tipo { get; set; }

        public string Opciones { get; set; }
    }
}

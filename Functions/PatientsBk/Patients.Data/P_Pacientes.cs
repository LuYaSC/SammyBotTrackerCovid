using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Data
{
    public class P_Pacientes : Base
    {
        public string NumeroContacto { get; set; }

        public string CI { get; set; }

        public int UsuarioWpp { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public virtual List<P_Controles> Controles { get; set; }
    }
}

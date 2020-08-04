using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.CaseRecovery.Business.Models
{
    public class CasesForRecoverResult
    {
        public int CasoId { get; set; }

        public string NumeroContacto { get; set; }

        public int Nivel { get; set; }

        public DateTime FechaAtencion { get; set; }

        public string Observaciones { get; set; }
    }
}

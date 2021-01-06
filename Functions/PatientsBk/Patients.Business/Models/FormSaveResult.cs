using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Business.Models
{
    public class FormSaveResult
    {
        public int NumeroDiagnostico { get; set; }

        public string HistoriaClinica { get; set; }

        public string Paciente { get; set; }

        public string Matricula { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.EvolutionForm.Business.Models
{
    public class GetHistoryClinicResult
    {
        public int Id { get; set; }

        public int IdForm { get; set; }

        public string NumeroHistoria { get; set; }

        public string Paciente { get; set; }

        public DateTime DateCreation { get; set; }

        public string TieneDiagInicial { get; set; }

        public string TieneDiagEvolucion { get; set; }
    }
}

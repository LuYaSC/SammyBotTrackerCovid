using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.GetInformationPatient.Business.Models
{
    public class GetInsurancePatientResult
    {
        public string Name { get; set; }

        public string DocumentNumber { get; set; }

        public string BornDate { get; set; }

        public string Insurance { get; set; }

        public string Departament { get; set; }

        public string Municipality { get; set; }
    }
}

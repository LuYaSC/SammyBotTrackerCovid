using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Connectors.HealthInsurance.GetInsurancePerson
{
    public class GetInsurancePersonRequest
    {
        public string DocumentNumber { get; set; }

        public string ComplementDocument { get; set; }

        public DateTime BornDate { get; set; }

        public string Name { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }
    }
}

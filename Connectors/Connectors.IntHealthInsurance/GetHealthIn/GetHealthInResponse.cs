using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Connectors.IntHealthInsurance.GetHealthIn
{
    public class GetHealthInResponse
    {
        public string name { get; set; }

        public string documentNumber { get; set; }

        public string bornDate { get; set; }

        public string insurance { get; set; }

        public string departament { get; set; }

        public string municipality { get; set; }
    }
}
